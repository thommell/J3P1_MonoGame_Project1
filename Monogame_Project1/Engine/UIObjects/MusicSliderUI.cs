﻿using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.UIObjects;

public class MusicSliderUI : SliderUI
{
    private SpriteFont _font;
    private string _text;
    public MusicSliderUI(Texture2D pTexture, string pText = null, bool pActive = true) : base(pTexture, pActive) 
    { 
        _text = pText;
    }
    public override void LoadContent(ContentManager pContent)
    {
        _font = pContent.Load<SpriteFont>("UIText");

        currentValue = AudioManager.Instance.MusicVolume;

        base.LoadContent(pContent);
    }
    public override void OnValue(float pValue)
    {
        AudioManager.Instance.SetMusicVolume(pValue);

        base.OnValue(pValue);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        if (_text != null)
        {
            Vector2 textSize = _font.MeasureString(_text);

            float textX = recBackGround.Left + (recBackGround.Width / 2) - (textSize.X / 2);

            float textY = textY = recBackGround.Top - textSize.Y - 15;

            pSpriteBatch.DrawString(_font, _text, new Vector2(textX + 5, textY - 5), Color.Black);
            pSpriteBatch.DrawString(_font, _text, new Vector2(textX, textY), Color.White);
        }

        base.Draw(pSpriteBatch);
    }
}