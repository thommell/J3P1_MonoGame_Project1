using Monogame_Project1.Engine.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.UIObjects;

public class SoundSliderUI : SliderUI
{
    public SoundSliderUI(Texture2D pTexture) : base(pTexture) { }

    public override void LoadContent(ContentManager pContent)
    {
        currentValue = AudioManager.Instance.SoundVolume;

        base.LoadContent(pContent);
    }
    public override void OnValue(float pValue)
    {
        AudioManager.Instance.SetSoundVolume(pValue);

        base.OnValue(pValue);
    }
}
