using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Squares.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetPointsController : ControllerBase
    {   

        private readonly ILogger<GetPointsController> _logger;

        public GetPointsController(ILogger<GetPointsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Points")]
        public IEnumerable<Point> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Point
            {
                X = Random.Shared.Next(-20, 55),
                Y = Random.Shared.Next(-20,55)
            })
            .ToArray();
        }
    }
}
