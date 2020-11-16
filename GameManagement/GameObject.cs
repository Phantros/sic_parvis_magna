using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq.Expressions;

public abstract class GameObject : IGameLoopObject
{
    protected GameObject parent;
    public Vector2 position, velocity;
    protected int layer;
    protected float mass = 1f;
    protected string id;
    protected bool visible;
    protected const int MAX_SPEED = 200000;

    public GameObject(int layer = 0, string id = "")
    {
        this.layer = layer;
        this.id = id;
        position = Vector2.Zero;
        velocity = Vector2.Zero; 
        visible = true;
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual void Reset()
    {
        visible = true;
    }

    public virtual Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public virtual Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public virtual Vector2 GlobalPosition
    {
        get
        {
            if (parent != null)
            {
                return parent.GlobalPosition + Position;
            }
            else
            {
                return Position;
            }
        }
    }

    public GameObject Root
    {
        get
        {
            if (parent != null)
            {
                return parent.Root;
            }
            else
            {
                return this;
            }
        }
    }

    public GameObjectList GameWorld
    {
        get
        {
            return Root as GameObjectList;
        }
    }

    public virtual int Layer
    {
        get { return layer; }
        set { layer = value; }
    }

    public virtual GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public string Id
    {
        get { return id; }
    }

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    public virtual Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
        }
    }

    public Vector2 Truncate(Vector2 vector, float length)
    {
        if (vector.LengthSquared() > length * length)
        {
            vector.Normalize();
            vector *= length;
        }
        return vector;
    }

    public void calculateTarget(Vector2 Target)
    {
        Vector2 Desired_velocity = Target - Position;

        Vector2 currentVelocity = Truncate(Desired_velocity, MAX_SPEED);
                currentVelocity = Desired_velocity - Velocity;
                currentVelocity /= mass;


                Velocity = Truncate(Velocity + currentVelocity, MAX_SPEED);
    }
}