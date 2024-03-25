using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squares.Api.Data;
using Squares.Api.Models;

namespace Squares.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsCollectionController : ControllerBase
    {   
        private readonly ILogger<PointsCollectionController> _logger;

        private readonly SquaresDbContext _DbContext;
      

        public PointsCollectionController(SquaresDbContext context, ILogger<PointsCollectionController> logger)
        {
            _logger = logger;
            _DbContext = context;
        }
        /// <summary>
        /// Gets a list of available collections
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IEnumerable<PointsCollection>> Get()
        {
            
            var pointsCollection = await _DbContext.PointsCollection.Include(collection=>collection.Points).ToListAsync();
            return pointsCollection;
        }

        /// <summary>
        /// FR - Import List of points.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPut("collection")]
        public PointsCollection Put(PointsCollection points)
        {
            // TODO: Implement
            points.PointsCollectionId =  Guid.NewGuid();
            return points;
        }

        /// <summary>
        /// Gets point collection.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpGet("collection/{collectionId}")]
        public PointsCollection DeletePoint(Guid collectionId)
        {
            // TODO: Implement
            var points = new PointsCollection
            {
                PointsCollectionId = collectionId
            };
            return points;
        }

        /// <summary>
        /// FR - add point to existing list.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPut("collection/{collectionId}")]
        public PointsCollection AddPoint(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new PointsCollection
            {
                PointsCollectionId = collectionId,
                Points = new List<Point> { newPoint }
            };
            return points;
        }



        /// <summary>
        /// FR - delete point from existing list.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpDelete("collection/{collectionId}/points")]
        public PointsCollection DeletePoint(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new PointsCollection
            {
                PointsCollectionId = collectionId
            };
            return points;
        }

        /// <summary>
        /// FR - retrieve squares.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpGet("collection/{collectionId}/squares")]
        public PointsCollection GetSquares(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new PointsCollection
            {
                PointsCollectionId = collectionId
            };
            return points;
        }
    }
}
