using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public int HighScore, LastScore;
    public const string HighScorePref = "High Score";
    public float musicVolume, soundVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            HighScore = PlayerPrefs.GetInt("High Score", 0);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        //Do another playerprefs for the audio

    }
}
