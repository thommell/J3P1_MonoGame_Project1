using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.BaseClasses;

public abstract class Scene
{
    #region Variables
    
    protected Game1 game;
    protected SceneManager manager;
    protected List<GameObject> objects = new();
    protected List<UIObject> uiObjects = new();
    protected SpriteFont font;
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
        font = game.Content.Load<SpriteFont>("UIText");
    }

    #endregion

    #region Public Methods

    public virtual void LoadContent(ContentManager pContent)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].IsActive) continue;
                objects[i].LoadContent(pContent);
        }

        for (int i = 0; i < uiObjects.Count; i++)
        {
            if (!uiObjects[i].IsActive) continue;
                uiObjects[i].LoadContent(pContent);
        }
    }
    public virtual void LateLoad()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].IsActive) continue;
                objects[i].LateLoad();
        }
        for (int i = 0; i < uiObjects.Count; i++)
        {
            if (!uiObjects[i].IsActive) continue;
                uiObjects[i].LateLoad();
        }
    }
    public virtual void Update(GameTime pGameTime)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].IsActive) continue;
                objects[i].Update(pGameTime);
        }
        for (int i = 0; i < uiObjects.Count; i++)
        {
            if (!uiObjects[i].IsActive) continue;
                uiObjects[i].Update(pGameTime);
        }
    }
    public virtual void Draw(SpriteBatch pSpriteBatch)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].IsActive) continue;
                objects[i].Draw(pSpriteBatch);
        }
        for (int i = 0; i < uiObjects.Count; i++)
            if (uiObjects[i].IsActive) 
                uiObjects[i].Draw(pSpriteBatch);
    }
    public T GetObject<T>() where T : GameObject
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] is T obj)
                return obj;
        }
        return null;
    }

    public T GetUIObject<T>() where T : UIObject
    {
        for (int i = 0; i < uiObjects.Count; i++)
        {
            if (uiObjects[i] is T uiObj)
                return uiObj;
        }
        return null;
    }
    public void DeactivateObjects(List<GameObject> pObjects) =>  
        pObjects.ForEach(pObject => pObject.IsActive = false);
   
    #endregion
}
