using Microsoft.VisualBasic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class MouseControl {
    public Vector2 Position { get; }
    MouseState? mouseState;
    public MouseState MousePos(GameWindow window){
        mouseState = window.MouseState;
        return mouseState;
    }
}