using ZombieGame.Entities;

public class ShotGun : Shot {
    private const int Damage = 2;
    private const int Range = 100;
    public ShotGun(double x, double y, int width, int height, double directionX, double directionY
        ) : base(x, y, width, height, directionX, directionY, Range, Damage) {
    }
}