using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZombieGame.Entities;
using ZombieGame.Levels;

public class Movement {
    
    private const int Width = 800;
    private const int Height = 400;
    private const int movementSpeed = 5;
    private Stopwatch zombieTimer = new Stopwatch();
    private Coordinate coordinate = new Coordinate();
    private ShortestPath shortestPath = new ShortestPath();
    private Tuple<double,double> updatedCoords;
    public Tuple<double, double> currentPosition;
    private Player player;
    private Zombie zombie;
    private Tuple<double, double> distance;
    private Random random = new Random();
    private RayTracer rayTracer = new RayTracer();
    Field startField = new Field(150, 150); // Change the coordinates to match the starting point.
    Field finishField = new Field(200, 200); // Change the coordinates to match the finishing point.
    int normalizedDirectionX;
    int normalizedDirectionY;

    public int getWidth {
        get {return Width;}
    }
    public int getHeight {
        get {return Height;}
    }


    public void PlayerControl(KeyPress keypress, Player player, int mouseX, int mouseY, List<List<Field>> field){
        if (keypress.getwPressed){
            // player.updateXpos(player.getX+player.getdeltaX);
            // player.updateYpos(player.getY+player.getdeltaY);
            normalizedDirectionX = (int)(player.getX + player.getdeltaX);
            normalizedDirectionY = (int)(player.getY + player.getdeltaY);
        } else if (keypress.getsPressed){
            // player.updateXpos(player.getX-player.getdeltaX);
            // player.updateYpos(player.getY-player.getdeltaY);
            normalizedDirectionX = (int)(player.getX - player.getdeltaX);
            normalizedDirectionY = (int)(player.getY - player.getdeltaY);
        }
        if (keypress.getaPressed){
            player.updatePlayerAngle(-0.1);
            if (player.GetViewAngle < 0){
                player.updatePlayerAngle(2*Math.PI);
            }
            player.updateDeltaX(Math.Cos(player.GetViewAngle)*player.getplayerSpeed);
            player.updateDeltaY(Math.Sin(player.GetViewAngle)*player.getplayerSpeed);
        } else if (keypress.getdPressed){
            player.updatePlayerAngle(0.1);
            if (player.GetViewAngle > 2*Math.PI){
                player.updatePlayerAngle(-2*Math.PI);
            }
            player.updateDeltaX(Math.Cos(player.GetViewAngle)*player.getplayerSpeed);
            player.updateDeltaY(Math.Sin(player.GetViewAngle)*player.getplayerSpeed);
        }
        int x =  normalizedDirectionX/10;
        int y = normalizedDirectionY/10;
        if (!field[x][y].getwall &&
            !field[x + 1][y + 1].getwall &&
            !field[x - 1][y + 1].getwall &&
            !field[x + 1][y - 1].getwall){
            player.updateXpos(normalizedDirectionX);
            player.updateYpos(normalizedDirectionY);
        }
        // //Set direction for raycasting
            double destinationX =  player.getX + player.getdeltaX;
            double destinationY =  player.getY + player.getdeltaY;
            player.SetDirection(destinationX, destinationY);

        //Mouse based movement:
        // double directionX = mouseX - player.getX;
        // double directionY = mouseY - player.getY;

        // //Check if mouse is on player
        // if (Math.Abs(directionX) < 5 && Math.Abs(directionY) < 5){
        //     return;
        // }
        // distance = coordinate.Direction(directionX, directionY);
        // // Normalize the direction vector (make it a unit vector)
        // if (keypress.getwPressed ){
        //     normalizedDirectionX = (int)(player.getX + distance.Item1*player.getplayerSpeed);
        //     normalizedDirectionY = (int)(player.getY + distance.Item2*player.getplayerSpeed);
        // } else if (keypress.getsPressed){
        //     normalizedDirectionX = (int)(player.getX - distance.Item1*player.getplayerSpeed);
        //     normalizedDirectionY = (int)(player.getY - distance.Item2*player.getplayerSpeed);
        // } else if (keypress.getaPressed){
        //     normalizedDirectionX = (int)(player.getX + distance.Item2*player.getplayerSpeed);
        //     normalizedDirectionY = (int)(player.getY + distance.Item1*player.getplayerSpeed);
        // }else if (keypress.getdPressed){
        //     normalizedDirectionX = (int)(player.getX - distance.Item2*player.getplayerSpeed);
        //     normalizedDirectionY = (int)(player.getY - distance.Item1*player.getplayerSpeed);
        // }
        
        // if (!field[normalizedDirectionX/10][normalizedDirectionY/10].getwall
        //     && !field[normalizedDirectionX/10+1][normalizedDirectionY/10 + 1].getwall
        //     && !field[normalizedDirectionX/10 - 1][normalizedDirectionY/10 - 1].getwall){
        //     player.updateXpos(normalizedDirectionX);
        //     player.updateYpos(normalizedDirectionY);

        //     //Set direction for raycasting
        //     double destinationX = distance.Item1 * 10 + player.getX;
        //     double destinationY = distance.Item2 * 10 + player.getY;
        //     player.SetDirection(destinationX, destinationY);
        // }


        // Old movement system:

        // if (keypress.getwPressed ){
        //     player.updateYpos(player.getY - movementSpeed); 
        // }
        // else if (keypress.getsPressed && player.getY < Height-player.getWidth){
        //     player.updateYpos(player.getY + movementSpeed); 
        // } else if (keypress.getaPressed && player.getX > 5){
        //     player.updateXpos(player.getX - movementSpeed); 
        // }
        // else if (keypress.getdPressed && player.getX < Width-player.getWidth){
        //     player.updateXpos(player.getX + movementSpeed); 
        // }
    }

