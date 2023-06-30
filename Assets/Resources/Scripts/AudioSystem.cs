using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioEnum
{
    mus_gameplay,
    mus_mainmenu,
    snd_enemyDamaged,
    snd_gameover,
    snd_playerDamaged,
    snd_shoot
}


public class AudioSystem : MonoBehaviour
{
    public static AudioSystem instance;
    List<AudioClip> audioList = new List<AudioClip>();

    public AudioSource SoundSource;
    public AudioSource MusicSource;
    private float defaultVolume;
    public float DefaultVolume { get => defaultVolume; set => defaultVolume = value; }

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (string _s in System.Enum.GetNames(typeof(AudioEnum)))
            audioList.Add(Resources.Load<AudioClip>("Audio/" + _s));    
    }

    public void PlaySound(AudioEnum _whichSound)
    {
        SoundSource.PlayOneShot(audioList[(int)_whichSound]);
    }

    public void PlayMusic(AudioEnum _whichMusic)
    {
        MusicSource.Stop();
        MusicSource.clip = (audioList[(int)_whichMusic]);
        MusicSource.Play();
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }


}
