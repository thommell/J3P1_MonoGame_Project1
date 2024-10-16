using Monogame_Project1.Engine.BaseClasses;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Monogame_Project1.Engine.GameObjects;


public class AudioManager
{
    private static AudioManager instance;

    public static AudioManager Instance => instance ??= (instance = new AudioManager());

    private Dictionary<string, SoundEffect> soundEffects = new();

    public void LoadContent(ContentManager pContent)
    {
        soundEffects.TryAdd("Gunshot", pContent.Load<SoundEffect>("GunshotWav"));
    }
    public void PlaySound(string pSoundName)
    {
        if (soundEffects.ContainsKey(pSoundName))
            soundEffects[pSoundName].Play();    
        else Console.WriteLine("Sound doesn't exist");
    }
}