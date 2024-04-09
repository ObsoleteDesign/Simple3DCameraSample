using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Simple3DCameraSample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        Grid grid;
        Camera camera;
        BasicEffect basicEffect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            basicEffect = new BasicEffect(GraphicsDevice);
            grid = new Grid(GraphicsDevice);
            //creating a camera that is 'above' the grid, looking at a point at the same Y-level, above the origin
            camera = new Camera(new Vector3(-50, 25, -50),
                Quaternion.CreateFromYawPitchRoll(MathHelper.ToRadians(225), 0, 0),
                GraphicsDevice.DisplayMode.AspectRatio);
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            Input.Update();

            camera.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            camera.ApplyTo(basicEffect);
            foreach (var pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                grid.Draw(GraphicsDevice);
            }
            
        }
    }
}
