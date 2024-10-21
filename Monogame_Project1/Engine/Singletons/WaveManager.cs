using System;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.Singletons;

public class WaveManager
{
    
    private static WaveManager _instance;
    public static WaveManager Instance => _instance ??= new WaveManager();

    private SpawningSystem _spawner;
    private Scene _currentScene;
    private int _currentWave;
    private bool _canSpawn = true;
    private WaveManager()
    {
        _currentWave = 0;
    }

    public void Initialize(Scene pScene)
    {
        _currentScene = pScene;
        _spawner = pScene.GetObject<SpawningSystem>();
    }

    public void Update()
    {
        var kb = Keyboard.GetState();
        if (kb.IsKeyDown(Keys.Space) && _canSpawn && _currentScene is LevelScene)
            StartWave();
    }

    public void Draw()
    {
        
    }
    
    private void StartWave()
    {
        _canSpawn = false;
        _currentWave++;
        Console.WriteLine($"Wave {_currentWave}");

        int targets = 3;
        int fakeTargets = 3;
        
        _spawner.StartSpawner(targets, fakeTargets);
    }
}