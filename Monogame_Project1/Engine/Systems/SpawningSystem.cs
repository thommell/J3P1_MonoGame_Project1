using System;
using System.Net;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Scenes;

namespace Monogame_Project1.Engine.Systems;

public class SpawningSystem : GameObject
{
    private Scene _scene;
    private Game1 _game;
    private KeyboardState kb;
    private List<TestTarget> _currentTargets = new();
    public SpawningSystem(Scene pScene, Game1 pGame) : base()
    {
        _scene = pScene;
        _game = pGame;
    }
    private Keys _spawnKey = Keys.Space;
    private bool _canSpawn = true;
    private int _amountToSpawn = 3;
    private List<Target> _targets = new();

    public override void Update(GameTime pGameTime)
    {
        kb = Keyboard.GetState();
        CheckInput();
    }

    private void CheckInput()
    {
        if (kb.IsKeyDown(_spawnKey) && _canSpawn)
        {
            _canSpawn = false;
            SpawnTargets();
        }
        if (kb.IsKeyUp(_spawnKey))
        {
            _canSpawn = true;
        } 
    }
    private void SpawnTargets()
    {
        foreach (var target in _currentTargets)
        {
            _scene.Objects.Remove(target);
        }
        _currentTargets.Clear(); 
        for (int i = 0; i < _amountToSpawn; i++)
        {
            var newTarget = CreateTarget();
            _scene.Objects.Add(newTarget); 
            _currentTargets.Add(newTarget);
        }

        _amountToSpawn++;
    }
    private TestTarget CreateTarget()
    {
        Random random = new();
        Vector2 newValue = new(
            random.Next(64, _game.GraphicsDevice.Viewport.Width - 64),
            random.Next(64, _game.GraphicsDevice.Viewport.Height - 64)
        );
        TestTarget newTarget = new(_game.Content.Load<Texture2D>("UI_Slot"))
        {
            Position = newValue
        };
        return newTarget;
    }
}