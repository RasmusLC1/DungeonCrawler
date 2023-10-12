using System.Security.Cryptography.X509Certificates;

namespace ZombieGame.Entities{

public class Zombie : Entity{
    private int HitPoints;
    private int ZombieSpeed;
    private double directionX;
    private double directionY;
    private bool startRoaming = true;
    private bool activateZombie = false;
    public int gethitPoints {
        get{return HitPoints;}
    }    public int getzombieSpeed {
        get{return ZombieSpeed;}
    }
    public double getdirectionX {
        get{return directionX;}
    }
    public double getdirectionY {
        get{return directionY;}
    }
    public bool getactivateZombie {
        get{return activateZombie;}
    }
    public bool getstartRoaming {
        get{return startRoaming;}
    }

    public Zombie(double x, double y, int width, int height, int hitPoints,
                int zombieSpeed) :
                base(x, y, width, height) {
        HitPoints = hitPoints;
        ZombieSpeed = zombieSpeed;
        height = 10;
    }
    public void Hit(){
        HitPoints--;
    }
    public void SetDirection(double x, double y){
        directionX = x;
        directionY = y;
    }
    public void SetActivateZombie(bool status){
        activateZombie = status;
    }
    public void SetstartRoaming(bool status){
        startRoaming = status;
    }
}
}