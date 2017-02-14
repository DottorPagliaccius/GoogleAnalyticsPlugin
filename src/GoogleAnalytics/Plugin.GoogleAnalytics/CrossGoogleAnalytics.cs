using Plugin.GoogleAnalytics.Abstractions;
using System;

namespace Plugin.GoogleAnalytics
{
  /// <summary>
  /// Cross platform GoogleAnalytics implemenations
  /// </summary>
  public class CrossGoogleAnalytics
  {
    static Lazy<IGoogleAnalytics> Implementation = new Lazy<IGoogleAnalytics>(() => CreateGoogleAnalytics(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IGoogleAnalytics Current
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

    static IGoogleAnalytics CreateGoogleAnalytics()
    {
#if PORTABLE
        return null;
#else
        return new GoogleAnalyticsImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
