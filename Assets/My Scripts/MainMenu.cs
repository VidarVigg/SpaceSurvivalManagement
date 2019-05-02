using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        AudioManager.instance?.PlayOneShot(AudioManager.EventType.ButtonSound);
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }
    public void StartTutorial()
    {
        AudioManager.instance?.PlayOneShot(AudioManager.EventType.ButtonSound);
        SceneManager.LoadScene("TutorialTest");
        Time.timeScale = 1;
        AudioManager.instance?.TryStopLoop(AudioManager.EventType.MuteAll);
    }
    public void Quit()
    {
        AudioManager.instance?.PlayOneShot(AudioManager.EventType.ButtonSound);
        Application.Quit();

    }


}
