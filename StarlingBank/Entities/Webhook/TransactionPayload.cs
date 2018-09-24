using System;
using StarlingBank.Constants;

namespace StarlingBank.Entities.Webhook
{
    public class TransactionPayload
    {
        public Guid WebhookNotificationUid { get; set; }

        public DateTime TimeStamp { get; set; }

        public TransactionContent Content { get; set; }

        public Guid AccountHolderUid { get; set; }

        /// <summary>
        /// String representation of the webhook event type. See <see cref="WebhookTypes"/> for a complete list.
        /// </summary>
        public string WebhookType { get; set; }

        public Guid CustomerUid { get; set; }

        /// <summary>
        /// Deprecated. Use WebhookNotificationUid instead.
        /// </summary>
        public Guid Uid { get; set; }
    }
}