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
    private TimeSystem _timeSystem;
    private Timer _timer;
    private int _currentWave;
    private int _maxWaves;
    private bool _canSpawn = true;
    private int _completedWaves;

    private float _delayBetweenWaves = 3f;
    private float _elapsedDelayTime = 0f;
    private bool _isDelaying = false;

    public bool IsDelaying { get => _isDelaying; set => _isDelaying = value; }

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
        _timeSystem = pScene.GetObject<TimeSystem>();
        _timer = pScene.GetObject<Timer>();

        WaveStarter += _timer.ToggleTimer;
        WaveEnder += _timer.ResetTimer;
        WaveEnder += _timer.ToggleTimer;

    }

    public void Update(GameTime pGameTime)
    {
        var kb = Keyboard.GetState();
        bool hasFinishedWave = !_spawner.HasActiveTargets();
        bool hasFinishedAllWaves = _maxWaves == _completedWaves;

        if (hasFinishedAllWaves)
        {
            ResultHandler.Instance.HandleResult((LevelScene)SceneManager.Instance.CurrentScene, Enums.Results.Win);
            //SceneManager.Instance.SwapScene(SceneManager.Instance.GetScene<WinScene>());
        }

        if (_isDelaying)
        {
            _elapsedDelayTime -= (float)pGameTime.ElapsedGameTime.TotalSeconds;
            _timer.IsRunning = false;
            if (_elapsedDelayTime <= 0)
            {
                _isDelaying = false;
                WaveStarter?.Invoke();
            }
            return;
        }

        if (_canSpawn && _currentWave < _maxWaves)
        {
            StartDelay();
            return;
        }
        if (!_canSpawn && hasFinishedWave)
            WaveEnder?.Invoke();
    }
    public void Draw(SpriteBatch pSpriteBatch)
    {
        string delayText = $"Next wave in: {Math.Ceiling(_elapsedDelayTime)} seconds.";
        string nextWaveText = $"Wave {_currentWave + 1} starting soon!";
        Vector2 boundingDelayText = _currentScene.font.MeasureString(delayText);
        Vector2 boundingWaveText = _currentScene.font.MeasureString(nextWaveText);
        if (_isDelaying)
        {
            pSpriteBatch.DrawString(_currentScene.font, delayText, new Vector2(Game1.ScreenWidth * 0.5f - boundingDelayText.X * 0.5f, Game1.ScreenHeight * 0.5f - boundingDelayText.Y * 0.5f), Color.White);
            pSpriteBatch.DrawString(_currentScene.font, nextWaveText, new Vector2(Game1.ScreenWidth * 0.5f - boundingWaveText.X * 0.5f, Game1.ScreenHeight * 0.5f - boundingWaveText.Y * 0.5f + 50), Color.White);
        }
        else
            pSpriteBatch.DrawString(_currentScene.font, "Wave " + _currentWave.ToString(), new Vector2(50, 50), Color.White);
    }
    private void StartWave()
    {
        _canSpawn = false;
        _currentWave++;
        Console.WriteLine($"Wave {_currentWave}");

        int targets = 2 + _currentWave * 2;
        int fakeTargets = 2 + _currentWave * 2;

        _spawner.StartSpawner(targets, fakeTargets);
    }
    private void EndWave()
    {
        _completedWaves++;
        _canSpawn = true;
        _spawner.ClearTargets();
    }

    private void StartDelay()
    {
        _isDelaying = true;
        _elapsedDelayTime = _delayBetweenWaves;
    }
}