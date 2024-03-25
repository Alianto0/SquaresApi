namespace Squares.Api.Models
{
    public class PointsCollection
    {
        public Guid PointsCollectionId { get; set; }
        public List<Point> Points { get; set; } = new List<Point>();
    }
}
