using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Canvas loseCanvas;
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
        //string gameOver = "The Ship was destroyed";
        //return gameOver;
    }
    public void RestartGame()
    {
        loseCanvas.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Debug.Log("Quit");
    }

}
