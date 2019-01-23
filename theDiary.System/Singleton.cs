using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Abstract class that provides a thread safe Singleton implementation for an implemention of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> that implements the singleton pattern.</typeparam>
    public abstract class Singleton<T>
        where T : class, new()
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Singleton"/> class.
        /// </summary>
        protected Singleton()
            : base()
        {
        }

        protected Singleton(EventHandler singletonCreatedHandler)
            : this()
        {
            Singleton<T>.SingletonCreated += singletonCreatedHandler;
        }
        #endregion

        #region Private Declarations
        private static volatile T instance;
        #endregion

        public static EventHandler SingletonCreated;
        #region Protected Static Declarations
        /// <summary>
        /// Provides access to the Syncronization object.
        /// </summary>
        protected static readonly object SyncObject = new object();
        #endregion

        #region Public Static Read-Only Properties
        /// <summary>
        /// Gets the singlton instance of <typeparamref name="T"/>
        /// <typeparamref name="T">The <see cref="Type"/> of the singleton instance.</typeparamref>
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (Singleton<T>.SyncObject)
                {
                    if (Singleton<T>.instance == null)
                    {                        
                        Singleton<T>.instance = System.Activator.CreateInstance(typeof(T), true) as T;
                        Singleton<T>.SingletonCreated?.Invoke(Singleton<T>.instance, new EventArgs());
                    }
                    return Singleton<T>.instance;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Abstract class that provides a thread safe Singleton implementation for an implemention of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> that implements the singleton pattern.</typeparam>
    /// <typeparam name="TConfiguration">The <see cref="Type"/> that implements the <see cref="ConfigurationSection"/>.</typeparam>
    public abstract class Singleton<T, TConfiguration>
        : Singleton<T>
        where T : class, new()
        where TConfiguration : ConfigurationSection, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Singleton"/> class.
        /// </summary>
        protected Singleton()
            : base()
        {
        }

        #region Private Static Volatile
        private static volatile ConfigurationScope<TConfiguration> configuration;
        #endregion

        #region Public Static Read-Only Properties
        /// <summary>
        /// Gets the singlton configuration instance.
        /// </summary>
        public static TConfiguration Configuration
        {
            get
            {

                lock (Singleton<T, TConfiguration>.SyncObject)
                {
                    if (Singleton<T, TConfiguration>.configuration == null)
                        Singleton<T, TConfiguration>.configuration = System.Activator.CreateInstance<ConfigurationScope<TConfiguration>>();

                    return Singleton<T, TConfiguration>.configuration.Configuration;
                }
            }
        }
        #endregion
    }
}
