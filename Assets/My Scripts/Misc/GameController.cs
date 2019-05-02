using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Canvas loseCanvas;
    [SerializeField] Canvas winCanvas;
    [SerializeField] Canvas pauseCanvas;
    private Coroutine routine;
    public SliderManager sliderManager;
    public static GameController instance;
    bool pauseMenuActive;

    

    private void Awake()
    {
        loseCanvas.enabled = false;
        pauseCanvas.enabled = false;
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
        routine = sliderManager.randomEventRoutine;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {


                pauseMenuActive = true;
                pauseCanvas.enabled = true;
                Time.timeScale = 0;
                Debug.Log("Paused");
                AudioManager.instance.PlayLoop(AudioManager.EventType.MuteAll);
          


        }

    }
    public void Resume()
    {
        pauseCanvas.enabled = false;
        Time.timeScale = 1;
        AudioManager.instance.TryStopLoop(AudioManager.EventType.MuteAll);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialTest");
        Time.timeScale = 1;
        AudioManager.instance.TryStopLoop(AudioManager.EventType.MuteAll);
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        AudioManager.instance.TryStopLoop(AudioManager.EventType.MuteAll);
        AudioManager.instance.TryStopLoop(AudioManager.EventType.ShipIntegrityDamage);
        AudioManager.instance.TryStopLoop(AudioManager.EventType.EngineSound);
        AudioManager.instance.TryStopLoop(AudioManager.EventType.CounterMeasuresAutomated, FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        AudioManager.instance.TryStopLoop(AudioManager.EventType.MuteAll);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
        AudioManager.instance.TryStopLoop(AudioManager.EventType.EngineSound);
        AudioManager.instance.TryStopLoop(AudioManager.EventType.CounterMeasuresAutomated);
        loseCanvas.enabled = true;

    }

    public void Win()
    {
        Debug.Log("Win");
        winCanvas.enabled = true;
        StopCoroutine(sliderManager.randomEventRoutine);
        AudioManager.instance.TryStopLoop(AudioManager.EventType.ShipIntegrityDamage);
        AudioManager.instance.TryStopLoop(AudioManager.EventType.EngineSound);
        AudioManager.instance.TryStopLoop(AudioManager.EventType.CounterMeasuresAutomated);
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
