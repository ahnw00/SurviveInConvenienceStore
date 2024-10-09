using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EffectSound : MonoBehaviour
{
    SoundManager soundManager;
    DataManager dataManager;
    SaveDataClass saveData;
    public Slider effectSlider;
    public Text effectText;
    int effectVolume;

    void Start()
    {
        soundManager = SoundManager.inst;
        dataManager = DataManager.singleTon;
        saveData = dataManager.saveData;

        effectSlider.value = soundManager.EffectVolume;
    }
    
    public void SetEffectVolume()
    {
        soundManager.buttonSource.volume = effectSlider.value; 
        soundManager.obstacleEffectSource.volume = effectSlider.value; 
        soundManager.bugEffectSource.volume = effectSlider.value; 
        soundManager.personEffectSource.volume = effectSlider.value; 

        soundManager.EffectVolume = effectSlider.value;
        saveData.EffectVolume = effectSlider.value;

        effectVolume = (int)(effectSlider.value * 100f);
        effectText.text = effectVolume.ToString();
        dataManager.Save();
    }
}
