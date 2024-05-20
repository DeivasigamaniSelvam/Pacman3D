using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class uicontroller : MonoBehaviour
{
    public int lifecount = 3,progressscount = 0;
    public Text LifecountText, ProgressText;
    public static uicontroller instance;
    public GameObject LevelCompleted, LevelFailed, Levelstart,status,restart;
    void Start()
    {
        instance = this;
    }

    public void updatelife()
    {
        lifecount--;
        LifecountText.text = "Life : " +lifecount.ToString();
        if(lifecount <= 0)
        {
            playercontroller.instance.gamestate = playercontroller.GameState.gameend;
            LevelFailed.SetActive(true);
        }
    }
    public void updateprogress()
    {
        progressscount++;
        ProgressText.text = "Progress : " + (progressscount/2).ToString() + " / 80 % ";
        if(progressscount >= 160)
        {
            LevelCompleted.SetActive(true);
            playercontroller.instance.gamestate = playercontroller.GameState.gameend;
        }
    }
    public void showLevelCompleted()
    {
        LevelCompleted.SetActive(true);
    }
    public void showLevelFailed()
    {
        LevelFailed.SetActive(true);
    }
    public void nextgame()
    {
        SceneManager.LoadScene(0);
    }
    public void startgame()
    {
        Levelstart.SetActive(false);
        playercontroller.instance.gamestate = playercontroller.GameState.gamestart;
        status.SetActive(true);
    }
    public void settings()
    {
        if (playercontroller.instance.gamestate != playercontroller.GameState.gameend)
        {
            restart.SetActive(true);
            playercontroller.instance.gamestate = playercontroller.GameState.gameend;
        }
    }
    public void cancel()
    {
        restart.SetActive(false);
        playercontroller.instance.gamestate = playercontroller.GameState.gamestart;
    }
}
