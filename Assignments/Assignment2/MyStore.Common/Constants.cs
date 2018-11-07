using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Common
{
    public static class Constants
    {
        public const string GeneratedBy = "System";
        public static class OrderStatuses
        {
            public const string Submitted = "Submitted";
            public const string Dispatched = "Dispatched";
            public const string AwaitingCollection = "Awaiting Collection";
            public const string AwaitingPayment= "Awaiting Payment";
            public const string Cancelled = "Cancelled";
            public const string Returned = "Returned";
            public const string Collected = "Collected";
            public const string Delivered = "Delivered";
        }
        public static class Roles
        {
            public const string SystemAdministrator = "System Administrator";
            public const string Owner = "Owner";
            public const string User = "User";
        }
        public static class DeliveryMethods
        {

            public const string Delivery = "Delivery";
            public const string Collect = "Collect";
        }
        public static class AddressTypes
        {

            public const string Business = "Business";
            public const string Residential = "Residential";
        }

    }
}
