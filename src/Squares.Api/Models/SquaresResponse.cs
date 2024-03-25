namespace Squares.Api.Models
{
    public class SquaresResponse
    {
        public PointsCollection PointsCollection { get; set; }

        public IEnumerable<Point[]> SquaresCollection { get; set; }
    }
}
