using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MatchingEngine.AdminApi.Models.OrderBooks;
using Microsoft.AspNetCore.Mvc;
using OrderBooks.Client;

namespace MatchingEngine.AdminApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderBooksController : ControllerBase
    {
        private readonly IOrderBooksClient _orderBooksClient;
        private readonly IMapper _mapper;

        public OrderBooksController(IOrderBooksClient orderBooksClient, IMapper mapper)
        {
            _orderBooksClient = orderBooksClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var orderBooks = await _orderBooksClient.OrderBooks.GetAllAsync();

            var model = orderBooks
                .Select(ToModel)
                .OrderBy(item => item.AssetPairId)
                .ToList();

            return Ok(model);
        }

        [HttpGet("{assetPairId}")]
        public async Task<IActionResult> GetByAssetPairIdAsync(string assetPairId)
        {
            var orderBook = await _orderBooksClient.OrderBooks.GetByAssetPairIdAsync(assetPairId);

            var model = _mapper.Map<OrderBookModel>(orderBook);

            return Ok(model);
        }

        private static OrderBookListItemModel ToModel(OrderBooks.Client.Models.OrderBooks.OrderBookModel orderBook)
        {
            var sellLimitOrders = orderBook.LimitOrders
                .Where(limitOrder => limitOrder.Type == OrderBooks.Client.Models.OrderBooks.LimitOrderType.Sell)
                .ToList();

            var buyLimitOrders = orderBook.LimitOrders
                .Where(limitOrder => limitOrder.Type == OrderBooks.Client.Models.OrderBooks.LimitOrderType.Buy)
                .ToList();

            var ask = sellLimitOrders
                .OrderBy(limitOrder => limitOrder.Price)
                .FirstOrDefault()?.Price;

            var bid = buyLimitOrders
                .OrderByDescending(limitOrder => limitOrder.Price)
                .FirstOrDefault()?.Price;

            return new OrderBookListItemModel
            {
                AssetPairId = orderBook.AssetPairId,
                Ask = sellLimitOrders.OrderBy(limitOrder => limitOrder.Price).FirstOrDefault()?.Price,
                Bid = bid,
                Mid = ask.HasValue && bid.HasValue ? (ask - bid) / 2 : null,
                Spread = ask.HasValue && bid.HasValue ? ask - bid : null,
                SellOrdersCount = sellLimitOrders.Count,
                BuyOrdersCount = buyLimitOrders.Count,
                Timestamp = orderBook.Timestamp
            };
        }
    }
}
