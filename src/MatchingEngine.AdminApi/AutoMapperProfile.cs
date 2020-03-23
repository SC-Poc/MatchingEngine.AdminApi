using AutoMapper;
using MatchingEngine.AdminApi.Models;
using MatchingEngine.AdminApi.Models.AssetPairs;
using MatchingEngine.AdminApi.Models.Assets;
using MatchingEngine.AdminApi.Models.Candles;
using MatchingEngine.AdminApi.Models.OrderBooks;
using MatchingEngine.AdminApi.Models.Wallets;

namespace MatchingEngine.AdminApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Assets.Client.Models.Assets.AssetModel, AssetModel>(MemberList.Source);
            CreateMap<AssetEditModel, Assets.Client.Models.Assets.AssetEditModel>(MemberList.Destination);

            CreateMap<Assets.Client.Models.AssetPairs.AssetPairModel, AssetPairModel>(MemberList.Source);
            CreateMap<AssetPairEditModel, Assets.Client.Models.AssetPairs.AssetPairEditModel>(MemberList.Destination);
            
            CreateMap<OrderBooks.Client.Models.OrderBooks.OrderBookModel, OrderBookModel>(MemberList.Source);
            CreateMap<OrderBooks.Client.Models.OrderBooks.LimitOrderModel, LimitOrderModel>(MemberList.Source);
            CreateMap<OrderBooks.Client.Models.OrderBooks.LimitOrderType, LimitOrderType>(MemberList.Source);
            
            CreateMap<Balances.Client.Models.Wallets.BalanceModel, BalanceModel>(MemberList.Source);
        }
    }
}
