using System;
using System.Collections.Generic;

namespace Plugin.GoogleAnalytics.Abstractions
{
    public enum Severity
    {
        Warning,
        Error
    }

    public interface IGoogleAnalytics
    {
        bool EnableAdvertisingTracking { get; set; }

        void TrackPage(string pageName);
        void TrackEvent(string category, string action, string label, long value);
        void Report(string message, Severity warningLevel = Severity.Warning);
        void Report(Exception exception, Severity warningLevel = Severity.Warning);
        void Report(Exception exception, IDictionary<string, string> extraData, Severity warningLevel = Severity.Warning);
    }
}
