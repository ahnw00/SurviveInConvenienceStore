using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BackSound : MonoBehaviour
{
    SoundManager soundManager;
    DataManager dataManager;
    SaveDataClass saveData;
    public Slider bgmSlider;
    public Text bgmText;
    int bgmVolume;

    void Start()
    {
        soundManager = SoundManager.inst;
        dataManager = DataManager.singleTon;
        saveData = dataManager.saveData;

        bgmSlider.value = soundManager.BGMvolume;
    }
    
    public void SetBgmVolume()
    {
        soundManager.bgmSource.volume = bgmSlider.value;

        soundManager.BGMvolume = bgmSlider.value;
        saveData.BGMvolume = bgmSlider.value;

        bgmVolume = (int)(bgmSlider.value * 100f);
        bgmText.text = bgmVolume.ToString();
        dataManager.Save();
    }
}
