using System;
using System.Drawing;
using System.Windows.Forms;
using ZombieGame.Levels;
using ZombieGame.Entities;

public class GameRenderer {

    public void DrawPlayer(Graphics g, Player player, int mouseX, int mouseY) {
        g.FillRectangle(Brushes.Black, 0, 0, 800, 400);
        // Calculate the angle between the player's position and the mouse position.
        double angle = Math.Atan2(mouseY - player.getY, mouseX - player.getX);
        
        // Convert the angle from radians to degrees.
        float angleDegrees = (float)(angle * 180 / Math.PI);

        // Create a rotation transformation to rotate the player.
        g.TranslateTransform((float)player.getX, (float)player.getY);
        g.RotateTransform(angleDegrees);

        // Draw the rotated player character.
        g.FillRectangle(Brushes.White, -player.getWidth / 2, -player.getWidth / 2, player.getWidth, player.getWidth);

        // Reset the graphics transformation to avoid affecting other drawings.
        g.ResetTransform();
    }
    public void drawShot(Graphics g, int x, int y){
        g.FillEllipse(Brushes.White, x, y, 5, 5);
    }
    public void drawZombie(Graphics g, int x, int y){
        g.FillRectangle(Brushes.Green, x, y, 10, 10);
    }

    public void drawWall(Graphics g, int x, int y, int width, int height){
        g.FillRectangle(Brushes.Brown, x, y, width, height);
    }
    public void drawTreasure(Graphics g, int x, int y, int width, int height){
        g.FillRectangle(Brushes.Gold, x, y, width, height);
        
    }
}
