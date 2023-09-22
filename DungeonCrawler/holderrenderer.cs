// using OpenTK;
// using OpenTK.Graphics.OpenGL;
// using ZombieGame.Entities;

// public class GameRenderer
// {
//     public void DrawPlayer(Player player){
//         // Set the color and draw a white rectangle for the player
//         GL.Color3(1.0f, 1.0f, 1.0f);
//         GL.Begin(PrimitiveType.Quads);
//         GL.Vertex2(player.getX, player.getY);
//         GL.Vertex2(player.getX + player.getWidth, player.getY);
//         GL.Vertex2(player.getX + player.getWidth, player.getY + player.getWidth);
//         GL.Vertex2(player.getX, player.getY + player.getWidth);
//         GL.End();
//     }

//     public void DrawShot(int x, int y) {
//         // Set the color and draw a white ellipse for the shot
//         GL.Color3(1.0f, 1.0f, 1.0f);
//         GL.Begin(PrimitiveType.Polygon);
//         for (int i = 0; i < 360; i++)
//         {
//             double angle = i * System.Math.PI / 180.0;
//             double xx = x + 2.5 * System.Math.Cos(angle);
//             double yy = y + 2.5 * System.Math.Sin(angle);
//             GL.Vertex2(xx, yy);
//         }
//         GL.End();
//     }

//     public void DrawZombie(int x, int y)
//     {
//         // Set the color and draw a green rectangle for the zombie
//         GL.Color3(0.0f, 1.0f, 0.0f);
//         GL.Begin(PrimitiveType.Quads);
//         GL.Vertex2(x, y);
//         GL.Vertex2(x + 10, y);
//         GL.Vertex2(x + 10, y + 10);
//         GL.Vertex2(x, y + 10);
//         GL.End();
//     }

//     public void DrawWall(int x, int y, int width, int height)
//     {
//         // Set the color and draw a brown rectangle for the wall
//         GL.Color3(0.5f, 0.2f, 0.0f);
//         GL.Begin(PrimitiveType.Quads);
//         GL.Vertex2(x, y);
//         GL.Vertex2(x + width, y);
//         GL.Vertex2(x + width, y + height);
//         GL.Vertex2(x, y + height);
//         GL.End();
//     }

//     public void DrawTreasure(int x, int y, int width, int height)
//     {
//         // Set the color and draw a gold rectangle for the treasure
//         GL.Color3(1.0f, 0.84f, 0.0f);
//         GL.Begin(PrimitiveType.Quads);
//         GL.Vertex2(x, y);
//         GL.Vertex2(x + width, y);
//         GL.Vertex2(x + width, y + height);
//         GL.Vertex2(x, y + height);
//         GL.End();
//     }
// }