    public void ZombieControl(ZombieSpawner zombiespawner, LevelCreator level){
        player = level.getPlayer;

        for (int i = 0; i < zombiespawner.getzombies.Count; i++){
            zombie = zombiespawner.getzombies[i];
            startField.UpdateX((int)zombie.getX / 10);
            startField.UpdateY((int)zombie.getY / 10);
            finishField.UpdateX((int)player.getX / 10);
            finishField.UpdateY((int)player.getY / 10);

            startField.SetDistance(finishField.getX, finishField.getY);
            //Check if zombie is at same position as player
            if (startField.getdistance > 0){
                
                //Raycast zombie's field of view
                rayTracer.FieldOfView(level, zombie.getdirectionX, zombie.getdirectionY, zombie);

                //Activate zombie and set aggro timer
                if (zombie.getactivateZombie){
                    if (zombieTimer.ElapsedMilliseconds == 0){
                        zombieTimer.Start();
                    }
                //Reset timer when aggro timer reaches 4 seconds
                    if (zombieTimer.ElapsedMilliseconds > 4000){
                        zombieTimer.Reset();
                        zombie.SetActivateZombie(false);
                    }
                    //Pathfinding for zombies
                    updatedCoords = shortestPath.Pathfinder(level.getfields, startField, finishField);
                    Tuple<double,double> distance = coordinate.Direction(updatedCoords.Item1*10-zombie.getX, updatedCoords.Item2*10-zombie.getY);

                    //Get the new coordinates
                    int normalizedDirectionX = (int) Math.Round(distance.Item1 * zombie.getzombieSpeed + zombie.getX);
                    int normalizedDirectionY = (int) Math.Round(distance.Item2 * zombie.getzombieSpeed + zombie.getY);
                    level.getfields[(int)zombie.getX/10][(int)zombie.getY/10].updateEnemy(false);
                    zombie.updateXpos(normalizedDirectionX);
                    zombie.updateYpos(normalizedDirectionY);
                    level.getfields[(int)zombie.getX/10][(int)zombie.getY/10].updateEnemy(true);
                    //Set direction for raycasting
                    double destinationX = distance.Item1 * 10 + zombie.getX;
                    double destinationY = distance.Item2 * 10 + zombie.getY;
                    zombie.SetDirection(destinationX, destinationY);
                }
            }                 
        }
    }
}