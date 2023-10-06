using ExevopanNotification.Api.Controllers.Base;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExevopanNotification.Api.Controllers.V1
{
    public class PriceTrendController : BaseController
    {
        private readonly IPriceTrendService _priceTrendService;

        public PriceTrendController(IPriceTrendService priceTrendService)
        {
            _priceTrendService = priceTrendService;
        }


        [HttpGet("{characterName}")]
        public async Task<IActionResult> GetPriceTrend(string characterName, int? analyzeMinLevel = 300, int? analyzeMaxLevel = null)
        {
            return await Analyze(characterName, false, analyzeMinLevel, analyzeMaxLevel);
        }

        [HttpGet("{characterName}/history")]
        public async Task<IActionResult> GetPriceTrendHistory(string characterName, int? analyzeMinLevel = 300, int? analyzeMaxLevel = null)
        {

            return await Analyze(characterName, true, analyzeMinLevel, analyzeMaxLevel);
        }

        private async Task<IActionResult> Analyze(string characterName, bool history, int? analyzeMinLevel, int? analyzeMaxLevel)
        {
            var filterLimits = FilterLimits.Create(analyzeMinLevel, analyzeMaxLevel);
            var priceTrend = await _priceTrendService.Analyze(characterName, history, filterLimits);

            if (priceTrend == 0)
            {
                return Problem();
            }

            return Ok(new
            {
                characterName,
                priceTrend
            });
        }
    }
}
