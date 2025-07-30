using MediatR;
using ResultNet;

namespace Result.Net.Samples.AspNetCore.Models.Mediator
{
    public class GetWeatherForecastQuery : IRequest<Result<WeatherForecast[]>> { }

    public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, Result<WeatherForecast[]>>
    {
        public Task<Result<WeatherForecast[]>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var data = WeatherForecast.GetDataWithSuccess();
            return Task.FromResult(data);
        }
    }
}
