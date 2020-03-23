using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Client;
using AutoMapper;
using MatchingEngine.AdminApi.Models;
using MatchingEngine.AdminApi.Models.AssetPairs;
using Microsoft.AspNetCore.Mvc;

namespace MatchingEngine.AdminApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetPairsController : ControllerBase
    {
        private readonly IAssetsClient _assetsClient;
        private readonly IMapper _mapper;

        public AssetPairsController(IAssetsClient assetsClient, IMapper mapper)
        {
            _assetsClient = assetsClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string filter, string sortField, string sortOrder, int pageIndex,
            int pageSize)
        {
            var assetPairs = await _assetsClient.AssetPairs.GetAllAsync();

            var query = assetPairs.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(assetPair =>
                    assetPair.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(sortField))
                query = query.Order(sortField, sortOrder);

            var count = query.Count();

            query = query
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            var model = _mapper.Map<List<AssetPairModel>>(query.ToList());

            return Ok(new PagedResponse<AssetPairModel> {Items = model, Total = count});
        }

        [HttpGet("{assetPairId}")]
        public async Task<IActionResult> GetByIdAsync(string assetPairId)
        {
            var assetPair = await _assetsClient.AssetPairs.GetByIdAsync(assetPairId);

            var model = _mapper.Map<AssetPairModel>(assetPair);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AssetPairEditModel model)
        {
            var request = _mapper.Map<Assets.Client.Models.AssetPairs.AssetPairEditModel>(model);

            request.Id = Guid.NewGuid().ToString();
            var asset = await _assetsClient.AssetPairs.AddAsync(request);

            var newModel = _mapper.Map<AssetPairModel>(asset);

            return Ok(newModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AssetPairEditModel model)
        {
            await _assetsClient.AssetPairs.UpdateAsync(
                _mapper.Map<Assets.Client.Models.AssetPairs.AssetPairEditModel>(model));

            return NoContent();
        }

        [HttpDelete("{assetPairId}")]
        public async Task<IActionResult> DeleteAsync(string assetPairId)
        {
            await _assetsClient.AssetPairs.DeleteAsync(assetPairId);

            return NoContent();
        }
    }
}
