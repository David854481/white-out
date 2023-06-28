using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    //declaration of variables
    public GameObject newHighScore;
    public TMPro.TextMeshProUGUI scoreTxts;


    void Start()
    {
        //sets boolean to false
        bool _newHighScore = false;

        //lowers the audio volume
        LeanTween.value(gameObject, AudioSystem.instance.MusicSource.volume, 0f, 1f)
        .setEaseLinear()
        .setOnUpdate(delegate (float _val)
        {
            AudioSystem.instance.MusicSource.volume = _val;
        });
        
        //plays game over audio
        AudioSystem.instance.PlaySound(AudioEnum.snd_gameover);

        //Sets new high score (if achieved)
        if (GameManager.instance.LastScore > GameManager.instance.HighScore)
        {
            GameManager.instance.HighScore = GameManager.instance.LastScore;
            _newHighScore = true;
            PlayerPrefs.SetInt(GameManager.HighScorePref, GameManager.instance.HighScore);
        }
        //Prints score + high score
        scoreTxts.text = string.Format("Your Score: {0}\nHigh Score: {1}",
            GameManager.instance.LastScore, GameManager.instance.HighScore);
        newHighScore.SetActive(_newHighScore);
    }

    public void ButtonCallback(int _index)
    {
        switch (_index)
        {
            case 0: //start
                SceneManager.LoadScene("ColorShooter");
                break;
            case 1: //menu
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
