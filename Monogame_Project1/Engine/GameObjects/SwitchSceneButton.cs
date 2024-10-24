using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects
{
    public class SwitchSceneButton : Button
    {
        private readonly Scene _scene;
        public SwitchSceneButton(Texture2D pTexture, string text, Scene pScene, bool pIsActive = true) : base(pTexture, text, pIsActive) 
        { 
            _scene = pScene;
        }
        protected override void OnClick()
        {
            SceneManager.Instance.SwapScene(_scene);

            base.OnClick();
        }
    }
}