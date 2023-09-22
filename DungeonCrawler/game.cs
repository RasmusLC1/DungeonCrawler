using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using ZombieGame.Renderer;
using ZombieGame.Levels;
using ZombieGame.Entities;

namespace ZombieGame{

    public class Game : GameWindow{
        private GraphicsRenderer renderer = new GraphicsRenderer();

        private KeyboardInput keyboardInput = new KeyboardInput();
        private MouseControl mousectrl = new MouseControl();
        private Vector2 position;
        private Movement movement = new Movement();
        private LevelCreator levelCreator = new LevelCreator();
        private Shooting shooting = new Shooting();
        private ZombieSpawner zombiespawner = new ZombieSpawner();
        private HitDetection hitdetection = new HitDetection();
        public Game(int width = 1280, int height = 768, string title = "Game") : base(GameWindowSettings.Default, new NativeWindowSettings(){
            Title = title,
            Size = new Vector2i(width, height),
            WindowBorder = WindowBorder.Fixed,
            StartVisible = false,
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            APIVersion = new Version(3, 3)
        }){
            VSync = VSyncMode.On;
            this.CenterWindow();
        }
        /// <summary>
        /// Responsible for resizing the window
        /// </summary>
        /// <param name="e">the resize window event</param>
        protected override void OnResize(ResizeEventArgs e){
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }
        /// <summary>
        /// Upon loading up the window this is run once
        /// </summary>
        protected override void OnLoad() {
            renderer.Setup(this);
            levelCreator.LevelBuilder("level");
            zombiespawner.SpawnerList(levelCreator.getfields);
            base.OnLoad();
        }
        /// <summary>
        /// Upon Unloading the window this is run once
        /// </summary>
        protected override void OnUnload() {
            renderer.OnUnload();
            base.OnUnload();
        }
        /// <summary>
        /// Updater that runs every time the game updates
        /// </summary>
        /// <param name="args">framerate based update argument</param>
        protected override void OnUpdateFrame(FrameEventArgs args) {

        // Control the frame rate
        renderer.Draw(this);
        //Control update
        keyboardInput.HandleKeyDown(this);
        keyboardInput.HandleKeyUp(this);
        position = mousectrl.MousePos(this).Position;

        //Game update
        movement.PlayerControl(keyboardInput, levelCreator.getPlayer);
        shooting.shooting(keyboardInput, (int)position[0], (int)position[1], levelCreator.getPlayer);
        shooting.UpdateShots(movement);
        movement.ZombieControl(zombiespawner, levelCreator);
        hitdetection.BulletHitZombie(zombiespawner, shooting);
        hitdetection.ZombieHitPlayer(levelCreator.getPlayer, zombiespawner);
        hitdetection.FieldDetection(levelCreator, levelCreator.getPlayer, zombiespawner, shooting, keyboardInput);
        zombiespawner.zombieSpawner();
        base.OnUpdateFrame(args);
    }
        /// <summary>
        /// Updater that is responsible for rendering visuals when the game updates
        /// </summary>
        /// <param name="args"></param>
        protected override void OnRenderFrame(FrameEventArgs args){
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            renderer.FrameRenderer(args, this);
            base.OnRenderFrame(args);
            SwapBuffers();
        }

        
    }
}
