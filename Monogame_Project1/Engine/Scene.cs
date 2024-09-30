namespace Monogame_Project1.Engine;

public abstract class Scene
{
    protected Game1 game;
    protected List<GameObject> objects;

    public Scene(Game1 pGame)
    {
        game = pGame;
    }

    public abstract void LoadContent(ContentManager pContent);

    public virtual void Update(GameTime pGameTime)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].Update(pGameTime);
        }
    }

    public virtual void Draw(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.Begin();
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].Draw(pSpriteBatch);
        }
        pSpriteBatch.End();
    }
}
