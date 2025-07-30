using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResultNet;

namespace Result.Net.Samples.AspNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController() : BaseController
    {
        [HttpGet("GetWithSuccess")]
        public ActionResult<Result<WeatherForecast[]>> GetWithSuccess()
        {
            return ActionResultFor(WeatherForecast.GetDataWithSuccess());
        }

        [HttpGet("GetWithFailure")]
        public ActionResult<Result<WeatherForecast[]>> GetWithFailure()
        {
            return ActionResultFor(WeatherForecast.GetDataWithFailure());
        }


        [HttpGet("GetWithMediator")]
        public async Task<ActionResult<Result<WeatherForecast[]>>> GetWithMediator(IMediator mediator)
        {
            var data = await mediator.Send(new Models.Mediator.GetWeatherForecastQuery());
            return ActionResultFor(data);
        }
    }
}
