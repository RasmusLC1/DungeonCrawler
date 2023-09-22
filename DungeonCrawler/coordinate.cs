using System;
public class Coordinate {
    public double DistanceTo(double x1, double x2, double y1, double y2) {
        double deltaX = x1 - x2;
        double deltaY = y1 - y2;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }
    public Tuple<double, double> Direction(double directionX, double directionY){
        double length = Math.Sqrt(Math.Abs(directionX) * Math.Abs(directionX) + Math.Abs(directionY) * Math.Abs(directionY));

        // Normalize the direction vector (make it a unit vector)
        double normalizedDirectionX = directionX / length;
        double normalizedDirectionY = directionY / length;
        return Tuple.Create(normalizedDirectionX, normalizedDirectionY);
    }
}