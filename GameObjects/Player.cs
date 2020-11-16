using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace sic_parvis_magna.GameObjects
{
    class Player : SpriteGameObject
    {
        Vector2 startPullPos, actualPullPos, maxPullDistance, actualPullDistance, pullForce;
        public Player(Vector2 startPosition) : base ("spr_player")
        {
            Origin = Center;
            this.position = startPosition;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            Position = inputHelper.MousePosition;
            Visible = false;

            if (inputHelper.MouseLeftButtonDown())
            {
                Visible = true;
                startPullPos = inputHelper.MousePosition;
                maxPullDistance = startPullPos + new Vector2(50, 50);
            }

            if (!inputHelper.MouseLeftButtonDown())
            {
                actualPullPos = inputHelper.MousePosition;
                actualPullDistance = actualPullPos - startPullPos;
                pullForce = (actualPullDistance / maxPullDistance * 100);
                Console.WriteLine(pullForce);
            }

            base.HandleInput(inputHelper);
        }
    }
}
