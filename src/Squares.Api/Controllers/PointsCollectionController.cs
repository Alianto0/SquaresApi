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
            var pointsCollection = await _DbContext.PointsCollection.Include(collection => collection.Points).ToListAsync();
            return pointsCollection;
        }

        /// <summary>
        /// FR - Import List of points.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> Put(PointsCollection points)
        {
            if (points.PointsCollectionId == Guid.Empty)
            {
                points.PointsCollectionId = Guid.NewGuid();
            }

            var existingCollection = await GetPointsCollectionById(points.PointsCollectionId);

            if(existingCollection is not null)
            {
                return BadRequest();
            }

            _DbContext.PointsCollection.Add(points);
            _DbContext.SaveChanges();

            return Ok(points);
        }

        /// <summary>
        /// Gets point collection.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpGet("{collectionId}")]
        public async Task<PointsCollection> GetPointsCollection(Guid collectionId)
        {            
            return await GetPointsCollectionById(collectionId);
        }

        /// <summary>
        /// FR - add point to existing list.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPut("{collectionId}")]
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
        [HttpDelete("{collectionId}/points")]
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
        [HttpGet("{collectionId}/squares")]
        public PointsCollection GetSquares(Guid collectionId, Point newPoint)
        {
            // TODO: Implement
            var points = new PointsCollection
            {
                PointsCollectionId = collectionId
            };
            return points;
        }

        private async Task<PointsCollection> GetPointsCollectionById(Guid collectionId)
        {
            var pointsCollection = await _DbContext.PointsCollection.Include(collection => collection.Points).Where(collection => collection.PointsCollectionId == collectionId).ToListAsync();
            return pointsCollection.FirstOrDefault();
        }    
    }
}
