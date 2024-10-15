using Microsoft.Xna.Framework;
using Monogame_Project1.Engine.BaseClasses;
using System;

namespace Monogame_Project1.Engine.GameObjects;

public class AnimationsPlayer : GameObject
{
    #region Fields
    private List<Animation> _animationsPlayer = new();
    #endregion

    #region Methods
    public void AddAnimation(Point pMousePoint, Texture2D pSpriteSheet, int pColumns, int pRows, float pAnimationSpeed = 0.1f)
    {
        _animationsPlayer.Add(new Animation(pSpriteSheet, new Vector2(pMousePoint.X, pMousePoint.Y), pColumns, pRows, pAnimationSpeed));
    }
    public override void Update(GameTime pGameTime) 
    {
        for (int i = 0; i < _animationsPlayer.Count; i++)
        {
            _animationsPlayer[i].Update(pGameTime);     
        }

        base.Update(pGameTime);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        for (int i = 0; i < _animationsPlayer.Count; i++)
        {
            _animationsPlayer[i].Draw(pSpriteBatch);
        }

        base.Draw(pSpriteBatch);
    }
    #endregion
}
