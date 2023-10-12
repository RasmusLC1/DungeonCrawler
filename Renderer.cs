using System;
using System.Drawing;
using System.Diagnostics;
using ZombieGame.Levels;
using ZombieGame.Entities;
using System.Collections.Generic;

public class GameRenderer {
    private Coordinate coordinate = new Coordinate();
    private Stopwatch FrameUpdater = new Stopwatch();
    private List< Tuple<double, double>> visibleTiles; 
    private Movement movement = new Movement();
    double angleDifference;
    double rayAngle;
    double rayX3D;
    double rayY3D;
    double rayDistance;
    double wallHeight;
    double wallTop;
    double wallLeft;
    double distancePlayertoWall;
    public void Render(Graphics g, LevelCreator level, Shooting shooting, ZombieSpawner zombiespawner, KeyPress keypress, int mouseX, int mouseY){

        Player player = level.getPlayer;
        g.FillRectangle(Brushes.Black, 0, 0, movement.getWidth, movement.getHeight);
        if (keypress.getpPressed){
            FieldOfViewPlayer3D(g, level, zombiespawner);
        } else{
            DrawPlayer(g, player);
            FieldOfViewPlayer(g, level, zombiespawner, mouseX, mouseY, player);
        }
        //Shots drawing
        foreach (var shot in shooting.shots) {
            drawShot(g, (int)shot.getX, (int)shot.getY);
        }
        //Zombie drawing
        
}
    private void DrawPlayer(Graphics g, Player player) {
        // Calculate the angle between the player's position and the mouse position.
        double angle = Math.Atan2(player.getdirectionY - player.getY, player.getdirectionX - player.getX);
        
        // Convert the angle from radians to degrees.
        float angleDegrees = (float)(angle * 180 / Math.PI);

        // Create a rotation transformation to rotate the player.
        g.TranslateTransform((float)player.getX, (float)player.getY);
        g.RotateTransform(angleDegrees);

        // Draw the rotated player character.
        g.FillRectangle(Brushes.White, -player.getWidth / 2, -player.getWidth / 2, player.getWidth, player.getWidth);

        // Reset the graphics transformation to avoid affecting other drawings.
        g.ResetTransform();

    }
    private void drawShot(Graphics g, int x, int y){
        g.FillEllipse(Brushes.White, x, y, 5, 5);
    }
    private void drawZombie(Graphics g, int x, int y){
        g.FillRectangle(Brushes.Green, x, y, 10, 10);
    }

    private void drawWall(Graphics g, int x, int y, int width, int height){
        g.FillRectangle(Brushes.Brown, x, y, width, height);
    }
    private void drawTreasure(Graphics g, int x, int y, int width){
        g.FillRectangle(Brushes.Gold, x, y, width, width);
        
    }
    public void FieldOfViewPlayer3D(Graphics g, LevelCreator levelCreator, ZombieSpawner zombiespawner) {
        Player player = levelCreator.getPlayer;

        // get direction vector
        double directionX = player.getdirectionX - player.getX;
        double directionY = player.getdirectionY - player.getY;

        // Normalize the direction vector to have a unit length
        Tuple<double, double> direction = coordinate.Direction(directionX, directionY);

        // Define the number of rays for FOV
        int numRays = 80;

        //Distance that the player can see
        int distance = 800;
        double fovAngle = Math.PI/3; // Change this value to adjust the field of view

        // Calculate the angle increment between rays
        double angleIncrement = fovAngle / (numRays - 1);

        for (int i = 0; i < numRays; i++) {
            // Calculate the angle for the current ray by spreading the direction
            rayAngle = Math.Atan2(direction.Item2, direction.Item1) + (i - (numRays - 1) / 2) * angleIncrement;

            // Initialize the ray's position at the player's position
            rayX3D = player.getX;
            rayY3D = player.getY;
            
            rayDistance = 0;
            // Wall width
            int wallWidth = 10;

            // Cast the front ray
            while (rayDistance < distance) {
                // Update the ray's position
                rayX3D += Math.Cos(rayAngle) * 0.1; 
                rayY3D += Math.Sin(rayAngle) * 0.1; 
                rayDistance += 0.1;

                int fieldRow = (int)(rayX3D / 10);
                int fieldColumn = (int)(rayY3D / 10);
                    // Check if the ray hit a wall
                    if (levelCreator.getfields[fieldRow][fieldColumn].getwall) {
                        Calculate3DObject(g, player, numRays, i);
                        // Draw the "3D" wall rectangle on the screen.
                        g.FillRectangle(Brushes.Gray, (float)wallLeft, (float)wallTop, (float)wallWidth, (float)wallHeight);
                        break;
                    // } else if (levelCreator.getfields[fieldRow][fieldColumn].getenemy){
                    //     Calculate3DObject(g, player, numRays, i);
                    //     // Draw the "3D" wall rectangle on the screen.
                    //     g.FillRectangle(Brushes.Green, (float)wallLeft, (float)wallTop, (float)wallWidth, (float)wallHeight);
                    // break; // Stop the ray when it hits a wall
                    // } else if (levelCreator.getfields[fieldRow][fieldColumn].gettreasure){
                    //     Calculate3DObject(g, player, numRays, i);
                    //     // Draw the "3D" wall rectangle on the screen.
                    //     g.FillRectangle(Brushes.Gold, (float)wallLeft, (float)wallTop, (float)wallWidth, (float)wallHeight);
                    //     break; // Stop the ray when it hits a wall
                    } 
            }
        }

    }
    public void Calculate3DObject(Graphics g, Player player, int numRays, int i){
        // Calculate the angle difference between the ray and the player's view direction
        angleDifference = rayAngle - coordinate.GetAngleBetweenPoints(player.getX, player.getY, rayX3D, rayY3D);
        // Calculate the correct wall height based on the distance.
        wallHeight = g.VisibleClipBounds.Height / (rayDistance * Math.Cos(angleDifference))*10;

        // Calculate the screen coordinates for drawing the wall rectangle
        wallTop = g.VisibleClipBounds.Height / 2 - wallHeight / 2;
        wallLeft = i * (g.VisibleClipBounds.Width / numRays);
    }

