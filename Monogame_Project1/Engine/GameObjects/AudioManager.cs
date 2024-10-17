using Monogame_Project1.Engine.BaseClasses;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Monogame_Project1.Engine.GameObjects;


public class AudioManager
{
    private static AudioManager instance;

    public static AudioManager Instance => instance ??= (instance = new AudioManager());

    private Dictionary<string, SoundEffect> _soundEffects = new();

    private float _volume = 1f;

    public float Volume { get { return _volume; } set { _volume = value; } }

    public void LoadContent(ContentManager pContent)
    {
        _soundEffects.Add("Gunshot", pContent.Load<SoundEffect>("GunshotWav"));
    }
    public void PlaySound(string pSoundName, float pPitch = 0f)
    {      
        if (_soundEffects.ContainsKey(pSoundName))
        {
            //SoundeffectInstance is created here to change volume and pitch
            SoundEffectInstance soundInstance = _soundEffects[pSoundName].CreateInstance();
            
            soundInstance.Volume = _volume;
            soundInstance.Pitch = Math.Clamp(pPitch, -1f, 1f);

            soundInstance.Play();
        }           
        else Console.WriteLine("Sound doesn't exist");
    }
    public void SetVolume(float pVolume)
    {
       // Console.WriteLine(pVolume);
        _volume = Math.Clamp(pVolume, 0f, 1f);
    }
}