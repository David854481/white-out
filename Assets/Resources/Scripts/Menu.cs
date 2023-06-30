using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Image transition;
    public GameObject instructions;
    public GameObject settings;
    public GameObject exitInstructionsButton;
    public GameObject exitSettingsButton;
    public GameObject startButton;
    public GameObject exitButton;
    public GameObject instructionsButton;
    public GameObject settingsButton;
    public GameManager gameManager;
    public bool inWindow;

    void Start()
    {
        //Gets audio file
        AudioSystem.instance.PlayMusic(AudioEnum.mus_mainmenu);
        //Gets the sliders
        GameObject.Find("SoundSlider").GetComponent<Slider>().normalizedValue = AudioSystem.instance.SoundSource.volume;
        GameObject.Find("MusicSlider").GetComponent<Slider>().normalizedValue = AudioSystem.instance.MusicSource.volume;
        //Fades in music
        /*LeanTween.value(gameObject, 0f, GameObject.Find("MusicSlider").GetComponent<Slider>().normalizedValue, 0f)
        .setEaseLinear()
        .setOnUpdate(delegate (float _val)
        {
            AudioSystem.instance.MusicSource.volume = _val;
        });*/
        //Plays the music
        AudioSystem.instance.MusicSource.Play();

    }
    public void ButtonCallback(int _index)
    {
        switch (_index)
        {
            case 0: //start
                AudioSystem.instance.DefaultVolume = GameObject.Find("MusicSlider").GetComponent<Slider>().normalizedValue;
                SceneManager.LoadScene("ColorShooter");
                break;
            case 1: //Enters instructions menu
                //Disables main menu buttons
                inWindow = true;
                //Animates the UI using LeanTween
                LeanTween.scale(instructions, new Vector2(1.0f, 1.0f), 0.80f)
                    .setEaseInOutQuad();
                LeanTween.scale(exitInstructionsButton, new Vector2(1.0f, 1.0f), 0.80f)
                    .setEaseInOutQuad();
                LeanTween.moveY(exitInstructionsButton, -5f, 1.1f)
                    .setEaseInOutQuad();
                break;
            case 2: //Exits instructions menu
                //Enables main menu buttons
                inWindow = false;
                //Animates the UI using LeanTween
                LeanTween.scale(instructions, new Vector2(0.0f, 0.0f), 0.80f)
                    .setEaseInOutQuad();
                LeanTween.scale(exitInstructionsButton, new Vector2(0.0f, 0.0f), 0.80f)
                    .setEaseInOutQuad();
                LeanTween.moveY(exitInstructionsButton, 0.1f, 1.1f)
                    .setEaseInOutQuad();
                break;
            case 3: //Enter Settings
                //Disables main menu buttons
                inWindow = true;
                LeanTween.scale(settings, new Vector2(1.7835f, 2.052f), 0.80f)
                    .setEaseInOutQuad();
                break;
            case 4: //Exit Settings
                //Enables main menu buttons
                inWindow = false;
                //Removes Settings window
                LeanTween.scale(settings, new Vector2(0, 0), 0.80f)
                    .setEaseInOutQuad();
                break;
            case 5: //quit
                //Quits game (If viewing in Unity Editor)
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
                //Quits game (If viewing from exe)
                Application.Quit();
                break;
        }
    }

    void Update()
    {
        if (inWindow) //In active window (Settings,Instructions)
        {
            startButton.SetActive(false);
            exitButton.SetActive(false);
            instructionsButton.SetActive(false);
            settingsButton.SetActive(false);
        }

        else if (!inWindow) //Not in active window (Settings, Instructions)
        {
            startButton.SetActive(true);
            exitButton.SetActive(true);
            instructionsButton.SetActive(true);
            settingsButton.SetActive(true);
        }
    }

    public void ChangeMusicVolume(float _val)
    {

        AudioSystem.instance.MusicSource.volume = _val;
        GameManager.instance.musicVolume = _val;
    }

    public void ChangeSoundVolume(float _val)
    {
        AudioSystem.instance.SoundSource.volume = _val;
        GameManager.instance.soundVolume = _val;
    }
}