using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    SoundManager soundManager;

    void Start()
    {
        soundManager = SoundManager.inst;
    }

    public void Button1SoundPlay()
    {
        soundManager.Button1Play();
    }
    public void Button2SoundPlay()
    {
        soundManager.Button2Play();
    }
    public void HideSoundPlay()
    {
        soundManager.HideEffectPlay();
    }
    public void MoveSoundPlay()
    {
        soundManager.MoveEffectPlay();
    }
}
