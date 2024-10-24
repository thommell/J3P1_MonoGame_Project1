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
    private int _completedWaves;

    private bool _canSpawn = true;
    private bool _isDelaying = false;
    private float _delayBetweenWaves = 3f;
    private float _elapsedDelayTime = 0f;

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

        AssignEvents();

    }

    public void Update(GameTime pGameTime)
    {
        if (_isDelaying)
        {
            UpdateDelay(pGameTime);
            return;
        }

        if (_canSpawn && _currentWave < _maxWaves)
        {
            StartDelay();
            return;
        }

        if (!_canSpawn && !_spawner.HasActiveTargets())
            WaveEnder?.Invoke();

        if (_completedWaves == _maxWaves)
            ResultHandler.Instance.HandleResult((LevelScene)SceneManager.Instance.CurrentScene, Enums.Results.Win);
    }

    public void Draw(SpriteBatch pSpriteBatch)
    {
        if (_isDelaying)
        {
            string delayText = $"Next wave in: {Math.Ceiling(_elapsedDelayTime)} seconds.";
            string nextWaveText = $"Wave {_currentWave + 1} starting soon!";
            DrawCenteredText(pSpriteBatch, delayText, nextWaveText);
        }
        else
            pSpriteBatch.DrawString(_currentScene.font, "Wave " + _currentWave.ToString(), new Vector2(50, 50), Color.White);
    }

    private void DrawCenteredText(SpriteBatch pSpriteBatch, string pDelayText, string pNextWaveText)
    {
        Vector2 delayTextBounds = _currentScene.font.MeasureString(pDelayText);
        Vector2 waveTextBounds = _currentScene.font.MeasureString(pNextWaveText);

        pSpriteBatch.DrawString(_currentScene.font, pDelayText, new Vector2(Game1.ScreenWidth * 0.5f - delayTextBounds.X * 0.5f, Game1.ScreenHeight * 0.5f - delayTextBounds.Y * 0.5f), Color.White);
        pSpriteBatch.DrawString(_currentScene.font, pNextWaveText, new Vector2(Game1.ScreenWidth * 0.5f - waveTextBounds.X * 0.5f, Game1.ScreenHeight * 0.5f - waveTextBounds.Y * 0.5f + 50f), Color.White);
    }

    private void StartWave()
    {
        _canSpawn = false;
        _currentWave++;

        int targetCount = CalculateTargetCount();

        _spawner.StartSpawner(targetCount, targetCount);
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
        _timer.IsRunning = false;
    }

    private void UpdateDelay(GameTime pGameTime)
    {
        _elapsedDelayTime -= (float)pGameTime.ElapsedGameTime.TotalSeconds;

        if ( _elapsedDelayTime <= 0)
        {
            _isDelaying = false;
            WaveStarter?.Invoke();
        }
    }

    private int CalculateTargetCount() => 2 + _currentWave * 2;

    private void AssignEvents()
    {
        WaveStarter += _timer.ToggleTimer;
        WaveEnder += _timer.ResetTimer;
        WaveEnder += _timer.ToggleTimer;
    }
}