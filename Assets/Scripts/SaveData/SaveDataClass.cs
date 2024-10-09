using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataClass
{
    public int bestScore;
    public float BGMvolume;
    public float EffectVolume;

    public SaveDataClass()
    {
        bestScore = 0;
        BGMvolume = 0.5f;
        EffectVolume = 0.5f;
    }
}
