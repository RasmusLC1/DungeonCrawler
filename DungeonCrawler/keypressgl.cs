// using OpenTK.Input;

// public class KeyPress
// {
//     private bool wPressed = false;
//     private bool aPressed = false;
//     private bool sPressed = false;
//     private bool dPressed = false;
//     private bool spacePressed = false;

//     public bool getwPressed
//     {
//         get { return wPressed; }
//     }

//     public bool getsPressed
//     {
//         get { return sPressed; }
//     }

//     public bool getaPressed
//     {
//         get { return aPressed; }
//     }

//     public bool getdPressed
//     {
//         get { return dPressed; }
//     }

//     public bool getspacePressed
//     {
//         get { return spacePressed; }
//     }

//     internal void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
//     {
//         switch (e.Key)
//         {
//             case Key.W:
//                 wPressed = true;
//                 break;
//             case Key.S:
//                 sPressed = true;
//                 break;
//             case Key.A:
//                 aPressed = true;
//                 break;
//             case Key.D:
//                 dPressed = true;
//                 break;
//             case Key.Space:
//                 spacePressed = true;
//                 break;
//         }
//     }

//     internal void HandleKeyUp(object sender, KeyboardKeyEventArgs e)
//     {
//         switch (e.Key)
//         {
//             case Key.W:
//                 wPressed = false;
//                 break;
//             case Key.S:
//                 sPressed = false;
//                 break;
//             case Key.A:
//                 aPressed = false;
//                 break;
//             case Key.D:
//                 dPressed = false;
//                 break;
//             case Key.Space:
//                 spacePressed = false;
//                 break;
//         }
//     }
// }
