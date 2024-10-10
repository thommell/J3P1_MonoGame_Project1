using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects
{
    public class SwitchSceneButton : Button
    {
        private Scene _scene;
        public SwitchSceneButton(Game1 pGame, SceneManager pManager, Texture2D pTexture, string text, Scene pScene, bool pIsActive = true) : base(pGame, pManager, pTexture, text, pIsActive) 
        { 
            _scene = pScene;
        }
        protected override void OnClick()
        {
            if (manager.CurrentScene is not MainMenu)
            {
                manager.RestartGame();
            }
            manager.ChangeScene(_scene);
        }
    }
}