namespace Squares.Api.Models
{
    public class PointsCollection
    {
        public Guid CollectionId { get; set; }
        public List<Point> Points { get; set; }
    }
}
