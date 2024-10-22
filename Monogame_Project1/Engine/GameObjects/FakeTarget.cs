using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Enums;
using Monogame_Project1.Engine.Scenes;
using Monogame_Project1.Engine.Singletons;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class FakeTarget : BaseTarget
{
    #region Constructors

    private SceneManager _sceneManager;
    private Scene _scene;
    private PauseSystem _pauseSystem;
    public FakeTarget(Texture2D pTexture) : base(pTexture)
    {
        _scene = SceneManager.Instance.CurrentScene;
        _pauseSystem = _scene.GetObject<PauseSystem>();
    }

    #endregion

    #region Public Voids
    public override void OnHit()
    {
        _pauseSystem.ToggleWithoutDraw();
        
        OnHitAsync();
    }
    private async void OnHitAsync()
    {
        await PlayAnimationAsync();

        ResultHandler.Instance.HandleResult((LevelScene)SceneManager.Instance.CurrentScene, Results.Lose);
    }
    private async Task PlayAnimationAsync()
    {
        AnimationsPlayer animPlayer = _scene.GetObject<AnimationsPlayer>();

        float animationSpeed = 0.15f;
        int columns = 3;
        int rows = 3;

        Console.WriteLine("hallo");

        animPlayer.AddAnimation(Position, SceneManager.Instance.Game.Content.Load<Texture2D>("BigExplosion"), columns, rows, animationSpeed);

        int delayTime = (int)Math.Round(columns * rows * animationSpeed * 1000);

        await Task.Delay(delayTime);
    }

    #endregion
}
