using System;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects
{
    public class SelectionScreenButton : Button
    {
        private Texture2D _lock;

        private LevelScene _sceneToSwitchTo;
        private bool _locked;

        public bool IsLocked
        {
            get => _locked;
            set => _locked = value;
        }

        public LevelScene SceneToSwitchTo => _sceneToSwitchTo;

        public SelectionScreenButton(Texture2D pTexture, string text, LevelScene pSceneToSwitchTo, Texture2D pLock, bool pLocked = false) : base(pTexture, text)
        {
            _sceneToSwitchTo = pSceneToSwitchTo;
            _locked = pLocked;
            _lock = pLock;
        }
        public override void Update(GameTime pGameTime)
        {
            if (_locked)
            {
                Console.WriteLine(IsLocked);
                return;
            }

            base.Update(pGameTime);
        }
        public override void Draw(SpriteBatch pSpriteBatch)
        {
            base.Draw(pSpriteBatch);

            if (_locked) pSpriteBatch.Draw(_lock, new Vector2(position.X - texture.Width / 2, position.Y - texture.Height / 2), Color.White);           
        }
        protected override void OnClick()
        {
            SceneManager.Instance.SwapScene(_sceneToSwitchTo);   
            base.OnClick();
        }
        public void Unlock()
        {
            _locked = false;
        }
    }
}