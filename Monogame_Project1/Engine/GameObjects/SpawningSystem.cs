using System;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects;

public class SpawningSystem : GameObject
{
    private readonly Scene _scene;
    private KeyboardState _kb;
    public List<GameObject> currentTargets = new();
    private ShootingSystem _shootingSystem;
    private readonly int _amountToSpawn;
    private readonly int _fakesAmount;
    private bool _hasSpawned;
    public bool HasSpawned
    {
        get => _hasSpawned;
        set => _hasSpawned = value;
    }
    public SpawningSystem(Scene pScene, int pAmountToSpawn, int pFakesAmount)
    {
        _scene = pScene;
        _amountToSpawn = pAmountToSpawn;
        _fakesAmount = pFakesAmount;
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
        for (int i = 0; i < _amountToSpawn; i++)
        {
            Target newTarget = new Target(SceneManager.Instance.Game.Content.Load<Texture2D>("Target"), _scene, 2)
            {
                Position = GetPosition()
            };
            // Temp Fix
            newTarget.MovementSystem = CreateMovement(newTarget);
            
            _scene.Objects.Add(newTarget); 
            currentTargets.Add(newTarget);
        }

        for (int i = 0; i < _fakesAmount; i++)
        {
            FakeTarget newTarget = new FakeTarget(SceneManager.Instance.Game.Content.Load<Texture2D>("Bomb"))
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

        return new(random.Next(64, SceneManager.Instance.Game.GraphicsDevice.Viewport.Width - 64),
            random.Next(64, SceneManager.Instance.Game.GraphicsDevice.Viewport.Height - 64)
        ); 
    }

    private TargetMovement CreateMovement(BaseTarget pOwner)
    {
        Random random = new();
        int[] speedValues = { 100, 350 };
        int[] elapsedValues = { 1, 5 };

        return new TargetMovement(pOwner, GetElapsedTime(), GetMovementSpeed());

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