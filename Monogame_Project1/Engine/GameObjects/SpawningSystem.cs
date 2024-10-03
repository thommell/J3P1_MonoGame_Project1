using System;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class SpawningSystem : GameObject
{
    private Scene _scene;
    private Game1 _game;
    private KeyboardState kb;
    public List<Target> CurrentTargets = new();
    private ShootingSystem _shootingSystem;
    private int _amountToSpawn;
    public SpawningSystem(Scene pScene, Game1 pGame, int pAmountToSpawn)
    {
        _scene = pScene;
        _game = pGame;
        _amountToSpawn = pAmountToSpawn;
    }
    private Keys _spawnKey = Keys.Space;
    private bool _canSpawn = true;

    public override void LateLoad()
    {
        _shootingSystem = _scene.GetObject<ShootingSystem>();
        base.LateLoad();
    }
    public override void Update(GameTime pGameTime)
    {
        kb = Keyboard.GetState();
        _shootingSystem.CheckCollision();
        CheckInput();
    }
    private void CheckInput()
    {
        if (kb.IsKeyDown(_spawnKey) && _canSpawn)
        {
            _canSpawn = false;
            CheckTargets();
        }
        if (kb.IsKeyUp(_spawnKey))
        {
            _canSpawn = true;
        } 
    }
    private void SpawnTargets()
    { 
        for (int i = 0; i < _amountToSpawn; i++)
        {
            var newTarget = CreateTarget();
            _scene.Objects.Add(newTarget); 
            CurrentTargets.Add(newTarget);
        }
    }
    public void CheckTargets()
    {
        for (int i = CurrentTargets.Count - 1; i >= 0; i--)
        {
            RemoveTarget(CurrentTargets[i]);
        }
        SpawnTargets();
    }
    public void RemoveTarget(Target pTarget)
    {
        _scene.Objects.Remove(pTarget);
        CurrentTargets.Remove(pTarget);
    }
    public Target CreateTarget()
    {
        Random random = new();
        Vector2 newValue = new(
            random.Next(64, _game.GraphicsDevice.Viewport.Width - 64),
            random.Next(64, _game.GraphicsDevice.Viewport.Height - 64)
        );
        Target newTarget = new(_game.Content.Load<Texture2D>("UI_Slot"), _scene)
        {
            Position = newValue
        };
        return newTarget;
    }
}