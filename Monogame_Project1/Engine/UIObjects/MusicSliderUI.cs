using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.UIObjects;

public class MusicSliderUI : SliderUI
{
    public MusicSliderUI(Texture2D pTexture) : base(pTexture) { }

    public override void LoadContent(ContentManager pContent)
    {
        currentValue = AudioManager.Instance.MusicVolume;

        base.LoadContent(pContent);
    }
    public override void OnValue(float pValue)
    {
        AudioManager.Instance.SetMusicVolume(pValue);

        base.OnValue(pValue);
    }
}
