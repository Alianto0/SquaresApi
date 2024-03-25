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
        [HttpGet(Name = "collection")]
        public IEnumerable<PointsCollection> Get()
        {
            // TODO: Implement
            return Enumerable.Range(1, 5).Select(index => new PointsCollection
            {
                CollectionId = Guid.NewGuid(),
                Points = new List<Point>
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
        [HttpPut(Name = "collection")]
        public PointsCollection Put(PointsCollection points)
        {
            // TODO: Implement
            points.CollectionId =  Guid.NewGuid();
            return points;
        }

        /// <summary>
        /// Gets point collection.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpGet(Name = "collection/{collectionId}")]
        public PointsCollection DeletePoint(Guid collectionId)
        {
            // TODO: Implement
            var points = new PointsCollection
            {
                CollectionId = collectionId
            };
            return points;
        }

        /// <summary>
        /// FR - add point to existing list.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPut(Name = "collection/{collectionId}")]
        public PointsCollection AddPoint(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new PointsCollection
            {
                CollectionId = collectionId,
                Points = new List<Point> { newPoint }
            };
            return points;
        }



        /// <summary>
        /// FR - delete point from existing list.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpDelete(Name = "collection/{collectionId}/points")]
        public PointsCollection DeletePoint(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new PointsCollection
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
        [HttpGet(Name = "collection/{collectionId}/squares")]
        public PointsCollection GetSquares(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new PointsCollection
            {
                CollectionId = collectionId
            };
            return points;
        }
    }
}
