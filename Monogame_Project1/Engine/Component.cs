namespace Monogame_Project1.Engine;

public abstract class Component
{
    #region Public Methods

    public abstract void Update(GameTime pGameTime); 
    public abstract void Draw(SpriteBatch pSpriteBatch);

    #endregion
}
