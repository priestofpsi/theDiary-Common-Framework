using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Templates
{
    public sealed class TemplateParser
    {
        #region Constructors
        public TemplateParser(System.IO.FileInfo templateFile)
            : this(templateFile, TemplateSettings.Default)
        {
            
        }

        public TemplateParser(System.IO.FileInfo templateFile, TemplateSettings settings)
        {
            if (templateFile == null)
                throw new ArgumentNullException("templateFile");

            if (!templateFile.Exists)
                throw new System.IO.FileNotFoundException(string.Format("The template file '{0}' can not be found.", templateFile.Name));
            
            if (settings == null)
                throw new ArgumentNullException("settings");
            
            using (var fs = templateFile.OpenText())
            {
                this.content = fs.ReadToEnd();
            }
            this.settings = settings;
        }

        public TemplateParser(string templateContent)
        {
            this.content = templateContent;
            this.settings = TemplateSettings.Default;
        }

        public TemplateParser(string templateContent, TemplateSettings settings)
        {
            this.content = templateContent;
            this.settings = settings;
        }
        #endregion

        #region Private Static Declarations
        private static readonly Dictionary<Type, Dictionary<PropertyInfo, string>> templateReplacementValues = new Dictionary<Type, Dictionary<PropertyInfo, string>>();
        private static readonly object syncOject = new object();
        #endregion

        #region Private Declarations
        private System.StringBuilder content;
        private TemplateSettings settings;
        
        #endregion

        #region Private Static Read-Only Properties
        private static Dictionary<Type, Dictionary<PropertyInfo, string>> TemplateReplacementValues
        {
            get
            {
                lock (TemplateParser.syncOject)
                {
                    return TemplateParser.templateReplacementValues;
                }
            }
        }
        #endregion

        #region Public Read-Only Properties
        public TemplateSettings Settings
        {
            get
            {
                return this.settings;
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }
        }
        #endregion

        public string Populate(params object[] contentValues)
        {            
            if (contentValues == null)
                throw new ArgumentNullException("contentValues");

            System.StringBuilder replacedContent = new System.StringBuilder(content);
            contentValues.Where(a=>a.IsNotNull()).ForEachAsParallel(contentValue => {
                Type contentValueType = contentValue.GetType();
                contentValueType.GetProperties().Where(a => a.CanRead).ForEachAsParallel(property =>
                {
                    string templateValue = TemplateParser.GetTemplateReplacementValue(contentValueType, property);
                    if (replacedContent.Contains(templateValue))
                        replacedContent.Replace(templateValue, property.GetValue(contentValue).ToString());
                });
            });
            return replacedContent;
        }

        public bool TryPopulate(out string result, params object[] contentValues)
        {
            result = string.Empty;
            if (contentValues.IsNullOrEmpty())
                return false;

            result = this.Populate(contentValues);
            return true;
        }

        #region Private Static Methods & Functions
        private static string GenerateTemplateReplacement(Type type, PropertyInfo property)
        {
            System.StringBuilder templateValue = new System.StringBuilder("{");
            templateValue.Append(type.Name);
            templateValue.Append(":");
            templateValue.Append(property.Name);
            templateValue.Append("}");

            return templateValue;
        }

        private static string GetTemplateReplacementValue(Type type, PropertyInfo property)
        {
            if (!TemplateParser.ContainsTemplateReplacementValue(type, property))
                TemplateParser.AddTemplateReplacementValue(type, property, TemplateParser.GenerateTemplateReplacement(type, property));

            return TemplateParser.TemplateReplacementValues[type][property];
        }

        private static bool ContainsTemplateReplacementValue(Type type, PropertyInfo property)
        {
            if (!TemplateParser.TemplateReplacementValues.ContainsKey(type))
                TemplateParser.TemplateReplacementValues.Add(type, new Dictionary<PropertyInfo, string>());
            return TemplateParser.TemplateReplacementValues[type].ContainsKey(property);
        }

        private static void AddTemplateReplacementValue(Type type, PropertyInfo property, string value)
        {
            if (!TemplateParser.ContainsTemplateReplacementValue(type, property))
                TemplateParser.TemplateReplacementValues[type].Add(property, value);
        }
        #endregion
    }
}
