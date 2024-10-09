namespace Monogame_Project1.Engine.BaseClasses;

public abstract class BaseTarget : GameObject
{
    #region Fields
    
    
    
    #endregion
    
    #region Constructors

    public BaseTarget(Texture2D pTexture) : base(pTexture) { }

    #endregion

    #region Public Voids
    public virtual void OnHit() {}

    #endregion
}
