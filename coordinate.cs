using System;
public class Coordinate {
    private double normalizedDirectionX;
    private double normalizedDirectionY;
    public double DistanceTo(double x1, double x2, double y1, double y2) {
        double deltaX = x1 - x2;
        double deltaY = y1 - y2;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }
    /// <summary>
    /// Create a normalised vector for the direction
    /// </summary>
    /// <param name="directionX">Direction X axis</param>
    /// <param name="directionY">Direction Y axis</param>
    /// <returns></returns>
    public Tuple<double, double> Direction(double directionX, double directionY){
        double length = Math.Sqrt(Math.Abs(directionX) * Math.Abs(directionX) + Math.Abs(directionY) * Math.Abs(directionY));
        if (length == 0 || directionX == 0 || directionY == 0){
            // Normalize the direction vector (make it a unit vector)
        }
            normalizedDirectionX = directionX / length;
            normalizedDirectionY = directionY / length;
            return Tuple.Create(normalizedDirectionX, normalizedDirectionY);
    }
    public double GetAngleBetweenPoints(double x1, double y1, double x2, double y2) {
        return Math.Atan2(y2 - y1, x2 - x1);
    }
}