using Plugin.GoogleAnalytics.Abstractions;
using System;
using System.Collections.Generic;

namespace Plugin.GoogleAnalytics
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class GoogleAnalyticsImplementation : IGoogleAnalytics
    {
        public void Report(Exception exception, Severity warningLevel = Severity.Warning)
        {
            throw new NotImplementedException();
        }

        public void Report(string message, Severity warningLevel = Severity.Warning)
        {
            throw new NotImplementedException();
        }

        public void Report(Exception exception, IDictionary<string, string> extraData, Severity warningLevel = Severity.Warning)
        {
            throw new NotImplementedException();
        }

        public void TrackEvent(string category, string action, string label, long value)
        {
            throw new NotImplementedException();
        }

        public void TrackPage(string pageName)
        {
            throw new NotImplementedException();
        }
    }
}