namespace Squares.Api.Models
{
    public class SquaresResponse
    {
        public PointsCollection PointsCollection { get; set; }

        public int NumberOfSquares {  get; set; }
        public IEnumerable<Point[]> SquaresCollection { get; set; }
    }
}
