using Microsoft.AspNetCore.Mvc;
using Squares.Api.Models;

namespace Squares.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {   
        private readonly ILogger<PointsController> _logger;

        public PointsController(ILogger<PointsController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Gets a list of available collections
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "points")]
        public IEnumerable<Points> Get()
        {
            // TODO: Implement
            return Enumerable.Range(1, 5).Select(index => new Points
            {
                CollectionId = Guid.NewGuid(),
                PointsCollection = new List<Point>
                {
                    new Point{
                    X = Random.Shared.Next(-20, 55),
                    Y = Random.Shared.Next(-20, 55)
                    }
                }
               
            })
            .ToArray();
        }

        /// <summary>
        /// FR - Import List of points.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPut(Name = "points")]
        public Points Put(Points points)
        {
            // TODO: Implement
            points.CollectionId =  Guid.NewGuid();
            return points;
        }

        /// <summary>
        /// FR - add point to existing list.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPut(Name = "points/{collectionId}")]
        public Points AddPoint(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new Points
            {
                CollectionId = collectionId,
                PointsCollection = new List<Point> { newPoint }
            };
            return points;
        }

        /// <summary>
        /// FR - delete point from existing list.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpDelete(Name = "points/{collectionId}/deletepoint")]
        public Points DeletePoint(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new Points
            {
                CollectionId = collectionId
            };
            return points;
        }

        /// <summary>
        /// FR - retrieve squares.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpGet(Name = "Points/{collectionId}/squares")]
        public Points GetSquares(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new Points
            {
                CollectionId = collectionId
            };
            return points;
        }
    }
}
