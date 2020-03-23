using System.Collections.Generic;
using MatchingEngine.AdminApi.Services;

namespace MatchingEngine.AdminApi.Configuration.Service
{
    public class AdminApiSettings
    {
        public string Secret { get; set; }

        public List<User> Users { get; set; }

        public string AssetsServiceUrl { get; set; }

        public string BalancesServiceUrl { get; set; }

        public string OrderBooksServiceUrl { get; set; }

        public string CandlesServiceUrl { get; set; }

        public string CashOperationsServiceAddress { get; set; }

        public string TradingServiceAddress { get; set; }
    }
}
