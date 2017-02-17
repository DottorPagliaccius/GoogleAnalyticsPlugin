using System;
using System.Collections.Generic;

namespace Plugin.GoogleAnalytics.Abstractions
{
    public interface IGoogleAnalytics
    {
        bool EnableAdvertisingTracking { get; set; }

        void TrackPage(string pageName);
        void TrackEvent(string category, string action, string label, long value);
        void Report(string message, bool isFatal = false);
        void Report(Exception exception, bool isFatal = false);
        void Report(Exception exception, IDictionary<string, string> extraData, bool isFatal = false);
    }
}
