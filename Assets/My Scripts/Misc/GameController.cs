using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Canvas loseCanvas;
    [SerializeField] Canvas winCanvas;
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
        AudioManager.instance.StopLoop(AudioManager.EventType.EngineSound);
        loseCanvas.enabled = true;

    }

    public void Win()
    {
        Debug.Log("Win");
        winCanvas.enabled = true;
        AudioManager.instance.StopLoop(AudioManager.EventType.EngineSound);

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
