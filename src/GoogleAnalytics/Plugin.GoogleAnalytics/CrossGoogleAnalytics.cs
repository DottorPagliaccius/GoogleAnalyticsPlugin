using System;
using System.Threading;
using Plugin.GoogleAnalytics.Abstractions;

namespace Plugin.GoogleAnalytics
{
    /// <summary>
    /// Cross platform GoogleAnalytics implemenations
    /// </summary>
    public static class CrossGoogleAnalytics
    {
        private static Lazy<IGoogleAnalytics> Implementation = new Lazy<IGoogleAnalytics>(CreateGoogleAnalytics, LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IGoogleAnalytics Current
        {
            get
            {
                var implementation = Implementation.Value;
                if (implementation == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }

                return implementation;
            }
        }

        private static IGoogleAnalytics CreateGoogleAnalytics()
        {
#if PORTABLE
            return null;
#else
            return new GoogleAnalyticsImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }

        internal static GoogleAnalyticsNotInitializedException NotInitialized()
        {
            return new GoogleAnalyticsNotInitializedException("CrossGoogleAnalytics Plugin is not initialized. Should initialize before use with CrossGoogleAnalytics Init method.");
        }
    }
}
