using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects
{
    public class SelectionScreenButton : Button
    {
        private Texture2D _lock;

        private Scene _sceneToSwitchTo;
        private bool _locked;

        public bool IsLocked
        {
            get => _locked;
            set => _locked = value;
        }

        public Scene SceneToSwitchTo => _sceneToSwitchTo;

        public SelectionScreenButton(Game1 pGame, SceneManager pManager, Texture2D pTexture, string text, Scene pSceneToSwitchTo, Texture2D pLock, bool pLocked = false) : base(pGame, pManager, pTexture, text)
        {
            _sceneToSwitchTo = pSceneToSwitchTo;
            _locked = pLocked;
            _lock = pLock;
        }
        public override void Update(GameTime pGameTime)
        {
            if (_locked) return;

            base.Update(pGameTime);
        }
        public override void Draw(SpriteBatch pSpriteBatch)
        {
            base.Draw(pSpriteBatch);

            if (_locked) pSpriteBatch.Draw(_lock, new Vector2(position.X - texture.Width / 2, position.Y - texture.Height / 2), Color.White);           
        }
        protected override void OnClick()
        {
            manager.ChangeScene(_sceneToSwitchTo);   
        }
        public void Unlock()
        {
            _locked = false;
        }
    }
}