using System;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class SpawningSystem : GameObject
{
    private readonly Scene _scene;
    private readonly Game1 _game;
    public List<GameObject> currentTargets = new();
    private ShootingSystem _shootingSystem;
    private SceneManager _sceneManager;

    /// <summary>
    /// X = Target Y = FakeTarget
    /// </summary>
    private readonly Vector2 _targetAmounts; 
    private bool _hasSpawned;

    public bool HasSpawned
    {
        get => _hasSpawned;
        set => _hasSpawned = value;
    }
    public SpawningSystem(SceneManager pSceneManager, Vector2 pAmountsToSpawn)
    {
        _sceneManager = pSceneManager;
        _game = _sceneManager.Game;
        _scene = _sceneManager.CurrentScene;
        _targetAmounts = pAmountsToSpawn;
    }
    public override void LateLoad()
    {
        _shootingSystem = _scene.GetObject<ShootingSystem>();
        base.LateLoad();
    }
    public override void Update(GameTime pGameTime)
    {
        if (currentTargets.Count <= 0) return;
        _shootingSystem.CheckCollision();
    }

    public void StartSpawner()
    {
        CreateNewTargets();
    }
    /// <summary>
    /// Spawns new Targets and adds them to the current scene's objects and CurrentTargets' list.
    /// </summary>
    private void SpawnTargets()
    { 
        CreateTargets();
        CreateFakes();
    }

    private void CreateTargets()
    {
        
        for (int i = 0; i < _targetAmounts.X; i++)
        {
            Target newTarget = new Target(_game.Content.Load<Texture2D>("Target"), _scene, 2)
            {
                Position = GetPosition()
            };
            // Temp Fix
            newTarget.MovementSystem = CreateMovement(newTarget);
            
            _scene.Objects.Add(newTarget); 
            currentTargets.Add(newTarget);
        }
    }

    private void CreateFakes()
    {
        for (int i = 0; i < _targetAmounts.Y; i++)
        {
            FakeTarget newTarget = new FakeTarget(_game.Content.Load<Texture2D>("Bomb"), _sceneManager)
            {
                Position = GetPosition(),
                Color = Color.Green
            };
            
            newTarget.MovementSystem = CreateMovement(newTarget);            
            _scene.Objects.Add(newTarget);
            currentTargets.Add(newTarget);
            _hasSpawned = true;
        }
    }
    /// <summary>
    /// Destroys the current scene's Target's and creates new ones.
    /// </summary>
    private void CreateNewTargets()
    {
        _scene.DeactivateObjects(currentTargets);
        currentTargets.Clear();
        SpawnTargets();
    }
    public Vector2 GetPosition()
    {
        Random random = new();

        return new(random.Next(64, _game.GraphicsDevice.Viewport.Width - 64),
            random.Next(64, _game.GraphicsDevice.Viewport.Height - 64)
        ); 
    }

    private TargetMovement CreateMovement(BaseTarget pOwner)
    {
        Random random = new();
        int[] speedValues = { 100, 350 };
        int[] elapsedValues = { 1, 5 };

        return new TargetMovement(pOwner, GetElapsedTime(), GetMovementSpeed(), _game);

        float GetElapsedTime() 
        {
            return random.Next(elapsedValues[0], elapsedValues[1]);
        }
        float GetMovementSpeed()
        {
            return random.Next(speedValues[0], speedValues[1]);
        }
    }
}