using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Scenes;

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
    private int _maxWaves;
    private bool _canSpawn = true;
    private int _completedWaves;

    public int MaxWaves { get => _maxWaves; set => _maxWaves = value; }
    private WaveManager()
    {
        WaveStarter += StartWave;
        WaveEnder += EndWave;
    }

    public void Initialize(Scene pScene)
    {
        _completedWaves = 0;
        _currentWave = 0;
        _currentScene = pScene;
        _spawner = pScene.GetObject<SpawningSystem>();
    }

    public void Update()
    {
        var kb = Keyboard.GetState();
        bool hasFinishedWave = !_spawner.HasActiveTargets();
        bool hasFinishedAllWaves = _maxWaves == _completedWaves;

        if (hasFinishedAllWaves)
        {
            ResultHandler.Instance.HandleResult((LevelScene)SceneManager.Instance.CurrentScene, Enums.Results.Win);
            //SceneManager.Instance.SwapScene(SceneManager.Instance.GetScene<WinScene>());
        }
        if (_canSpawn && _currentWave < _maxWaves)
        {
            WaveStarter?.Invoke();
            return;
        }
        if (!_canSpawn && hasFinishedWave)
            WaveEnder?.Invoke();
    }
    public void Draw(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.DrawString(_currentScene.font, "Wave " + _currentWave.ToString(), new Vector2(50, 50), Color.White);
    }
    private void StartWave()
    {
        _canSpawn = false;
        _currentWave++;
        Console.WriteLine($"Wave {_currentWave}");

        int targets = 3 + _currentWave * 2;
        int fakeTargets = 3 + _currentWave * 2;

        _spawner.StartSpawner(targets, fakeTargets);
    }
    private void EndWave()
    {
        _completedWaves++;
        _spawner.currentTargets.Clear();
        _canSpawn = true;
    }
}