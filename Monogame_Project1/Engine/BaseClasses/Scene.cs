namespace Monogame_Project1.Engine.BaseClasses;

public abstract class Scene
{
    #region Variables
    
    protected Game1 game;
    protected List<GameObject> objects;
    #endregion
    
    #region Properties
    public bool IsLoaded { get; set; }
    
    #endregion

    #region Constructor

    public Scene(Game1 pGame)
    {
        game = pGame;
    }

    #endregion

    #region Public Methods

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

    #endregion
}
