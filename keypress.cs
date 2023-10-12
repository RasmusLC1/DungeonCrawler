using System;
using System.Drawing;
using System.Windows.Forms;

public class KeyPress{
    public bool wPressed = false;
    private bool aPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;
    private bool spacePressed = false;
    
    
    public bool getwPressed
    {
        get { return wPressed; }
    }

    public bool getsPressed
    {
        get { return sPressed; }
    }

    public bool getaPressed
    {
        get { return aPressed; }
    }

    public bool getdPressed
    {
        get { return dPressed; }
    }

    public bool getspacePressed
    {
        get { return spacePressed; }
    }

    internal void ZombieGame_KeyDown(object sender, KeyEventArgs e){
        if (e.KeyCode == Keys.W){
            wPressed = true;
        }
        else if (e.KeyCode == Keys.S) {
            sPressed = true;
        }
        else if (e.KeyCode == Keys.A) {
            aPressed = true;
        }
        else if (e.KeyCode == Keys.D) {
            dPressed = true;
        } else if (e.KeyCode == Keys.Space) {
            spacePressed = true;
        }
    }

    internal void ZombieGame_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.W){
            wPressed = false;
        }
        else if (e.KeyCode == Keys.S) {
            sPressed = false;
        }
        else if (e.KeyCode == Keys.A) {
            aPressed = false;
        }
        else if (e.KeyCode == Keys.D) {
            dPressed = false;
        } else if (e.KeyCode == Keys.Space) {
            spacePressed = false;
        }
    }
}    