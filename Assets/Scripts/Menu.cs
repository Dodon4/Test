using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject panel;
    public AudioMixer audioMixer;
    public Text textAudio;
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SoundPanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        textAudio.text = "Звук: " + (100 - Mathf.Abs(Mathf.RoundToInt(volume / 80 * 100))) + "%";
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
