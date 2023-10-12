namespace ZombieGame.Entities{

public class Entity {
    private double X;
    private double Y;
    private int Width;
    private int Height;
    public double getX {
        get{return X;}
    }
    public double getY {
        get{return Y;}
    }
    public int getWidth {
        get{return Width;}
    }
    public int getHeight{
        get{return Height;}
    }

    public Entity(double x, double y, int width, int height) {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
    public void updateXpos(double newPos){
        X = newPos;
    }
    public void updateYpos(double newPos){
        Y = newPos;
    }
}
}