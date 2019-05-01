using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Canvas loseCanvas;
    [SerializeField] Canvas winCanvas;
    public SliderManager sliderManager;
    public static GameController instance;

    

    private void Awake()
    {
        loseCanvas.enabled = false;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        AudioManager.instance.PlayLoop(AudioManager.EventType.EngineSound);
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
        AudioManager.instance.StopLoops(AudioManager.EventType.EngineSound);
        AudioManager.instance.StopLoops(AudioManager.EventType.CounterMeasuresAutomated);
        StopCoroutine(sliderManager.randomEventRoutine);
        loseCanvas.enabled = true;

    }

    public void Win()
    {
        Debug.Log("Win");
        winCanvas.enabled = true;
        AudioManager.instance.StopLoops(AudioManager.EventType.EngineSound);
        AudioManager.instance.StopLoops(AudioManager.EventType.CounterMeasuresAutomated);
        StopCoroutine(sliderManager.randomEventRoutine);

    }
    public void RestartGame()
    {
        loseCanvas.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();

    }

}
