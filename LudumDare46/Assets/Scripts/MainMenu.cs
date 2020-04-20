using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = GetMasterLevel();
    }
    public void Quit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public float GetMasterLevel()
    {
        float value;
        bool result = audioMixer.GetFloat("volume", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }
}
