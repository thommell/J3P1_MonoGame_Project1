using System;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.BaseClasses;

public abstract class Scene
{
    #region Variables
    protected Game1 game;
    protected List<GameObject> objects = new();
    protected List<UIObject> uiObjects = new();
    protected SpriteFont font;
    protected string sceneName;
    #endregion
    
    #region Properties
    
    public bool IsLoaded { get; set; }

    public string SceneName
    {
        get => sceneName;
        set
        {
            if (sceneName != null)
                throw new Exception("The scene already has a name!");
            sceneName = value.ToLower();
        }
    }

    public List<GameObject> Objects => objects;
    
    #endregion

    #region Constructor
    public Scene()
    {
        font = SceneManager.Instance.Game.Content.Load<SpriteFont>("UIText");
        game = SceneManager.Instance.Game;
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
    public void UnloadScene()
    {
        Console.WriteLine($"Unloading {SceneManager.Instance.CurrentScene.SceneName}");
        objects.Clear();
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

    public List<T> GetObjects<T>() where T : GameObject
    {
        List<T> objectHolder = new List<T>();
        for (int i = 0; i < objects.Count; i++)
            if (objects[i] is T obj)
                objectHolder.Add(obj);
        return objectHolder;
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
