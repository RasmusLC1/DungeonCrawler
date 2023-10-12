using System.Windows.Forms;
using ZombieGame.Entities;

public class Pistol : Shot {
    private const int Damage = 1;
    private const int Range = 100;
    public Pistol(double x, double y, int width, int height, double directionX, double directionY
        ) : base(x, y, width, height, directionX, directionY, Range, Damage) {
    }
}