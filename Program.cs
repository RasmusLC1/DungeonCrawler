using System;
using System.Drawing;
using System.Windows.Forms;
using ZombieGame.Levels;
using ZombieGame.Entities;

public class main : Form {
    private KeyPress keypress = new KeyPress();
    private Movement movement = new Movement();
    private MousePos mouse;
    private MousePos MousePos;
    private LevelCreator levelCreator = new LevelCreator();
    private Shooting shooting = new Shooting();
    private ZombieSpawner zombiespawner = new ZombieSpawner();
    private HitDetection hitdetection = new HitDetection();
    private RayTracer raytracer = new RayTracer();
    int mouseX;
    int mouseY;
    private GameRenderer gameRenderer = new GameRenderer();
    private Bitmap backBuffer; // Back buffer for double buffering

    public main(){
        Text = "Zombie Game";
        Size = new Size(movement.getWidth, movement.getHeight);
        //Setting the backBuffer
        backBuffer = new Bitmap(movement.getWidth, movement.getHeight);
        
        // Subscribe to the Paint event
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

        levelCreator.LevelBuilder("level");
        zombiespawner.SpawnerList(levelCreator.getfields);
        Timer timer = new Timer();
        timer.Interval = 1000 / 60; // 60 FPS
        timer.Tick += Update;
        MousePos = new MousePos(this);

        // Subscribe to the MouseMoveEvent
        MousePos.MouseMoveEvent += MouseMove;
        timer.Start();

        UpdateStyles();
        Paint += ZombieGame_Paint;

        KeyDown += keypress.ZombieGame_KeyDown;
        KeyUp += keypress.ZombieGame_KeyUp;
    }
    private void MouseMove(object sender, MouseEventArgs e){
        mouseX = e.X; // X-coordinate of the mouse cursor
        mouseY = e.Y; // Y-coordinate of the mouse cursor
    }

    private void Update(object sender, EventArgs e) {
        GC.Collect(0);
        zombiespawner.zombieSpawner();
        movement.ZombieControl(zombiespawner, levelCreator);
        movement.PlayerControl(keypress, levelCreator.getPlayer, mouseX, mouseY, levelCreator.getfields);
        shooting.shooting(keypress, mouseX, mouseY, levelCreator.getPlayer);
        shooting.UpdateShots(movement);
        hitdetection.BulletHitZombie(zombiespawner, shooting);
        hitdetection.ZombieHitPlayer(levelCreator.getPlayer, zombiespawner);
        hitdetection.FieldDetection(levelCreator, levelCreator.getPlayer, zombiespawner, shooting, keypress);
        
        Invalidate(); // Triggers the Paint event
    }

    private void ZombieGame_Paint(object sender, PaintEventArgs e){
        // // Create a Label control to display the text
        // Label label = new Label();
        // label.Text = "Hi";
        // label.Location = new Point(10, 10); // Set the location of the Label
        // label.Size = new Size(100, 20); // Set the size of the Label

        // // BackColor is the textBox and ForeColor is the text
        // label.BackColor = Color.Transparent;
        // label.ForeColor = Color.Pink;
        // Controls.Add(label); // Add the Label to the form



        // Draw on the back buffer
        using (Graphics backBufferGraphics = Graphics.FromImage(backBuffer)){
            gameRenderer.DrawPlayer(backBufferGraphics, levelCreator.getPlayer, mouseX, mouseY);
            foreach (var row in levelCreator.getfields) {
                foreach (var field in row){
                    if (field.getwall){
                        gameRenderer.drawWall(backBufferGraphics, field.getX, field.getY, field.getsize, field.getsize);
                    } else if (field.gettreasure){
                        gameRenderer.drawTreasure(backBufferGraphics, field.getX, field.getY, field.getsize, field.getsize);
                    }
                }
            }
            foreach (var shot in shooting.shots) {
                gameRenderer.drawShot(backBufferGraphics, (int)shot.getX, (int)shot.getY);
            }
            foreach (var zombie in zombiespawner.getzombies) {
                gameRenderer.drawZombie(backBufferGraphics, (int)zombie.getX, (int)zombie.getY);
            }
        
        }

        // Draw the back buffer onto the form's graphics
        e.Graphics.DrawImage(backBuffer, 0, 0);
    }

    [STAThread]
    public static void Main() {
        Application.EnableVisualStyles();
        Application.Run(new main());
    }
}
