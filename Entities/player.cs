namespace ZombieGame.Entities{

public class Player : Entity{
    internal double DirectionX;
    internal double DirectionY;
    private int HitPoints;
    private int PlayerSpeed;
    private string weapon = "shotgun";
    private const int playerWidth = 10;
    private double viewAngle;
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

    public Player(double x, double y, int width, int height, int hitPoints, int playerSpeed,
             double directionX, double directionY) : base(x, y, width, height) {
        HitPoints = hitPoints;
        PlayerSpeed = playerSpeed;
        DirectionX = directionX;
        DirectionY = directionY;
    }
    public void Hit(){
        HitPoints--;
    }
    public void SetViewAngle(double angle) {
        viewAngle = angle;
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