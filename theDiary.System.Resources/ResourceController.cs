using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Translation;
using System.Threading.Tasks;

namespace System.Resources
{
    public class ResourceController
        : Singleton<ResourceController, ResourceControllerConfigurationSection>,
        ITranslationHandler
    {
        #region Constructors
        public ResourceController()
        {
            this.resourceDictionary = new Dictionary<Type, ResourceKeys>();
        }

        public ResourceController(Type commonResourceType)
            : this()
        {
            if (commonResourceType == null)
                throw new ArgumentNullException("commonResourceType");

            this.CommonResourceType = commonResourceType;
        }

        public ResourceController(Type commonResourceType, TranslateHandler translateHandler)
        {
            this.Translate += translateHandler;
        }

        #endregion

        #region Private Declarations
        private volatile Dictionary<Type, ResourceKeys> resourceDictionary;
        private readonly object syncObject = new object();
        private static RegExPattern languagePattern = new RegExPattern("((?:[a-z][a-z]+))", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        private Type commonResourceType;
        //private bool saving;
        //private bool isDirty;
        #endregion

        #region Event Declarations
        public event TranslateHandler Translate;
        #endregion

        #region Private Properties
        private Dictionary<Type, ResourceKeys> ResourceDictionary
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.resourceDictionary;
                }
            }
        }

        private bool IsDirty
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.dirtyResources.Count > 0;
                }
            }
        }
        #endregion

        #region Public Read-Only Properties
        public bool AutoTranslate
        {
            get
            {
                return this.Translate != null;
            }
        }

        public Type CommonResourceType
        {
            get
            {
                return this.commonResourceType;
            }
            set
            {
                if (this.commonResourceType == null)
                    this.commonResourceType = value;
            }
        }

        public System.Globalization.CultureInfo DefaultCulture
        {
            get
            {
                return System.Globalization.CultureInfo.InstalledUICulture;
            }
        }

        public System.Globalization.CultureInfo CurrentCulture
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentUICulture;
            }
        }

        public string CurrentCultureISO
        {
            get
            {
                return this.CurrentCulture.TwoLetterISOLanguageName;
            }
        }

        public string CurrentLanguage
        {
            get
            {
                return ResourceController.GetLanguage(this.CurrentCulture);
            }
        }
        #endregion

        #region Public Methods & Functions
        public string GetString<T>(string resourceKey)
        {
            return this.GetString(typeof(T), resourceKey);
        }

        public string GetString(Type type, string resourceKey)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (string.IsNullOrWhiteSpace(resourceKey))
                throw new ArgumentNullException("resourceKey");

            Type modelType = ResourceController.IsIEnumerable(type) ? type.GenericTypeArguments[0] : type;
            if (!this.ResourceDictionary.ContainsKey(modelType))
                this.ResourceDictionary.Add(modelType, ResourceController.Open(modelType, this.CurrentCulture));

            if (!this.ResourceDictionary[modelType].Contains(resourceKey))
                this.ResourceDictionary[modelType].RegisterResourceKey(resourceKey);

            if (!this.ResourceDictionary[modelType][resourceKey].Contains(this.DefaultCulture))
                this.RegisterResourceCultureValue(modelType, resourceKey, this.DefaultCulture);

            if (this.CurrentCultureISO != this.DefaultCulture.TwoLetterISOLanguageName)
            {
                if (!this.ResourceDictionary[modelType][resourceKey].Contains(this.CurrentCulture))
                    this.RegisterResourceCultureValue(modelType, resourceKey, this.CurrentCulture);

                if (!this.ResourceDictionary[modelType][resourceKey].Contains(this.CurrentCultureISO))
                    return this.ResourceDictionary[modelType][resourceKey][this.CurrentCulture];
            }

            return this.ResourceDictionary[modelType][resourceKey][this.CurrentCultureISO];
        }

        public void SaveResources()
        {
        }
        
        public void SaveResources(Type type, System.Globalization.CultureInfo culture, Queue<Tuple<string, string>> dirtyResources)
        {
            string pathRoot = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetCallingAssembly().CodeBase).LocalPath);
            string fileName = GetResourceFileName(type, culture);
            fileName = System.IO.Path.Combine(pathRoot, fileName);
            using (ResourceWriter writer = new ResourceWriter(fileName))
            {
                while (dirtyResources.Count > 0)
                {
                    var value = dirtyResources.Dequeue();
                    writer.AddResource(value.Item1, value.Item2);
                }
            }
        }
        
        public void SaveResources(IEnumerable<KeyValuePair<Tuple<Type, System.Globalization.CultureInfo>, List<Tuple<string, string>>>> dirtyResources)
        {
            if (dirtyResources.IsNullOrEmpty())
                return;

            string pathRoot = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetCallingAssembly().CodeBase).LocalPath);
            foreach (var key in dirtyResources)
            {

            }
        }
        #endregion

        #region Private Methods & Functions
        private void RegisterResourceCultureValue(Type modelType, string resourceKey, System.Globalization.CultureInfo culture)
        {
            bool translate = culture != this.DefaultCulture
                && !ResourceController.ResourceExists(modelType, culture);

            Type resourceManagerType = ResourceController.GetType(modelType.Assembly, modelType.Name);
            PropertyInfo property = null;
            if (ResourceController.PropertyExists(resourceManagerType, resourceKey))
            {
                property = ResourceController.GetProperty(resourceManagerType, resourceKey);
            }
            else if (ResourceController.PropertyExists(this.CommonResourceType, resourceKey))
            {
                resourceManagerType = this.CommonResourceType;
                property = ResourceController.GetProperty(this.CommonResourceType, resourceKey);
            }

            string resourceValue = string.Empty;
            if (property != null)
            {
                resourceValue = property.GetValue(null) as string;

                if (!this.ResourceDictionary[modelType][resourceKey].Contains(culture))
                {
                    if (translate && this.AutoTranslate)
                        resourceValue = this.Translate(this, new TranslateEventArgs(resourceValue, this.DefaultCulture, culture));

                    this.ResourceDictionary[modelType][resourceKey].Add(culture, resourceValue);
                    if (translate)
                        this.AddDirtyResourceKeyValue(modelType, culture, resourceKey, resourceValue);
                }
            }
        }
                
        private void AddDirtyResourceKeyValue(Type modelType, System.Globalization.CultureInfo culture, string resourceKey, string resourceValue)
        {
            var key = new Tuple<Type, Globalization.CultureInfo>(modelType, culture);
            if (!this.dirtyResources.ContainsKey(key))
                this.dirtyResources.Add(key, new Queue<Tuple<string, string>>());
            this.dirtyResources[key].Enqueue(new Tuple<string, string>(resourceKey, resourceValue));
            System.Threading.ThreadPool.QueueUserWorkItem(a =>
                {
                    this.SaveResources(modelType, culture, this.dirtyResources[key]);
                });
        }

        private Dictionary<Tuple<Type, System.Globalization.CultureInfo>, Queue<Tuple<string, string>>> dirtyResources = new Dictionary<Tuple<Type, Globalization.CultureInfo>, Queue<Tuple<string, string>>>();

        private IEnumerable<ResourceKeyValue> GetResourceKeys(Type resourceType)
        {
            string fileName = string.Format("{0}.{1}.resx", resourceType.Name, this.CurrentCultureISO);
            using (System.Resources.ResourceReader resourceReader = new System.Resources.ResourceReader(fileName))
            {
                IDictionaryEnumerator readerEnumerator = resourceReader.GetEnumerator();
                while (readerEnumerator.MoveNext())
                {
                    yield return new ResourceKeyValue(readerEnumerator.Key.ToString(), readerEnumerator.Value.ToString());
                }
            }
        }
        #endregion

        #region Protected Static Methods & Functions
        protected void AddResourceKey(Type resourceType, string resourceKey, string value)
        {
            string pathRoot = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetCallingAssembly().CodeBase).LocalPath);
            string fileName = string.Format("{0}.{1}.resx", resourceType.Name, this.CurrentCultureISO);
            fileName = System.IO.Path.Combine(pathRoot, fileName);
            using (var rw = new System.Resources.ResourceWriter(fileName))
            {
                rw.AddResource(resourceKey, value);
            }
        }

        protected void AddResourceKeys(Type resourceType, CultureInfo culture, IEnumerable<Tuple<string, string>> keyValues)
        {
            string pathRoot = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetCallingAssembly().CodeBase).LocalPath);
            string fileName = string.Format("{0}.{1}.resx", resourceType.Name, culture);
            fileName = System.IO.Path.Combine(pathRoot, fileName);
            using (var rw = new System.Resources.ResourceWriter(fileName))
            {
                foreach (var keyValue in keyValues)
                    rw.AddResource(keyValue.Item1, keyValue.Item2);
            }
        }

        protected bool ResourceKeyExists(Type resourceType, string resourceKey)
        {
            bool exists = false;
            string fileName = string.Format("{0}.{1}.resx", resourceType.Name, this.CurrentCultureISO);
            using (System.Resources.ResourceReader resourceReader = new System.Resources.ResourceReader(fileName))
            {
                string resourceKeyType;
                byte[] resourceKeyValue;
                resourceReader.GetResourceData(resourceKey, out resourceKeyType, out resourceKeyValue);
                exists = resourceKeyValue != null && resourceKeyValue.Length != 0;
            }

            return exists;
        }

        protected static bool ResourceExists(Type resourceType, System.Globalization.CultureInfo culture)
        {
            return new ResourceManager(resourceType).GetResourceSet(culture, true, false) != null;
        }

        protected bool ResourceExists(Type resourceType, string propertyName)
        {
            return ResourceController.GetProperty(resourceType, propertyName) != null
                && new ResourceManager(resourceType).GetResourceSet(this.CurrentCulture, true, false) != null;
        }

        protected static bool IsIEnumerable(Type type)
        {
            return (type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        protected static string GetLanguage()
        {
            return ResourceController.GetLanguage(System.Globalization.CultureInfo.CurrentUICulture);
        }

        protected static string GetLanguage(System.Globalization.CultureInfo cultureInfo)
        {
            return ResourceController.languagePattern.Match(cultureInfo.EnglishName).Value;
        }

        protected static Type GetType(Assembly assembly, string propertyOwner)
        {
            return assembly.GetTypes().Where(a => a.FullName.EndsWith(string.Format("Resources.{0}", propertyOwner))).FirstOrDefault();
        }

        protected static PropertyInfo GetProperty(Type type, string propertyName)
        {
            if (type == null)
                return null;

            return type.GetProperties().Where(prop => prop.Name == propertyName).FirstOrDefault();
        }

        protected static bool PropertyExists(Type type, string propertyName)
        {
            if (type == null)
                return false;

            return type.GetProperties().Where(prop => prop.Name == propertyName).FirstOrDefault() != null;
        }
        #endregion

        #region Private Static Methods & Functions

        private static ResourceKeys Open(Type resourceType, CultureInfo culture)
        {
            string pathRoot = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetCallingAssembly().CodeBase).LocalPath);
            ResourceKeys returnValue = ResourceKeys.Open(resourceType);
            string fileName = GetResourceFileName(resourceType, culture);
            fileName = System.IO.Path.Combine(pathRoot, fileName);
            using (ResourceReader reader = new ResourceReader(fileName))
            {
                IDictionaryEnumerator dict = reader.GetEnumerator();

                while (dict.MoveNext())
                {
                    string resourceKey, resourceValue;
                    resourceKey = dict.Key as string;
                    resourceValue = dict.Value as string;
                    if (resourceKey.IsNullEmptyOrWhiteSpace() || resourceValue.IsNullEmptyOrWhiteSpace())
                        continue;

                    returnValue.Add(resourceKey, resourceValue, culture);
                }
            }

            return returnValue;
        }

        private static string GetResourceFileName(Type resourceType, CultureInfo culture)
        {
            return string.Format("{0}.{1}.resx", resourceType.Name, culture.TwoLetterISOLanguageName);
        }
        #endregion
    }
}
