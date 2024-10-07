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
    private SceneManager _sceneManager;
    private readonly int _amountToSpawn;
    private readonly int _fakesAmount;
    public SpawningSystem(Scene pScene, Game1 pGame, SceneManager sceneManager, int pAmountToSpawn, int pFakesAmount)
    {
        _scene = pScene;
        _game = pGame;
        _amountToSpawn = pAmountToSpawn;
        _fakesAmount = pFakesAmount;
        _sceneManager = sceneManager;
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
            Target newTarget = new Target(_game.Content.Load<Texture2D>("UI_Slot"), _scene, 2)
            {
                Position = GetPosition()
            };

            _scene.Objects.Add(newTarget); 
            CurrentTargets.Add(newTarget);
        }

        for (int i = 0; i < _fakesAmount; i++)
        {
            FakeTarget newTarget = new FakeTarget(_game.Content.Load<Texture2D>("UI_Slot"), _sceneManager)
            {
                Position = GetPosition(),
                Color = Color.Green
            };

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
    public Vector2 GetPosition()
    {
        Random random = new();
        Vector2 newValue = new(
            random.Next(64, _game.GraphicsDevice.Viewport.Width - 64),
            random.Next(64, _game.GraphicsDevice.Viewport.Height - 64)
        );

        return newValue;
    }

    /// <summary>
    /// Creates one new Target with a randomized position.
    /// </summary>
    /// <returns>A new Target</returns>
  /* public Target CreateTarget()
    {
        Random random = new();
        Vector2 newValue = new(
            random.Next(64, _game.GraphicsDevice.Viewport.Width - 64),
            random.Next(64, _game.GraphicsDevice.Viewport.Height - 64)
        );
        Target newTarget = new(_game.Content.Load<Texture2D>("UI_Slot"), _scene, 2) // 
        {
            Position = newValue
        };
        return newTarget;
    } */
}