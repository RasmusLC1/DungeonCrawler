using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using ZombieGame.Entities;
using ZombieGame.Levels;
namespace ZombieGame{
    public class Movement {
        
        private const int Width = 800;
        private const int Height = 400;
        private const int movementSpeed = 5;
        private Stopwatch zombieTimer = new Stopwatch();
        private bool start = true;
        private Coordinate coordinate = new Coordinate();
        private ShortestPath shortestPath = new ShortestPath();
        private Tuple<double,double> updatedCoords;
        public Tuple<double, double> currentPosition;
        private Player player;
        private double testX;
        private double testY;
        private Zombie zombie;

        public int getWidth {
            get {return Width;}
        }
        public int getHeight {
            get {return Height;}
        }

        public void PlayerControl(KeyboardInput keypress, Player player){
            if (keypress.getwPressed && player.getY > 5){
                player.updateYpos(player.getY - movementSpeed); 
            }
            else if (keypress.getsPressed && player.getY < Height-player.getWidth){
                player.updateYpos(player.getY + movementSpeed); 
            } else if (keypress.getaPressed && player.getX > 5){
                player.updateXpos(player.getX - movementSpeed); 
            }
            else if (keypress.getdPressed && player.getX < Width-player.getWidth){
                player.updateXpos(player.getX + movementSpeed); 
            }
        }

        public void ZombieControl(ZombieSpawner zombiespawner, LevelCreator level){
            player = level.getPlayer;
                for (int i = 0; i < zombiespawner.getzombies.Count; i++){
                    zombie = zombiespawner.getzombies[i];
                    if (!(Math.Round(coordinate.DistanceTo(player.getX, zombie.getX, player.getY, zombie.getY)) < 10)){
                        updatedCoords = shortestPath.Pathfinder(level.getfields, zombie.getX/10, zombie.getY/10, player.getX/10, player.getY/10);
                        Tuple<double,double> distance = coordinate.Direction(updatedCoords.Item1*10-zombie.getX, updatedCoords.Item2*10-zombie.getY);
                        int normalizedDirectionX = (int) Math.Round(distance.Item1 * zombie.getzombieSpeed + zombie.getX);
                        int normalizedDirectionY = (int) Math.Round(distance.Item2 * zombie.getzombieSpeed + zombie.getY);
                        
                        zombie.SetPrevXY();
                        zombie.updateXpos(normalizedDirectionX);
                        zombie.updateYpos(normalizedDirectionY);
                    }
                    
                }
        }
    }
}