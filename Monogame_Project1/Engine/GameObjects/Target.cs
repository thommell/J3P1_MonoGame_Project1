using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Singletons;
using System;

namespace Monogame_Project1.Engine.GameObjects;

public class Target : BaseTarget
{
    #region Fields
    public int ScoreAmount { get; private set; }

    private readonly Scene _scene;
    private Game1 _game;
 
    #endregion
    
    #region Constructors
    
    public Target(Texture2D pTexture, Scene pScene, int pScoreAmount) : base(pTexture)
    {
        ScoreAmount = pScoreAmount;
        _scene = pScene;
    }

    #endregion

    public override void OnHit()
    {
        ScoringSystem scoringSystem = _scene.GetObject<ScoringSystem>();
        AmmoSystem ammoSystem = _scene.GetObject<AmmoSystem>();
        AnimationsPlayer animPlayer = _scene.GetObject<AnimationsPlayer>();

        animPlayer.AddAnimation(Position, SceneManager.Instance.Game.Content.Load<Texture2D>("TargetExplosie"), 3, 3, 0.05f);
        scoringSystem.AddScore(ScoreAmount);
        ammoSystem.ResetAmmo();

        base.OnHit();
    }
}

