using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class Target : GameObject
{
    #region Fields
    
    private ShootingSystem _shootingSystem;
    private SpawningSystem _spawningSystem;
    public int ScoreAmount { get; private set; }
    
    #endregion
    
    #region Constructors
    
    public Target(Texture2D pTexture, Scene pScene) : base(pTexture)
    {
        _shootingSystem = pScene.GetObject<ShootingSystem>();
        _spawningSystem = pScene.GetObject<SpawningSystem>();
        ScoreAmount = 2;
    }
    
    #endregion
    
    #region Public Methods

    public override void Update(GameTime pGameTime)
    {
        _shootingSystem.CheckCollision(this);
    }
    public void Destroy()
    {
        _spawningSystem.RemoveTarget(this);
    }
    #endregion
}

