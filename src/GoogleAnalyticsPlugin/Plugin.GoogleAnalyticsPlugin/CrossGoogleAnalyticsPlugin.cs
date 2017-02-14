using Plugin.GoogleAnalyticsPlugin.Abstractions;
using System;

namespace Plugin.GoogleAnalyticsPlugin
{
  /// <summary>
  /// Cross platform GoogleAnalyticsPlugin implemenations
  /// </summary>
  public class CrossGoogleAnalyticsPlugin
  {
    static Lazy<IGoogleAnalyticsPlugin> Implementation = new Lazy<IGoogleAnalyticsPlugin>(() => CreateGoogleAnalyticsPlugin(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IGoogleAnalyticsPlugin Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IGoogleAnalyticsPlugin CreateGoogleAnalyticsPlugin()
    {
#if PORTABLE
        return null;
#else
        return new GoogleAnalyticsPluginImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
