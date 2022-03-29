using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource musicPlayer = null;
    [SerializeField] private List<AudioSource> sfxPlayers = new List<AudioSource>();

    private Queue<SoundData> soundQueue = new Queue<SoundData>();

    private void Awake() => Initialize();

    public void PlayMusic(AudioClip musicClip, float volume)
    {
        //Fade Track In...
    }

    public void PlaySound(AudioClip soundClip, float volume, Vector2 position, bool is3D)
    {
        SoundData soundData = new SoundData();
        soundData.soundClip = soundClip;
        soundData.position = position;
        soundData.volume = volume;
        soundData.is3D = is3D;

        soundQueue.Enqueue(soundData);
    }
}

struct SoundData
{
    public AudioClip soundClip;
    public Vector2 position;
    public float volume;
    public bool is3D;
}
