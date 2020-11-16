using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class SpriteGameObject : GameObject
{
    public enum CollisionResult { NONE, LEFT, RIGHT, TOP, BOTTOM };

    protected Color shade = Color.White;
    protected SpriteSheet sprite;
    protected Vector2 origin;
    protected float scale = 1f;
    public bool PerPixelCollisionDetection = true;

    public SpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0)
        : base(layer, id)
    {
        if (assetName != "")
        {
            sprite = new SpriteSheet(assetName, sheetIndex);
        }
        else
        {
            sprite = null;
        }
    }    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
        {
            return;
        }
        spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, shade, 0, Origin, scale, SpriteEffects.None, 0);
    }

    public SpriteSheet Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sprite.Width;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }

    /// <summary>
    /// Returns / sets the scale of the sprite.
    /// </summary>
    public float Scale {
        get { return scale; }
        set { scale = value;  }
    }

    /// <summary>
    /// Set the shade the sprite will be drawn in.
    /// </summary>
    public Color Shade {
        get { return shade; }
        set { shade = value; }
    }

    public bool Mirror
    {
        get { return sprite.Mirror; }
        set { sprite.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public bool CollidesWith(SpriteGameObject obj)
    {
        if (!visible || !obj.visible || !BoundingBox.Intersects(obj.BoundingBox))
        {
            return false;
        }
        if (!PerPixelCollisionDetection)
        {
            return true;
        }
        if (this == obj)
        {
            return false;
        }
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
        {
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                if (sprite.IsTranslucent(thisx, thisy) && obj.sprite.IsTranslucent(objx, objy))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public CollisionResult CollisionSide(SpriteGameObject other)
    {
        float distanceToLeftEdge = GlobalPosition.X - Origin.X + Width - other.GlobalPosition.X - other.Origin.X;
        float distanceToRightEdge = other.GlobalPosition.X - other.Origin.X + other.Width - GlobalPosition.X - Origin.X;
        float xOverlap = distanceToLeftEdge < 0 || distanceToRightEdge < 0 ? 0 :
            distanceToLeftEdge <= distanceToRightEdge ? Math.Abs(distanceToLeftEdge) : Math.Abs(distanceToRightEdge);

        float distanceToTopEdge = GlobalPosition.Y - Origin.Y + Height - other.GlobalPosition.Y - other.Origin.Y;
        float distanceToBottomEdge = other.GlobalPosition.Y - other.Origin.Y + other.Height - GlobalPosition.Y - Origin.Y;
        float yOverlap = distanceToTopEdge < 0 || distanceToBottomEdge < 0 ? 0 :
            distanceToTopEdge <= distanceToBottomEdge ? Math.Abs(distanceToTopEdge) : Math.Abs(distanceToBottomEdge);

        if (xOverlap < yOverlap)
        {
            if (GlobalPosition.X - Origin.X < other.GlobalPosition.X - other.Origin.X)
                return CollisionResult.RIGHT;
            else
                return CollisionResult.LEFT;
        }
        else
        {
            if (GlobalPosition.Y - Origin.Y < other.GlobalPosition.Y - other.Origin.Y)
                return CollisionResult.BOTTOM;
            else
                return CollisionResult.TOP;
        }
    }
}

