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
    
    public void GameOver()
    {

        loseCanvas.enabled = true;

    }

    public void Win()
    {
        winCanvas.enabled = true;
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
