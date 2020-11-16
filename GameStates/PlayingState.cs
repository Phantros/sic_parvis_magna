using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using sic_parvis_magna.GameObjects;

namespace sic_parvis_magna.GameStates
{
    class PlayingState : GameObjectList
    {
        private Player thePlayer;

        public PlayingState()
        {
            this.Add(new SpriteGameObject("spr_background"));

            thePlayer = new Player(new Vector2(800, 450));
            this.Add(thePlayer);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


    }
}
