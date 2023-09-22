using ZombieGame.Entities;

public class HuntingRifle : Shot {
    private const int Damage = 3;
    private const int Range = 400;
    private const int Recharge = 1000;
    public HuntingRifle(double x, double y, int width, int height, double directionX, double directionY
        ) : base(x, y, width, height, directionX, directionY, Range, Damage) {
    }
}