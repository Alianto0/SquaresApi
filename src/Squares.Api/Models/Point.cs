using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Squares.Api.Models
{
    [Table("Point")]
    public class Point
    {
        public Point(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

        [JsonIgnore]
        [Key]
        public int PointId { get; set; }
        [JsonIgnore]
        public Guid PointsCollectionId { get; set; }
       
        public int XCoordinate { get; set; }
       
        public int YCoordinate { get; set; }        
    }
}
