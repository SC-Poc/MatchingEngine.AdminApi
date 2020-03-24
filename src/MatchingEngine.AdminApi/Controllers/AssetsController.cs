using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Client;
using AutoMapper;
using MatchingEngine.AdminApi.Models;
using MatchingEngine.AdminApi.Models.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MatchingEngine.AdminApi.Extensions;

namespace MatchingEngine.AdminApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetsClient _assetsClient;
        private readonly IMapper _mapper;

        public AssetsController(IAssetsClient assetsClient, IMapper mapper)
        {
            _assetsClient = assetsClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string filter, string sortField, string sortOrder, int pageIndex,
            int pageSize)
        {
            var assets = await _assetsClient.Assets.GetAllAsync();

            var query = assets.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(asset => asset.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(sortField))
                query = query.Order(sortField, sortOrder);

            var count = query.Count();

            query = query
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            var model = _mapper.Map<List<AssetModel>>(query.ToList());

            return Ok(new PagedResponse<AssetModel> {Items = model, Total = count});
        }

        [HttpGet("{assetId}")]
        public async Task<IActionResult> GetByIdAsync(string assetId)
        {
            var asset = await _assetsClient.Assets.GetByIdAsync(assetId);

            var model = _mapper.Map<AssetModel>(asset);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AssetEditModel model)
        {
            var request = _mapper.Map<Assets.Client.Models.Assets.AssetEditModel>(model);
            request.Id = Guid.NewGuid().ToString();

            var asset = await _assetsClient.Assets.AddAsync(request);

            var newModel = _mapper.Map<AssetModel>(asset);

            return Ok(newModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AssetEditModel model)
        {
            await _assetsClient.Assets.UpdateAsync(_mapper.Map<Assets.Client.Models.Assets.AssetEditModel>(model));

            return NoContent();
        }

        [HttpDelete("{assetId}")]
        public async Task<IActionResult> DeleteAsync(string assetId)
        {
            await _assetsClient.Assets.DeleteAsync(assetId);

            return NoContent();
        }
    }
}
