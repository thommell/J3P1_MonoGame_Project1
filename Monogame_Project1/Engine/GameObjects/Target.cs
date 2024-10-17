using Monogame_Project1.Engine.BaseClasses;
using System;

namespace Monogame_Project1.Engine.GameObjects;

public class Target : BaseTarget
{
    #region Fields
    public int ScoreAmount { get; private set; }

    private readonly Scene _scene;
 
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

        scoringSystem.AddScore(ScoreAmount);
        ammoSystem.ResetAmmo();

        base.OnHit();
    }
}

