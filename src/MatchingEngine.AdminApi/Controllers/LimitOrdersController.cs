using System;
using System.Threading.Tasks;
using MatchingEngine.AdminApi.Models;
using MatchingEngine.AdminApi.Models.LimitOrders;
using MatchingEngine.Client;
using Microsoft.AspNetCore.Mvc;

namespace MatchingEngine.AdminApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LimitOrdersController : ControllerBase
    {
        private readonly IMatchingEngineClient _matchingEngineClient;

        public LimitOrdersController(IMatchingEngineClient matchingEngineClient)
        {
            _matchingEngineClient = matchingEngineClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] LimitOrderCreateModel model)
        {
            var result = await _matchingEngineClient.Trading.CreateLimitOrderAsync(
                new MatchingEngine.Client.Models.Trading.LimitOrderRequestModel
                {
                    Id = Guid.NewGuid(),
                    AssetPairId = model.AssetPairId,
                    Price = model.Price,
                    Volume = model.Type == LimitOrderType.Sell
                        ? -model.Volume
                        : model.Volume,
                    WalletId = model.WalletId,
                    CancelAllPreviousLimitOrders = model.CancelPrevious,
                    Timestamp = DateTime.UtcNow
                });

            var response = new LimitOrderResponseModel {Id = result.Id, Status = result.Status, Reason = result.Reason};

            return Ok(response);
        }

        [HttpDelete("{limitOrderId}")]
        public async Task<IActionResult> CancelAsync(Guid limitOrderId)
        {
            await _matchingEngineClient.Trading.CancelLimitOrderAsync(limitOrderId);

            return NoContent();
        }
    }
}
