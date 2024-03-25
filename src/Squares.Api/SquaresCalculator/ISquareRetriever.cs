using Squares.Api.Models;

namespace Squares.Api.SquaresCalculator
{
    public interface ISquareRetriever
    {
        public IEnumerable<Point[]> GetSquares(Point[] input);
    }
}
