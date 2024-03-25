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
        private readonly SquaresDbContext _dbContext;

        public PointsCollectionController(SquaresDbContext context, ISquareRetriever squareCounter)
        {
            _squareCounter = squareCounter;
            _dbContext = context;
        }

        /// <summary>
        /// Gets a list of available collections
        /// </summary>
        [HttpGet()]
        public async Task<IEnumerable<PointsCollection>> Get()
        {
            var pointsCollection = await _dbContext.PointsCollection.ToListAsync();
            return pointsCollection;
        }

        /// <summary>
        /// FR - Import List of points.
        /// </summary>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(PointsCollection))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            _dbContext.PointsCollection.Add(points);
            _dbContext.SaveChanges();

            return Ok(points);
        }

        /// <summary>
        /// Gets point collection.
        /// </summary>
        [HttpGet("{collectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PointsCollection))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPointsCollection(Guid collectionId)
        {
            var collection = await GetPointsCollectionById(collectionId);

            if (collection is null)
            {
                return NotFound();
            }
            return Ok(collection);
        }

        /// <summary>
        /// FR - add point to existing list.
        /// </summary>
        [HttpPut("{collectionId}/points")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PointsCollection))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPoint(Guid collectionId, Point newPoint)
        {
            var collection = await GetPointsCollectionById(collectionId);

            if (collection is null)
            {
                return NotFound();
            }

            collection.Points.Add(newPoint);
            _dbContext.SaveChanges();

            return Ok(collection);
        }

        /// <summary>
        /// FR - delete point from existing list.
        /// </summary>
        [HttpDelete("{collectionId}/points")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PointsCollection))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <IActionResult> DeletePoint(Guid collectionId, Point pointToDelete)
        {
            var collection = await GetPointsCollectionById(collectionId);

            if (collection is null)
            {
                return NotFound();
            }

            var pointToDeleteInDb = collection.Points.Where(p => p.XCoordinate == pointToDelete.XCoordinate && p.YCoordinate == pointToDelete.YCoordinate).FirstOrDefault();

            if (pointToDeleteInDb is null)
            {
                return NotFound();
            }
            collection.Points.Remove(pointToDeleteInDb);
            _dbContext.SaveChanges();

            return Ok(collection);
        }

        /// <summary>
        /// FR - retrieve squares.
        /// </summary>
        [HttpGet("{collectionId}/squares")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SquaresResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSquares(Guid collectionId)
        {
            var collection = await GetPointsCollectionById(collectionId);
            if (collection is null)
            {
                return NotFound();
            }

            var pointsArray = collection.Points.ToArray();            

            var squares = _squareCounter.GetSquares(pointsArray);

            SquaresResponse response = new SquaresResponse()
            {
                PointsCollection = collection,
                NumberOfSquares = squares.Count(),
                SquaresCollection = squares
            };

            return Ok(response);
        }

        private async Task<PointsCollection?> GetPointsCollectionById(Guid collectionId)
        {
            var pointsCollection = await _dbContext.PointsCollection.Include(collection => collection.Points).Where(collection => collection.PointsCollectionId == collectionId).ToListAsync();
            return pointsCollection.FirstOrDefault();
        }    
    }
}
