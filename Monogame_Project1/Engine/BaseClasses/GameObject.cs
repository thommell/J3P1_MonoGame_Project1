using System;

namespace Monogame_Project1.Engine.BaseClasses;

public class GameObject : Component
{
    #region Fields
    protected float layer;
    protected Vector2 position;
    protected Vector2 origin;
    protected float rotation;
    protected Texture2D texture;
    protected Color color;
    #endregion

    #region Properties
    public float Layer
    {
        get => layer;
        set => layer = value;
    }
    public Vector2 Position
    {
        get => position;
        set => position = value;
    }
    public Vector2 Origin
    {
        get => origin;
        set => origin = value;
    }
    public float Rotation
    {
        get => rotation;
        set => rotation = value;
    }
    public Texture2D Texture
    {
        get => texture;
        set => texture = value;
    }
    public Color Color
    {
        get => color;
        set => color = value;
    }
    
    public Rectangle Rectangle
    {
        get
        {
            if (texture != null)
                return new((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, texture.Width, texture.Height);
    
            throw new Exception("Texture not found/invalid.");
        }
    }
    #endregion

    #region Constructor

    public GameObject(Texture2D pTexture)
    {
        texture = pTexture;
        origin = new(texture.Width / 2, texture.Height / 2);
        color = Color.White;
    }

    public GameObject() {}

    #endregion

    #region Public Methods

    public override void Update(GameTime pGameTime) {}
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        if (texture != null)
            pSpriteBatch.Draw(texture, position, null, color, rotation, origin, 1f, SpriteEffects.None, layer);
    }

    #endregion
}


