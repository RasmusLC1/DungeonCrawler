using System;
using System.Drawing;
using System.Windows.Forms;
using ZombieGame.Entities;
using ZombieGame.Levels;

public class RayTracer
{
    private Coordinate coordinate = new Coordinate();
    private bool wallHit = false;

    public void FieldOfView(LevelCreator levelCreator, double destinationX, double destinationY, Entity entity)
    {
        Player player = levelCreator.getPlayer;

        // Calculate the direction vector as a normalized vector pointing from the entity to the destination.
        double directionX = destinationX - entity.getX;
        double directionY = destinationY - entity.getY;
        double magnitude = Math.Sqrt(directionX * directionX + directionY * directionY);
        double normalizedDirectionX = directionX / magnitude;
        double normalizedDirectionY = directionY / magnitude;

        // entity is at the destination.
        if (Math.Abs(directionX) < 5 && Math.Abs(directionY) < 5)
        {
            return;
        }

        // Define the number of rays for FOV.
        int numRays = 30;
        double fovAngle = Math.PI / 3; // Change this value to adjust the field of view.

        // Calculate the angle increment between rays.
        double angleIncrement = fovAngle / (numRays - 1);

        for (int i = 0; i < numRays; i++)
        {
            // Calculate the angle for the current ray by spreading the direction.
            double angle = Math.Atan2(normalizedDirectionY, normalizedDirectionX) + (i - (numRays - 1) / 2) * angleIncrement;

            // Initialize the ray's position at the player's position.
            double rayX = entity.getX;
            double rayY = entity.getY;
            double rayDistance = 0;

            // Cast the ray
            while (rayDistance < 100)
            {
                // Update the ray's position.
                rayX += Math.Cos(angle);
                rayY += Math.Sin(angle);
                rayDistance += 1; // Increment the ray's distance, increase if laggy
                if (entity.GetType() == typeof(Zombie) && Math.Abs(rayX - player.getX) < 5 &&
                    Math.Abs(rayY - player.getY) < 5)
                {
                    Zombie zombie = (Zombie)entity;
                    zombie.SetActivateZombie(true);
                    return;
                }

                // Check if the ray hit a wall in the level.
                if (levelCreator.getfields[(int)rayX / 10][(int)rayY / 10].getwall)
                {
                    wallHit = true;

                    break; // Stop the ray when it hits a wall.
                }
            }

            // Ray drawing for troubleshooting, add graphics g to the method input
            // and add it to the ZombieGame_Paint event in the program to trigger

            // g.DrawLine(linePen, (float)entity.getX, (float)entity.getY, (float)rayX, (float)rayY);
        }
    }
}
