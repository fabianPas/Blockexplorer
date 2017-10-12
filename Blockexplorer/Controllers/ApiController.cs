using System;
using System.Threading.Tasks;
using Blockexplorer.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blockexplorer.Models.Api;

namespace Blockexplorer.Controllers
{
    public class ApiController : Controller
    {
	    readonly IApiService _apiService;

	    public ApiController(IApiService apiService)
	    {
		    _apiService = apiService;
	    }

	    [HttpGet]
		public async Task<ActionResult> MoneySupply(ResponseFormat format = ResponseFormat.Json)
	    {
		    try
		    {
			    var info = await _apiService.GetInfo();
			    if (info == null || !string.IsNullOrWhiteSpace(info.Errors))
				    return StatusCode(StatusCodes.Status500InternalServerError);

                if (format == ResponseFormat.String)
                    return Content(Convert.ToInt32(info.MoneySupply).ToString());

                return Json(new { moneySupply = Convert.ToInt32(info.MoneySupply) });
			}
		    catch (Exception)
		    {
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
	    }

        [HttpGet]
        public async Task<ActionResult> StakingInfo()
        {
            try
            {
                var stakingInfo = await _apiService.GetStakingInfo()

            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
	}
}
