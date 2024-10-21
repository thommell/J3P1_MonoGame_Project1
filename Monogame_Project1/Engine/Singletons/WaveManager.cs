using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.Singletons;

public class WaveManager
{
    
    private static WaveManager _instance;
    public static WaveManager Instance => _instance ??= new WaveManager();

    public delegate void WaveEventHandler();


    public event WaveEventHandler WaveStarter;
    public event WaveEventHandler WaveEnder;
    
    private SpawningSystem _spawner;
    private Scene _currentScene;
    private int _currentWave;
    private bool _canSpawn = true;
    private WaveManager()
    {
        _currentWave = 0;
        WaveStarter += StartWave;
        WaveEnder += EndWave;
    }

    public void Initialize(Scene pScene)
    {
        _currentScene = pScene;
        _spawner = pScene.GetObject<SpawningSystem>();
    }

    public void Update()
    {
        var kb = Keyboard.GetState();
        bool hasFinishedLevel = _spawner.HasActiveTargets();
        if (kb.IsKeyDown(Keys.Space) && _canSpawn)
        {
            WaveStarter?.Invoke();
            return;
        }
        if (!_canSpawn && hasFinishedLevel)
            WaveEnder?.Invoke();
    }
    public void Draw(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.DrawString(_currentScene.font, _currentWave.ToString(), new Vector2(50, 50), Color.White);
    }
    private void StartWave()
    {
        _canSpawn = false;
        _currentWave++;
        Console.WriteLine($"Wave {_currentWave}");

        int targets = 3 + _currentWave * 2;
        int fakeTargets = 3 + _currentWave * 2;
        
        _spawner.StartSpawner();
    }
    private void EndWave()
    {
        _spawner.currentTargets.Clear();
        _canSpawn = true;
    }
}