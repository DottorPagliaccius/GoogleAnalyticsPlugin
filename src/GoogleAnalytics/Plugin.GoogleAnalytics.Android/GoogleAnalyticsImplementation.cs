using System;
using Android.Content;
using Android.Gms.Analytics;
using Plugin.GoogleAnalytics.Abstractions;

using GA = Android.Gms.Analytics.GoogleAnalytics;

namespace Plugin.GoogleAnalytics
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class GoogleAnalyticsImplementation : AbstractGoogleAnalytics
    {
        private static Tracker _tracker;
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

                _tracker?.EnableAdvertisingIdCollection(value);
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
        /// Init the specified trackingId and AppContext.
        /// </summary>
        /// <param name="trackingId">Tracking identifier.</param>
        /// <param name="AppContext">App context.</param>
        public static void Init(string trackingId, Context AppContext = null)
        {
            if (trackingId == null)
                throw new ArgumentNullException(nameof(trackingId));

            var gaInstance = GA.GetInstance(AppContext);
            gaInstance.SetLocalDispatchPeriod(10);

            _tracker = gaInstance.NewTracker(trackingId);

            _tracker.EnableExceptionReporting(true);
            _tracker.EnableAdvertisingIdCollection(false);
            _tracker.EnableAutoActivityTracking(false);
        }

        /// <summary>
        /// Report the specified message and warningLevel.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="warningLevel">Warning level.</param>
        public override void Report(string message, Severity warningLevel = Severity.Warning)
        {
            var builder = new HitBuilders.ExceptionBuilder();
            builder.SetDescription(message);
            builder.SetFatal(warningLevel != Severity.Warning);

            _tracker.Send(builder.Build());
        }

        /// <summary>
        /// Tracks the page.
        /// </summary>
        /// <param name="pageName">Page name.</param>
        public override void TrackPage(string pageName)
        {
            _tracker.SetScreenName(pageName);
            _tracker.Send(new HitBuilders.ScreenViewBuilder().Build());
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
            var builder = new HitBuilders.EventBuilder();
            builder.SetCategory(category);
            builder.SetAction(action);
            builder.SetLabel(label);
            builder.SetValue(value);

            _tracker.Send(builder.Build());
        }
    }
}