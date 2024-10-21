using Monogame_Project1.Engine.BaseClasses;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Monogame_Project1.Engine.Singletons;


public class AudioManager
{
    private static AudioManager instance;

    public static AudioManager Instance => instance ??= (instance = new AudioManager());

    private Dictionary<string, SoundEffect> _soundEffects = new();

    private Dictionary<string, Song> _musics = new();

    private float _soundEffectVolume = 1f;
    private float _musicVolume = 1f;
    private string _activeSong;

    public float SoundVolume { get { return _soundEffectVolume; } set { _soundEffectVolume = value; } }

    public float MusicVolume { get { return _musicVolume; } set { _musicVolume = value; } }

    public void LoadContent(ContentManager pContent)
    {
        MediaPlayer.Volume = _musicVolume;

        //for Soundeffects, use Wav-files

        _soundEffects.Add("Gunshot", pContent.Load<SoundEffect>("GunshotWav"));

        //for music use Mp3-files

        _musics.Add("TestMusic", pContent.Load<Song>("TestMusic"));
        _musics.Add("MenuMusic", pContent.Load<Song>("Menu"));
    }
    public void PlaySound(string pSoundName, float pPitch = 0f)
    {
        if (_soundEffects.ContainsKey(pSoundName))
        {
            //SoundeffectInstance is created here to change volume and pitch
            SoundEffectInstance soundInstance = _soundEffects[pSoundName].CreateInstance();

            soundInstance.Volume = _soundEffectVolume;
            soundInstance.Pitch = Math.Clamp(pPitch, -1f, 1f);

            soundInstance.Play();
        }
        else Console.WriteLine("Sound doesn't exist");
    }
    public void PlayMusic(string pMusicName, bool pLoop)
    {
        if (MediaPlayer.State != MediaState.Playing)
        {
            Console.WriteLine("Tried playing song but there is no music playing.");
            _activeSong = null;
        }

        // Check if there is no music playing and if the user has given the correct song name.
        if (_musics.ContainsKey(pMusicName) && _activeSong != pMusicName && MediaPlayer.State != MediaState.Playing)
        {
            _activeSong = pMusicName;
            MediaPlayer.IsRepeating = pLoop;
            MediaPlayer.Play(_musics[pMusicName]);
        }
        else Console.WriteLine("Music doesn't exist or is already playing this music");
    }
    public void StopMusic()
    {
        _activeSong = null;
        MediaPlayer.Stop();
    }
    public void PauseMusic()
    {
        MediaPlayer.Pause();
    }
    public void UnpauseMusic()
    {
        MediaPlayer.Resume();
    }

    public void SetSoundVolume(float pVolume)
    {
        _soundEffectVolume = Math.Clamp(pVolume, 0f, 1f);
    }
    public void SetMusicVolume(float pVolume)
    {
        _musicVolume = Math.Clamp(pVolume, 0f, 1f);
        MediaPlayer.Volume = _musicVolume;
    }
}