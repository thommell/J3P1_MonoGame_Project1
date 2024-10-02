using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects
{
    internal class SelectionScreenButton : Button
    {
        private Scene _sceneToSwitchTo;
        public SelectionScreenButton(Game1 pGame, SceneManager pManager, Texture2D pTexture, string text, Scene pSceneToSwitchTo) : base(pGame, pManager, pTexture, text) { 
        
            _sceneToSwitchTo = pSceneToSwitchTo;
        }

        protected override void OnClick()
        {
            manager.ChangeScene(_sceneToSwitchTo);
        }
    }
}
