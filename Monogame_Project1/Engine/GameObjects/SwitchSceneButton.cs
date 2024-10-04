using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects
{
    public class SwitchSceneButton : Button
    {
        private Scene _scene;
        public SwitchSceneButton(Game1 pGame, SceneManager pManager, Texture2D pTexture, string text, Scene pScene) : base(pGame, pManager, pTexture, text) 
        { 
             _scene = pScene;
        }
        protected override void OnClick()
        {
            manager.ChangeScene(_scene);
        }
}
    }