    public void FieldOfViewPlayer(Graphics g, LevelCreator levelCreator, ZombieSpawner zombiespawner, double destionationX, double destionationY,
                            Entity entity) {
        Player player = levelCreator.getPlayer;

        // get direction vector
        double directionX = destionationX - entity.getX;
        double directionY = destionationY - entity.getY;

        // entity is at destination
        if (Math.Abs(directionX) < 5 && Math.Abs(directionY) < 5){
            return;
        }

        // Normalize the direction vector to have a unit length
        Tuple<double, double> direction = coordinate.Direction(directionX, directionY);

        // Define the number of rays for FOV
        int numRays = 400;

        //Distance that the player can see
        int distance = 200;
        double fovAngle = Math.PI; // Change this value to adjust the field of view

        // Calculate the angle increment between rays
        double angleIncrement = fovAngle / (numRays - 1);

        for (int i = 0; i < numRays; i++) {
            // Calculate the angle for the current ray by spreading the direction
            double angle = Math.Atan2(direction.Item2, direction.Item1) + (i - (numRays - 1) / 2) * angleIncrement;

            // Initialize the ray's position at the player's position
            double rayXFront = entity.getX;
            double rayYFront = entity.getY;
            double rayXBack = entity.getX;
            double rayYBack = entity.getY;
            double rayDistance = 0;
            Field field = levelCreator.getfields[0][0];

            // Cast the front ray
            while (rayDistance < distance) {
                // Update the ray's position
                rayXFront += Math.Cos(angle);
                rayYFront += Math.Sin(angle);
                rayDistance += 1;
                int fieldRow = (int)rayXFront / 10;
                int fieldColumn =(int)rayYFront / 10;
                int xPos = levelCreator.getfields[fieldRow][fieldColumn].getX;
                int yPos = levelCreator.getfields[fieldRow][fieldColumn].getY;

                // Check if the ray hit a wall in the level
                if (levelCreator.getfields[fieldRow][fieldColumn].getwall){
                    drawWall(g, xPos, yPos, field.getsize, field.getsize);
                    break; // Stop the ray when it hits a wall
                } else if (levelCreator.getfields[fieldRow][fieldColumn].getenemy){
                    drawZombie(g, xPos, yPos);
                    break; // Stop the ray when it hits a wall
                } else if (levelCreator.getfields[fieldRow][fieldColumn].gettreasure){
                    drawTreasure(g, xPos, yPos, field.getsize);
                    break; // Stop the ray when it hits a wall
                } 
            }
            rayDistance = 0;
            // Cast the backwards rays
            while (rayDistance < distance) {
                rayXBack -= Math.Cos(angle);
                rayYBack -= Math.Sin(angle);
                rayDistance += 1; // Increment the ray's distance
                int fieldRow = (int)rayXBack / 10;
                int fieldColumn =(int)rayYBack / 10;
                int xPos = levelCreator.getfields[fieldRow][fieldColumn].getX;
                int yPos = levelCreator.getfields[fieldRow][fieldColumn].getY;

                // Check if the ray hit a wall in the level
                if (levelCreator.getfields[fieldRow][fieldColumn].getwall){
                    drawWall(g, xPos, yPos, field.getsize, field.getsize);
                    break; // Stop the ray when it hits a wall
                } else if (levelCreator.getfields[fieldRow][fieldColumn].getenemy){
                    drawZombie(g, xPos, yPos);
                    break; // Stop the ray when it hits a wall
                } else if (levelCreator.getfields[fieldRow][fieldColumn].gettreasure){
                    drawTreasure(g, xPos, yPos, field.getsize);
                    break; // Stop the ray when it hits a wall
                } 
            }
            // visibleTiles.Add(Tuple.Create(rayXFront, rayYFront));
            // visibleTiles.Add(Tuple.Create(rayXBack, rayYBack));

            // Ray drawing for troubleshooting, add graphics g to the method input
            //and add it to the ZombieGame_Paint event in program to trigger
            // Pen linePen = new Pen(Brushes.Aqua);
            // g.DrawLine(linePen, (float)entity.getX, (float)entity.getY, (float)rayXFront, (float)rayYFront);
            // g.DrawLine(linePen, (float)entity.getX, (float)entity.getY, (float)rayXBack, (float)rayYBack);
        }
        // foreach (Zombie zombie in zombiespawner.getzombies){
        //     if (visibleTiles.Contains(Tuple.Create(zombie.getX, zombie.getY))){
        //         drawZombie(g, (int)zombie.getX, (int)zombie.getY);
        //     }
        // }
        // visibleTiles.Clear();
    }
}
