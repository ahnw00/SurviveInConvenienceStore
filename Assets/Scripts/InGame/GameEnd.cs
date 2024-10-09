using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    SoundManager soundManager;
    private void Start()
    {
        soundManager = SoundManager.inst;
    }
    public void SceneChangeEnd()
    {
        Time.timeScale = 1f;
        soundManager.MainBGMPlay();
        SceneManager.LoadScene("MainScene");
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        soundManager.IngameBGMPlay();
        SceneManager.LoadScene("Ingame");
    }

}
