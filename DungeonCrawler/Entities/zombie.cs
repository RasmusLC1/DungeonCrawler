using System.Security.Cryptography.X509Certificates;

namespace ZombieGame.Entities{

public class Zombie : Entity{
    private int HitPoints;
    private int ZombieSpeed;
    private double prevX;
    private double prevY;
    public int gethitPoints {
        get{return HitPoints;}
    }
    public int getzombieSpeed {
        get{return ZombieSpeed;}
    }
    public double getprevX {
        get{return prevX;}
    }
    public double getprevY {
        get{return prevY;}
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
    public void SetPrevXY(){
        prevX = getX;
        prevY = getY;
    }
}
}