namespace Monogame_Project1.Engine.BaseClasses;

public abstract class Scene
{
    #region Variables
    
    protected Game1 game;
    protected SceneManager manager;
    protected List<GameObject> objects = new();
    #endregion
    
    #region Properties
    
    public bool IsLoaded { get; set; }

    public List<GameObject> Objects => objects;
    
    #endregion

    #region Constructor

    public Scene(Game1 pGame, SceneManager pManager)
    {
        game = pGame;
        manager = pManager;
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
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw(pSpriteBatch);
            }
        }

    #endregion
}
