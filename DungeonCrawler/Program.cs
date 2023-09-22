using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using ZombieGame.Renderer;

namespace ZombieGame{
    class Program{
        static void Main(string[] args){
            using (Game game = new Game()){
                game.Run();
            }
        }
    }
}