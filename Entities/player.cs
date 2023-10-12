using System;
using System.Collections.Generic;

namespace ZombieGame.Entities{

    public class Player : Entity{
        private int HitPoints;
        private int PlayerSpeed;
        private string weapon = "shotgun";
        private const int playerWidth = 10;
        private double viewAngle;
        private double directionX;
        private double directionY;
        private double deltaX;
        private double deltaY;
        public int gethitPoints {
            get{return HitPoints;}
        }
        public string getweapon {
            get{return weapon;}
        }
        public int getplayerSpeed {
            get{return PlayerSpeed;}
        }
        public double GetViewAngle {
            get{return viewAngle;}
        }
        public double getdirectionX {
            get{return directionX;}
        }
        public double getdirectionY {
            get{return directionY;}
        }
        public double getdeltaX {
            get{return deltaX;}
        }
        public double getdeltaY {
            get{return deltaY;}
        }

        public Player(double x, double y, int width, int height, int hitPoints, int playerSpeed) : base(x, y, width, height) {
            HitPoints = hitPoints;
            PlayerSpeed = playerSpeed;
        }
        public void Hit(){
            HitPoints--;
        }
        public void SetDirection(double x, double y){
            directionX = x;
            directionY = y;
        }
        public double getRotation(){
            // Calculate the rotation angle based on directionX and directionY
            return Math.Atan2(directionY - getY, directionX - getX);
        }
        public void updatePlayerAngle(double rotation){
            viewAngle += rotation;
        }
        public void updateDeltaX(double updatedDeltaX){
            deltaX = updatedDeltaX;
        }
        public void updateDeltaY(double updatedDeltaY){
            deltaY = updatedDeltaY;
        }

        public void updateWeapoon(string newWeapon) {
            switch (newWeapon){
                case "pistol":
                    weapon = "pistol";
                    break;
                case "shotgun":
                    weapon = "shotgun";
                    break;
                case "huntingrifle":
                    weapon = "huntingrifle";
                    break;
                default:
                    break;
            }
        }
    }
}