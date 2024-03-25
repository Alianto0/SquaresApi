using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squares.Api.Data;
using Squares.Api.Models;
using Squares.Api.SquaresCalculator;

namespace Squares.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsCollectionController : ControllerBase
    {
        private readonly ISquareRetriever _squareCounter;
        private readonly SquaresDbContext _DbContext;

        public PointsCollectionController(SquaresDbContext context, ISquareRetriever squareCounter)
        {
            _squareCounter = squareCounter;
            _DbContext = context;
        }

        /// <summary>
        /// Gets a list of available collections
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IEnumerable<PointsCollection>> Get()
        {
            var pointsCollection = await _DbContext.PointsCollection.ToListAsync();
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
        [HttpPut("{collectionId}/points")]
        public async Task<PointsCollection> AddPoint(Guid collectionId, Point newPoint)
        {
            var collection = await GetPointsCollectionById(collectionId);

            collection.Points.Add(newPoint);
            _DbContext.SaveChanges();

            return collection;
        }

        /// <summary>
        /// FR - delete point from existing list.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpDelete("{collectionId}/points")]
        public async Task <IActionResult> DeletePoint(Guid collectionId, Point pointToDelete)
        {
            var collection = await GetPointsCollectionById(collectionId);
            var pointToDeleteInDb = collection.Points.Where(p => p.XCoordinate == pointToDelete.XCoordinate && p.YCoordinate == pointToDelete.YCoordinate).FirstOrDefault();

            if (pointToDeleteInDb is null)
            {
                return NotFound();
            }
            collection.Points.Remove(pointToDeleteInDb);
            _DbContext.SaveChanges();

            return Ok(collection);
        }

        /// <summary>
        /// FR - retrieve squares.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpGet("{collectionId}/squares")]
        public async Task<SquaresResponse> GetSquares(Guid collectionId)
        {
            var collection = await GetPointsCollectionById(collectionId);

            var pointsArray = collection.Points.ToArray();

            var squares = _squareCounter.GetSquares(pointsArray);

            SquaresResponse response = new SquaresResponse()
            {
                PointsCollection = collection,
                NumberOfSquares = squares.Count(),
                SquaresCollection = squares
            };

            return response;
        }

        private async Task<PointsCollection> GetPointsCollectionById(Guid collectionId)
        {
            var pointsCollection = await _DbContext.PointsCollection.Include(collection => collection.Points).Where(collection => collection.PointsCollectionId == collectionId).ToListAsync();
            return pointsCollection.FirstOrDefault();
        }    
    }
}
