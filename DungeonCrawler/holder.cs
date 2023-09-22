// using System;
// using OpenTK;
// using OpenTK.Graphics.OpenGL;
// using OpenTK.Mathematics;
// using OpenTK.Windowing.Common;
// using OpenTK.Windowing.Desktop;
// using System.Drawing;
// using System.Runtime.ExceptionServices;
// using System.Reflection.Metadata;
// using ZombieGame.Levels;
// using ZombieGame.Entities;

// public class Game : GameWindow{
//     private KeyPress keypress = new KeyPress();
//     private Movement movement = new Movement();
//     private MousePos mouse;
//     private MousePos MousePos;
//     private LevelCreator levelCreator = new LevelCreator();
//     private Shooting shooting = new Shooting();
//     private ZombieSpawner zombiespawner = new ZombieSpawner();
//     private HitDetection hitdetection = new HitDetection();
//     int mouseX;
//     int mouseY;
//     private GameRenderer gameRenderer = new GameRenderer();

//     public Game() : base(800, 600, GraphicsMode.Default, "Zombie Game")
//     {
//         VSync = VSyncMode.On; // Enable VSync
//     }

//     protected override void OnLoad(EventArgs e)
//     {
//         GL.ClearColor(Color4.Aqua); // Background color
//         GL.Enable(EnableCap.DepthTest);

//         levelCreator.LevelBuilder("level");
//         zombiespawner.SpawnerList(levelCreator.getfields);

//         MousePos = new MousePos(this);
//         MousePos.MouseMove += MouseMove;

//         Title = "Zombie Game";

//         KeyDown += OnKeyDown;
//         KeyUp += OnKeyUp;

//         base.OnLoad(e);
//     }

//     private void OnKeyDown(object sender, KeyboardKeyEventArgs e)
//     {
//         keypress.ZombieGame_KeyDown(sender, e);
//     }

//     private void OnKeyUp(object sender, KeyboardKeyEventArgs e)
//     {
//         keypress.ZombieGame_KeyUp(sender, e);
//     }

//     private void MouseMove(object sender, MouseMoveEventArgs e)
//     {
//         mouseX = e.X;
//         mouseY = e.Y;
//     }

//     protected override void OnUpdateFrame(FrameEventArgs e)
//     {
//         base.OnUpdateFrame(e);

//         movement.PlayerControl(keypress, levelCreator.getPlayer);
//         shooting.shooting(keypress, mouseX, mouseY, levelCreator.getPlayer);
//         shooting.UpdateShots(movement);
//         movement.ZombieControl(zombiespawner, levelCreator);
//         hitdetection.BulletHitZombie(zombiespawner, shooting);
//         hitdetection.ZombieHitPlayer(levelCreator.getPlayer, zombiespawner);
//         hitdetection.FieldDetection(levelCreator, levelCreator.getPlayer, zombiespawner, shooting, keypress);
//         zombiespawner.zombieSpawner();
//     }

//     protected override void OnRenderFrame(FrameEventArgs e)
//     {
//         base.OnRenderFrame(e);

//         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

//         // Render your game objects here using OpenGL calls
//         gameRenderer.DrawPlayer(levelCreator.getPlayer);
//         foreach (var row in levelCreator.getfields)
//         {
//             foreach (var field in row)
//             {
//                 if (field.wall)
//                 {
//                     gameRenderer.drawWall(field.getX, field.getY, field.getsize, field.getsize);
//                 }
//                 else if (field.treasure)
//                 {
//                     gameRenderer.drawTreasure(field.getX, field.getY, field.getsize, field.getsize);
//                 }
//             }
//         }
//         foreach (var shot in shooting.shots)
//         {
//             gameRenderer.drawShot((int)shot.getX, (int)shot.getY);
//         }
//         foreach (var zombie in zombiespawner.getzombies)
//         {
            // gameRenderer.drawZombie((int)zombie.getX, (int)zombie.getY);
//         }

//         SwapBuffers();
//     }

//     [STAThread]
//     public static void Main()
//     {
//         using (var game = new ZombieGame())
//         {
//             game.Run(60.0);
//         }
//     }
// }