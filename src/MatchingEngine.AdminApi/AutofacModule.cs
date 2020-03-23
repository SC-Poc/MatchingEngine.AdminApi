using Assets.Client;
using Autofac;
using Assets.Client.Extensions;
using Balances.Client;
using Balances.Client.Extensions;
using MatchingEngine.AdminApi.Services;
using MatchingEngine.Client;
using MatchingEngine.Client.Extensions;
using OrderBooks.Client;
using OrderBooks.Client.Extensions;

namespace MatchingEngine.AdminApi
{
    public class AutofacModule : Module
    {
        private readonly string _assetsServiceUrl;
        private readonly string _balancesServiceUrl;
        private readonly string _orderBooksServiceUrl;
        private readonly string _cashOperationsServiceAddress;
        private readonly string _tradingServiceAddress;

        public AutofacModule(string assetsServiceUrl, string balancesServiceUrl, string orderBooksServiceUrl,
            string cashOperationsServiceAddress, string tradingServiceAddress)
        {
            _assetsServiceUrl = assetsServiceUrl;
            _balancesServiceUrl = balancesServiceUrl;
            _orderBooksServiceUrl = orderBooksServiceUrl;
            _cashOperationsServiceAddress = cashOperationsServiceAddress;
            _tradingServiceAddress = tradingServiceAddress;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>()
                .As<IUsersService>()
                .SingleInstance();

            builder.RegisterAssetsClient(new AssetsClientSettings {ServiceAddress = _assetsServiceUrl});
            builder.RegisterBalancesClient(new BalancesClientSettings {ServiceAddress = _balancesServiceUrl});
            builder.RegisterOrderBooksClient(new OrderBooksClientSettings {ServiceAddress = _orderBooksServiceUrl});
            builder.RegisterMatchingEngineClient(new MatchingEngineClientSettings
            {
                CashOperationsServiceAddress = _cashOperationsServiceAddress,
                TradingServiceAddress = _tradingServiceAddress
            });
        }
    }
}
