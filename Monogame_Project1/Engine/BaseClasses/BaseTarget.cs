using Monogame_Project1.Engine.GameObjects;
using System;

namespace Monogame_Project1.Engine.BaseClasses;

public abstract class BaseTarget : GameObject
{
    #region Fields

    private TargetMovement _movement;
    private Game1 _game;
    
    private bool _hit;

    protected Texture2D[] _animationTextures;
    protected float _animationSpeed = 0.05f;

    private float _time;
    private int _textureToShow = 0;
    #endregion

    #region Properties
    public bool Hit 
    { 
        get => _hit;
        set => _hit = value;
    }

    public TargetMovement MovementSystem
    {
        get => _movement;
        set => _movement = value;
    }

    #endregion
    
    
    #region Constructors

    public BaseTarget(Texture2D pTexture, Game1 pGame) : base(pTexture)
    {
        _game = pGame;
    }

    #endregion

    #region Public Voids

    public virtual void OnHit() 
    {       
        _hit = true;
    }

    public override void Update(GameTime pGameTime)
    {     
         _movement.Update(pGameTime);

        base.Update(pGameTime);
    }   

    #endregion
}
