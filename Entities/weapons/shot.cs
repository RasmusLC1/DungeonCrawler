using ZombieGame.Entities;

public class Shot : Entity {
    private double DirectionX;
    private double DirectionY;
    private double startX;
    private double startY;
    private int Range;
    private int Damage;
    private Movement movement = new Movement();
    private Coordinate coordinate = new Coordinate();
    public double getDirectionX {
        get{return DirectionX;}
    }
    public double getDirectionY {
        get{return DirectionY;}
    }
    public double getstartX {
        get{return startX;}
    }
    public double getstartY {
        get{return startY;}
    }
    public double getRange {
        get{return startX;}
    }
    public int getDamage {
        get{return Damage;}
    }

    public Shot(double x, double y, int width, int height, double directionX,
    double directionY, int range, int damage) :
        base(x, y, width, height) {
        DirectionX = directionX;
        DirectionY = directionY;
        startX = x;
        startY = y;
        Range = range;
        Damage = damage;
    }
    public bool CheckRange(){
        if (coordinate.DistanceTo(startX, (int)getX, startY, (int)getY) > Range){
            return true;
        } else{
            return false;
        }
    }
    public bool OutOfBounds(){
        if (getX > movement.getWidth || getY > movement.getHeight ||
                getY < 0 || getX < 0){
            return true;
        } else{
            return false;
        }
    }

}