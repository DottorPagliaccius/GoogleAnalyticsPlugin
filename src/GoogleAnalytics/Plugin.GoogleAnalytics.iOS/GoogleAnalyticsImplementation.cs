using System;
using Foundation;
using Google.Analytics;
using Plugin.GoogleAnalytics.Abstractions;

namespace Plugin.GoogleAnalytics
{
    /// <summary>
    /// Implementation for GoogleAnalytics
    /// </summary>
    public class GoogleAnalyticsImplementation : AbstractGoogleAnalytics
    {
        private const string AllowTrackingKey = "AllowTracking";

        private static ITracker _tracker;

        private bool _enableAdvertisingTracking;

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:Plugin.GoogleAnalytics.GoogleAnalyticsImplementation"/> enable advertising tracking.
        /// </summary>
        /// <value><c>true</c> if enable advertising tracking; otherwise, <c>false</c>.</value>
        public override bool EnableAdvertisingTracking
        {
            get
            {
                return _enableAdvertisingTracking;
            }

            set
            {
                _enableAdvertisingTracking = value;

                _tracker?.SetAllowIdfaCollection(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Plugin.GoogleAnalytics.GoogleAnalyticsImplementation"/> class.
        /// </summary>
        public GoogleAnalyticsImplementation()
        {
            if (_tracker == null)
                CrossGoogleAnalytics.NotInitialized();
        }

        /// <summary>
        /// Init the specified trackerName and trackingId.
        /// </summary>
        /// <param name="trackerName">Tracker name.</param>
        /// <param name="trackingId">Tracking identifier.</param>
        public static void Init(string trackerName, string trackingId)
        {
            if (trackingId == null)
                throw new ArgumentNullException(nameof(trackingId));

            var optionsDict = NSDictionary.FromObjectAndKey(new NSString("YES"), new NSString(AllowTrackingKey));
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(optionsDict);

            Gai.SharedInstance.OptOut = !NSUserDefaults.StandardUserDefaults.BoolForKey(AllowTrackingKey);
            Gai.SharedInstance.DispatchInterval = 10;
            Gai.SharedInstance.TrackUncaughtExceptions = true;

            _tracker = Gai.SharedInstance.GetTracker(trackerName, trackingId);
        }

        /// <summary>
        /// Report the specified message and warningLevel.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="warningLevel">Warning level.</param>
        public override void Report(string message, Severity warningLevel = Severity.Warning)
        {
            _tracker.Send(DictionaryBuilder.CreateException(message, warningLevel != Severity.Warning).Build());
        }

        /// <summary>
        /// Tracks the page.
        /// </summary>
        /// <param name="pageName">Page name.</param>
        public override void TrackPage(string pageName)
        {
            Gai.SharedInstance.DefaultTracker.Set(GaiConstants.ScreenName, pageName);
            Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateScreenView().Build());
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="category">Category.</param>
        /// <param name="action">Action.</param>
        /// <param name="label">Label.</param>
        /// <param name="value">Value.</param>
        public override void TrackEvent(string category, string action, string label, long value)
        {
            Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateEvent(category, action, label, new NSNumber(value)).Build());
            Gai.SharedInstance.Dispatch();
        }
    }
}