namespace Monogame_Project1.Engine.BaseClasses;

public abstract class Component
{
    #region Public Methods

    public abstract void LoadContent(ContentManager pContent);
    public abstract void LateLoad();
    public abstract void Update(GameTime pGameTime); 
    public abstract void Draw(SpriteBatch pSpriteBatch);

    #endregion
}
