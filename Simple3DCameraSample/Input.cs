using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Simple3DCameraSample
{
    internal class Input
    {
        static MouseState mouseState;
        static KeyboardState keyboardState;
        static MouseState mousePrev;
        static KeyboardState keyboardPrev;

        public static void Update()
        {
            mousePrev = mouseState;
            keyboardPrev = keyboardState;
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        public static Vector2 mousePos => new Vector2(mouseState.Position.X, mouseState.Position.Y);

        public static Vector2 mouseMove => new Vector2(mouseState.Position.X - mousePrev.Position.X, mouseState.Position.Y - mousePrev.Position.Y);

        public static bool isKeyDown(Keys key) => keyboardState.IsKeyDown(key);
    }
}
