using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainScene : MonoBehaviour
{
    public void GotoIngameScene()
    {
        SceneManager.LoadScene("Ingame");
    }

    public void GotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OutGame()
    {
        Application.Quit();
        //Debug.Log("OutGame");
    }
    
}
