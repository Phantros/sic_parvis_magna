using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sic_parvis_magna.GameStates;

namespace sic_parvis_magna
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class sic_parvis_magna : GameEnvironment
    {
        public sic_parvis_magna()
        {
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Screen = new Point(1600, 900);
            ApplyResolutionSettings();

            GameStateManager.AddGameState("Play", new PlayingState());
            GameStateManager.SwitchTo("Play");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}
