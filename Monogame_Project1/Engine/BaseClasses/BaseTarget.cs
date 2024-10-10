namespace Monogame_Project1.Engine.BaseClasses;

public abstract class BaseTarget : GameObject
{
    #region Fields

    private TargetMovement _movement;
    
    #endregion

    #region Properties

    public TargetMovement MovementSystem
    {
        get => _movement;
        set => _movement = value;
    }

    #endregion
    
    
    #region Constructors

    public BaseTarget(Texture2D pTexture) : base(pTexture)
    {
    }

    #endregion

    #region Public Voids
    public virtual void OnHit() {}

    public override void Update(GameTime pGameTime)
    {
        _movement.Update(pGameTime);
        base.Update(pGameTime);
    }

    #endregion
}
