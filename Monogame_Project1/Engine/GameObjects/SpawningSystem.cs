using System;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class SpawningSystem : GameObject
{
    private readonly Scene _scene;
    private readonly Game1 _game;
    private KeyboardState _kb;
    public List<GameObject> CurrentTargets = new();
    private ShootingSystem _shootingSystem;
    private readonly int _amountToSpawn;
    public SpawningSystem(Scene pScene, Game1 pGame, int pAmountToSpawn)
    {
        _scene = pScene;
        _game = pGame;
        _amountToSpawn = pAmountToSpawn;
    }
    private readonly Keys _spawnKey = Keys.Space;
    private bool _canSpawn = true;

    public override void LateLoad()
    {
        _shootingSystem = _scene.GetObject<ShootingSystem>();
        base.LateLoad();
    }
    public override void Update(GameTime pGameTime)
    {
        _kb = Keyboard.GetState();
        _shootingSystem.CheckCollision();
        CheckInput();
    }
    private void CheckInput()
    {
        if (_kb.IsKeyDown(_spawnKey) && _canSpawn)
        {
            _canSpawn = false;
            CreateNewTargets();
        }
        if (_kb.IsKeyUp(_spawnKey))
            _canSpawn = true;
    }
    /// <summary>
    /// Spawns new Targets and adds them to the current scene's objects and CurrentTargets' list.
    /// </summary>
    private void SpawnTargets()
    { 
        for (int i = 0; i < _amountToSpawn; i++)
        {
            Target newTarget = CreateTarget();
            _scene.Objects.Add(newTarget); 
            CurrentTargets.Add(newTarget);
        }
    }
    /// <summary>
    /// Destroys the current scene's Target's and creates new ones.
    /// </summary>
    public void CreateNewTargets()
    {
        _scene.DeactivateObjects(CurrentTargets);
        CurrentTargets.Clear();
        SpawnTargets();
    }
    /// <summary>
    /// Creates one new Target with a randomized position.
    /// </summary>
    /// <returns>A new Target</returns>
    public Target CreateTarget()
    {
        Random random = new();
        Vector2 newValue = new(
            random.Next(64, _game.GraphicsDevice.Viewport.Width - 64),
            random.Next(64, _game.GraphicsDevice.Viewport.Height - 64)
        );
        Target newTarget = new(_game.Content.Load<Texture2D>("UI_Slot"), 2) // 
        {
            Position = newValue
        };
        return newTarget;
    }
}