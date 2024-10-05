using System;

namespace Monogame_Project1.Engine.BaseClasses;

public abstract class GameObject
{
    #region Fields
    protected float layer;
    protected Vector2 position;
    protected Vector2 origin;
    protected float rotation;
    protected Texture2D texture;
    protected Color color;
    protected bool isActive;
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

    public bool IsActive { get => isActive; set => isActive = value; }
    #endregion

    #region Constructor

    protected GameObject(Texture2D pTexture, bool pActive = true)
    {
        texture = pTexture;
        origin = new(texture.Width / 2, texture.Height / 2);
        color = Color.White;
        isActive = pActive;
    }

    protected GameObject(bool pActive = true)
    {
        isActive = pActive;
    }

    #endregion

    #region Public Methods

    public virtual void LoadContent(ContentManager pContent) {}
    public virtual void LateLoad() {}
    public virtual void Update(GameTime pGameTime) {}
    public virtual void Draw(SpriteBatch pSpriteBatch)
    {
        if (texture != null)
            pSpriteBatch.Draw(texture, position, null, color, rotation, origin, 1f, SpriteEffects.None, layer);
    }
    public void DeactivateObject(GameObject pObject)
    {
        pObject.IsActive = false;
    }

    #endregion
}


