using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;

namespace Monogame_Project1.Engine.GameObjects;

public class ResultHandler : GameObject
{
    private SpawningSystem _spawningSystem;
    private SceneManager _sceneManager;
    public ResultHandler(SpawningSystem spawningSystem, SceneManager pSceneManager)
    {
        _spawningSystem = spawningSystem;
        _sceneManager = pSceneManager;
    }

    public override void Update(GameTime pGameTime)
    {
        if (!_spawningSystem.HasSpawned) return;
        if (_spawningSystem.CurrentTargets.Any(a => a is Target && a.IsActive) && _spawningSystem.HasSpawned)
        {
            Console.WriteLine("User hasn't finished the level yet!");
        }
        else
        {
            Console.WriteLine("User has finished the level!");
            _sceneManager.ChangeScene(_sceneManager.GetScene<LevelSelectionScene>());
        }
    }
}