namespace Squares.Api.Models
{
    public class Points
    {
        public Guid CollectionId { get; set; }
        public List<Point> PointsCollection { get; set; }
    }
}
