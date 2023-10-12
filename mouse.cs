using System;
using System.Windows.Forms;

public class MousePos {
    public event EventHandler<MouseEventArgs> MouseMoveEvent;

    public MousePos(Control controlToTrack) {
        controlToTrack.MouseMove += ControlToTrack_MouseMove;
    }

    private void ControlToTrack_MouseMove(object sender, MouseEventArgs e) {
        OnMouseMoveEvent(e);
    }

    protected virtual void OnMouseMoveEvent(MouseEventArgs e) {
        MouseMoveEvent?.Invoke(this, e);
    }
}