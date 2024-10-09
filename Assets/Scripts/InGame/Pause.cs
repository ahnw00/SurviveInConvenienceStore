using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void ClickPauseButton()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0f;
    }
    public void ClickResumeButton()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1f;
    }
}
