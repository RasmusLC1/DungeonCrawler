
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZombieGame.Entities;
using ZombieGame.Renderer;
namespace ZombieGame{
    public class Shooting{
        private bool firstShot = true;
        private Stopwatch shotTimer = new Stopwatch();
        public List<Shot> shots = new List<Shot>();
        private Coordinate coordinate = new Coordinate();
        private const int shotSpeed = 20;
        private const int shotgunCooldDown = 1000;
        private const int pistolCooldDown = 1000;
        private const int huntingRifleCooldDown = 1000;
        private Tuple<double, double> distance;
        double shotX;
        double shotY;
        double normalizedDirectionX;
        double normalizedDirectionY;
        public int getshotSpeed{
            get {return shotSpeed;}
        }


        public void shooting(KeyboardInput keypress, int mouseX, int mouseY, Player player){
            if (keypress.getspacePressed == true) {
                System.Console.WriteLine("TEST");
                    // Calculate the direction vector from player to mouse
                double directionX = mouseX - player.getX;
                double directionY = mouseY - player.getY;

                distance = coordinate.Direction(directionX, directionY);

                // Normalize the direction vector (make it a unit vector)
                normalizedDirectionX = distance.Item1;
                normalizedDirectionY = distance.Item2;

                // Calculate the shot's initial position
                shotX = player.getX;
                shotY = player.getY;

                // Add the shot with a fixed speed
                if ( player.getweapon == "pistol" && shotTimer.ElapsedMilliseconds >= pistolCooldDown
                    || player.getweapon == "pistol" && firstShot){
                    PistolShot(); 
                    firstShot = false;
                    shotTimer.Restart();
                } else if ( player.getweapon == "shotgun" && shotTimer.ElapsedMilliseconds >= shotgunCooldDown
                    || player.getweapon == "shotgun" && firstShot){
                    ShotGunShot(); 
                    firstShot = false;
                    shotTimer.Restart();
                    
                } else if ( player.getweapon == "huntingrifle" && shotTimer.ElapsedMilliseconds >= huntingRifleCooldDown
                            || player.getweapon == "huntingrifle" && firstShot){
                    HuntingRifleShot();
                    firstShot = false;
                    shotTimer.Restart();
                } 
            }
            
        }
        private void PistolShot(){
            shots.Add(new Pistol(shotX, shotY, 5, 5, normalizedDirectionX * shotSpeed,
                                normalizedDirectionY * shotSpeed));
        }
        private void HuntingRifleShot(){
            shots.Add(new HuntingRifle(shotX, shotY, 5, 5, normalizedDirectionX * shotSpeed,
                                normalizedDirectionY * shotSpeed));
        }
        private void ShotGunShot(){
            for (int i = -5; i <= 5; i+= 5){ 
                shots.Add(new ShotGun(shotX+i, shotY+i, 5, 5, normalizedDirectionX * shotSpeed,
                                normalizedDirectionY * shotSpeed));
            }
                    
        }
        
        public void UpdateShots(Movement movement) {
            for (int i = 0; i < shots.Count; i++){
                shots[i].updateXpos (shots[i].getX + (int)(shots[i].getDirectionX));
                shots[i].updateYpos (shots[i].getY + (int)(shots[i].getDirectionY));
                // Remove shots that have gone off the screen
                if (shots[i].CheckRange()){
                    shots.RemoveAt(i);
                    i--; // Decrement i to account for the removed shot
                } else if (shots[i].OutOfBounds()){
                    shots.RemoveAt(i);
                    i--;
                }
            }
        }
    }    
}