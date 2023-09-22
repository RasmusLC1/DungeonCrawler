using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;



namespace ZombieGame{
    public class KeyboardInput {
        private bool wPressed = false;
        private bool aPressed = false;
        private bool sPressed = false;
        private bool dPressed = false;
        private bool spacePressed = false;

        public bool getwPressed {
            get { return wPressed; }
        }
        public bool getsPressed {
            get { return sPressed; }
        }

        public bool getaPressed {
            get { return aPressed; }
        }

        public bool getdPressed {
            get { return dPressed; }
        }

        public bool getspacePressed {
            get { return spacePressed; }
        }

        internal void HandleKeyDown(GameWindow window) {
            if (window.KeyboardState.IsKeyDown(Keys.W)){
                wPressed = true;
            } else if (window.KeyboardState.IsKeyDown(Keys.S)){
                sPressed = true;
            }else if (window.KeyboardState.IsKeyDown(Keys.A)){
                aPressed = true;
            }else if (window.KeyboardState.IsKeyDown(Keys.D)){
                dPressed = true;
            }else if (window.KeyboardState.IsKeyDown(Keys.Space)){
                spacePressed = true;
            }
        }

        internal void HandleKeyUp(GameWindow window) {
            if (window.KeyboardState.IsKeyReleased(Keys.W)){
                wPressed = false;
            } else if (window.KeyboardState.IsKeyReleased(Keys.S)){
                sPressed = false;
            } else if (window.KeyboardState.IsKeyReleased(Keys.A)){
                aPressed = false;
            } else if (window.KeyboardState.IsKeyReleased(Keys.D)){
                dPressed = false;
            } else if (window.KeyboardState.IsKeyReleased(Keys.Space)){
                spacePressed = false;
            }
        }
    }
}
