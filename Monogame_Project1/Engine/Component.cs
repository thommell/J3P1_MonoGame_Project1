namespace Monogame_Project1.Engine;

public abstract class Component
{
    public abstract void Update(GameTime pGameTime);
    public abstract void Draw(SpriteBatch pSpriteBatch);
}
