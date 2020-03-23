using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Balances.Client;
using MatchingEngine.AdminApi.Models;
using MatchingEngine.AdminApi.Models.Wallets;
using MatchingEngine.Client;
using Microsoft.AspNetCore.Mvc;

namespace MatchingEngine.AdminApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IBalancesClient _balancesClient;
        private readonly IMatchingEngineClient _matchingEngineClient;
        private readonly IMapper _mapper;

        public WalletsController(
            IBalancesClient balancesClient,
            IMatchingEngineClient matchingEngineClient,
            IMapper mapper)
        {
            _balancesClient = balancesClient;
            _matchingEngineClient = matchingEngineClient;
            _mapper = mapper;
        }

        [HttpGet("{walletId}")]
        public async Task<IActionResult> GetAllAsync(string walletId, string sortField, string sortOrder, int pageIndex,
            int pageSize)
        {
            var balances = await _balancesClient.Balances.GetAllAsync(walletId);

            var query = balances.AsQueryable();

            // if (!string.IsNullOrEmpty(sortField))
            //     query = query.Order(sortField, sortOrder);

            var count = query.Count();

            query = query
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            var model = _mapper.Map<List<BalanceModel>>(query.ToList());

            return Ok(new PagedResponse<BalanceModel> {Items = model, Total = count});
        }

        [HttpGet("{walletId}/assets/{assetId}")]
        public async Task<IActionResult> GetByAssetIdAsync(string walletId, string assetId)
        {
            var balance = await _balancesClient.Balances.GetByAssetIdAsync(walletId, assetId);

            var model = _mapper.Map<BalanceModel>(balance);

            return Ok(model);
        }

        [HttpPost("cash-in")]
        public async Task<IActionResult> CashInAsync([FromBody] CashOperationModel model)
        {
            await _matchingEngineClient.CashOperations.CashInAsync(model.WalletId, model.AssetId, model.Amount);

            return NoContent();
        }

        [HttpPost("cash-out")]
        public async Task<IActionResult> CashOutAsync([FromBody] CashOperationModel model)
        {
            await _matchingEngineClient.CashOperations.CashOutAsync(model.WalletId, model.AssetId, model.Amount);

            return NoContent();
        }
    }
}
