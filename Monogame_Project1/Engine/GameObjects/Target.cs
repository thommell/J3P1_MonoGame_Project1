using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class Target : GameObject
{
    #region Fields
    
    private ShootingSystem _shootingSystem;
    private SpawningSystem _spawningSystem;
    private Scene _currentScene;
    public int ScoreAmount { get; private set; }
    
    #endregion
    
    #region Constructors
    
    public Target(Texture2D pTexture, Scene pScene) : base(pTexture)
    {
        _currentScene = pScene;
        ScoreAmount = 2;
        _spawningSystem = _currentScene.GetObject<SpawningSystem>();
        _shootingSystem = _currentScene.GetObject<ShootingSystem>();
    }
    
    #endregion
    
    #region Public Methods

    public override void Update(GameTime pGameTime)
    {
        _shootingSystem.CheckCollision(this);

        base.Update(pGameTime);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        if (!isActive) return;

        base.Draw(pSpriteBatch);
    }
    public void Destroy()
    {
        _spawningSystem.RemoveTarget(this);
    }
    #endregion
}

