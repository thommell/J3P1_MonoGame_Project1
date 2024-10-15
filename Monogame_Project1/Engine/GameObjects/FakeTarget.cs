using Monogame_Project1.Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class FakeTarget : BaseTarget
{
    #region Constructors

    private SceneManager _sceneManager;
    private ResultHandler _resultHandler;
    public FakeTarget(Texture2D pTexture, SceneManager pSceneManager, Game1 pGame) : base(pTexture, pGame) 
    {
        _sceneManager = pSceneManager;
        _resultHandler = _sceneManager.CurrentScene.GetObject<ResultHandler>();
    }

    #endregion

    #region Public Voids
    public override void OnHit()
    {
        _resultHandler.HandleResult(Result.Lose);
    }

    #endregion
}
