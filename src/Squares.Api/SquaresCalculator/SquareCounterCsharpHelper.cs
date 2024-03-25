using System.Drawing;
using Point = Squares.Api.Models.Point;

namespace Squares.Api.SquaresCalculator
{
    public class SquareCounterCsharpHelper : ISquareRetriever
    {
        public IEnumerable<Point[]> GetSquares(Point[] input)
        {
            var mapToDrawingPoints = new System.Drawing.Point[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                mapToDrawingPoints[i] = new System.Drawing.Point(input[i].XCoordinate, input[i].YCoordinate);
            }
            var squaresIndexes = GetSquares(mapToDrawingPoints);

            var listSquares = new List<Point[]>();

            foreach (var index in squaresIndexes) {
                listSquares.Add(new Point[]
                {
                    input[index[0]],
                    input[index[1]],
                    input[index[2]],
                    input[index[3]]
                });
            }
            return listSquares;
        }

        private new List<int[]> GetSquares(System.Drawing.Point[] input)
        {
            var solutions = new List<int[]>();
            for (int i = 0; i < input.Length - 3; i++)
            {
                for (int j = i + 1; j < input.Length - 2; j++)
                {
                    for (int k = j + 1; k < input.Length - 1; k++)
                    {
                        for (int m = k + 1; m < input.Length; m++)
                        {
                            // See if these points make a square.
                            int[] square_points =
                                GetSquarePoints(i, j, k, m, input);
                            if (square_points != null)
                            {
                                solutions.Add(square_points);
                            }
                        }
                    }
                }
            }

            return solutions;
        }


        private int[] GetSquarePoints(int i, int j, int k, int m, System.Drawing.Point[] input)
        {
            // A small value for equality testing.
            const double tiny = 0.001;

            // Store all but the first index in an array.
            int[] indexes = { j, k, m };

            // Get the distances from point i to the others.
            float[] distances =
            {
        PointFDistance(input[i], input[j]),
        PointFDistance(input[i], input[k]),
        PointFDistance(input[i], input[m]),
    };

            // Sort the distances and the corresponding indexes.
            Array.Sort(distances, indexes);

            // If the two smaller distances are not roughly
            // the same (the side length), then this isn't a square.
            if (Math.Abs(distances[0] - distances[1]) > tiny) return null;

            // If the longer distance isn't roughly Sqr(2) times the
            // side length (the diagonal length), then this isn't a square.
            float diagonal_length = (float)(Math.Sqrt(2) * distances[0]);
            if (Math.Abs(distances[2] - diagonal_length) > tiny)
                return null;

            // See if the distance between the farther point and
            // the two closer points is roughly the side length.
            float distance1 =
                PointFDistance(input[indexes[2]], input[indexes[0]]);
            if (Math.Abs(distances[0] - distance1) > tiny) return null;
            float distance2 =
                PointFDistance(input[indexes[2]], input[indexes[1]]);
            if (Math.Abs(distances[0] - distance2) > tiny) return null;

            // It's a square!
            return new int[] { i, indexes[0], indexes[2], indexes[1] };
        }

        // Return the distance between two PointFs.
        private float PointFDistance(PointF point1, PointF point2)
        {
            float dx = point1.X - point2.X;
            float dy = point1.Y - point2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

    }
}
