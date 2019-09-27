using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoAPI;
using GeoAPI.Geometries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using ProjNet;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using WebAPI.DataContracts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/ExchangeRate")]
    [ApiController]
    //[Authorize]
    public class ExchangeRateController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ExchangeRateController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GetExchangeRateResponse>> GetExchangeRates()
        {
            var userId = User.FindFirst("UserId")?.Value;

            var exhangeRates = await _context.ExchangeRates.Where(it => it.ExchangeOfficerId == userId).ToListAsync();

            var response = new GetExchangeRateResponse
            {

                ExchangeOfficerIdentifier = userId,
                ExchangeRates = new List<ExchangeRateItem>()
            };

            exhangeRates.ForEach(it =>
            {
                response.ExchangeRates.Add(new ExchangeRateItem
                {
                    Currency = it.Currency.ToString(),
                    BuyRate = it.BuyRate,
                    SellRate = it.SellRate
                });
            });

            foreach (var currency in Enum.GetValues(typeof(Currencies)).Cast<Currencies>())
            {
                if (exhangeRates.Any(it => it.Currency == currency) == false)
                {
                    response.ExchangeRates.Add(new ExchangeRateItem
                    {
                        Currency = currency.ToString(),
                        BuyRate = 0,
                        SellRate = 0,
                    });
                }
            }

            return response;
        }

        [HttpPost("{currency}")]
        public async Task<IActionResult> SetExchangeRate(
            [FromRoute(Name = "currency")]string currency,
            [FromBody] SetExchangeRateCommand command)
        {
            var userId = User.FindFirst("UserId")?.Value;

            var currencyConverted = (Currencies)Enum.Parse(typeof(Currencies), currency);

            var existingRate = await _context.ExchangeRates.Where(it => it.ExchangeOfficerId == userId && it.Currency == currencyConverted).FirstOrDefaultAsync();

            if (existingRate != null)
            {
                existingRate.BuyRate = command.BuyRate;
                existingRate.SellRate = command.SellRate;
            }
            else
            {
                var newRate = new ExchangeRate
                {
                    Currency = currencyConverted,
                    BuyRate = command.BuyRate,
                    SellRate = command.SellRate,
                    ExchangeOfficerId = userId
                };

                _context.ExchangeRates.Add(newRate);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchExchangeRates(
            [FromQuery(Name = "currency")] string currency,
            [FromQuery(Name = "distance")] int? distance,
            [FromQuery(Name = "longitude")] double? longitude,
            [FromQuery(Name = "latitude")] double? latitude,
            [FromQuery(Name = "intention")] string intention)
        {
            intention = (intention ?? "buy").ToLower().Trim();
            intention = new[] { "buy", "sell" }.Contains(intention) ? intention : "buy";

            var userId = User.FindFirst("UserId")?.Value;
            var userLocation = GeometryExtensions.GeneratePoint(longitude.Value, latitude.Value, 4326);

            /// Spatial Data
            /// https://docs.microsoft.com/en-us/ef/core/modeling/spatial 
            /// SRID Ignored during client operations
            var query = await _context.ExchangeOfficers
                .Where(it => it.ExchangeRates.Any(er => er.Currency.ToString() == currency))
                .Include(it => it.ExchangeRates)
                .Select(it => new
                {
                    exchangeOfficer = it,
                    rate = intention == "buy" ?
                        it.ExchangeRates.FirstOrDefault(er => er.Currency.ToString() == currency).BuyRate :
                        it.ExchangeRates.FirstOrDefault(er => er.Currency.ToString() == currency).SellRate,
                    calculatedDistance = (int)Math.Ceiling(it.Location.ProjectTo(2855).Distance(userLocation.ProjectTo(2855)))
                })
                .ToListAsync();

            query = (intention == "buy" ? query.OrderBy(it => it.rate) : query.OrderByDescending(it => it.rate))
                .Where(it => distance == null || distance == 0 || it.calculatedDistance <= distance)
                .Take(10).ToList();
                        
            var response = new SearchExchangeRateResponse
            {
                Currency = currency,
                Items = query.Select(it =>
                    new SearchExchangeRateResponseItem
                    {
                        ExchangeOfficerIdentifier = it.exchangeOfficer.Id,
                        ExchangeOfficerName = it.exchangeOfficer.FullName,
                        ExchangeOfficerAddress = it.exchangeOfficer.Address,
                        ExchangeOfficerDistance = it.calculatedDistance,
                        Rate = it.rate,
                        RateType = intention
                    })
                .ToList()
            };

            return Ok(response);
        }
    }

    static class GeometryExtensions
    {
        static readonly IGeometryServices _geometryServices = NtsGeometryServices.Instance;

        static readonly ICoordinateSystemServices _coordinateSystemServices
            = new CoordinateSystemServices(
                new CoordinateSystemFactory(),
                new CoordinateTransformationFactory(),
                new Dictionary<int, string>
                {
                    // Coordinate systems:

                    // (3857 and 4326 included automatically)

                    // This coordinate system covers the area of our data.
                    // Different data requires a different coordinate system.
                    [2855] =
                    @"
                        PROJCS[""NAD83(HARN) / Washington North"",
                            GEOGCS[""NAD83(HARN)"",
                                DATUM[""NAD83_High_Accuracy_Regional_Network"",
                                    SPHEROID[""GRS 1980"",6378137,298.257222101,
                                        AUTHORITY[""EPSG"",""7019""]],
                                    AUTHORITY[""EPSG"",""6152""]],
                                PRIMEM[""Greenwich"",0,
                                    AUTHORITY[""EPSG"",""8901""]],
                                UNIT[""degree"",0.01745329251994328,
                                    AUTHORITY[""EPSG"",""9122""]],
                                AUTHORITY[""EPSG"",""4152""]],
                            PROJECTION[""Lambert_Conformal_Conic_2SP""],
                            PARAMETER[""standard_parallel_1"",48.73333333333333],
                            PARAMETER[""standard_parallel_2"",47.5],
                            PARAMETER[""latitude_of_origin"",47],
                            PARAMETER[""central_meridian"",-120.8333333333333],
                            PARAMETER[""false_easting"",500000],
                            PARAMETER[""false_northing"",0],
                            UNIT[""metre"",1,
                                AUTHORITY[""EPSG"",""9001""]],
                            AUTHORITY[""EPSG"",""2855""]]
                    "
                });

        public static IPoint GeneratePoint(double x, double y, int srid)
        {
            var geometryFactory = _geometryServices.CreateGeometryFactory(srid);

            return geometryFactory.CreatePoint(new Coordinate(x, y));
        }

        public static IGeometry ProjectTo(this IGeometry geometry, int srid)
        {
            var geometryFactory = _geometryServices.CreateGeometryFactory(srid);

            var transformation = _coordinateSystemServices.CreateTransformation(geometry.SRID, srid);

            return NetTopologySuite.CoordinateSystems.Transformations.GeometryTransform.TransformGeometry(geometryFactory, geometry, transformation.MathTransform);
        }
    }

}
