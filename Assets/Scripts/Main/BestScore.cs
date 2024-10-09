using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    GameManager gameManager;
    DataManager dataManager;
    SaveDataClass saveData;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dataManager = DataManager.singleTon;
        saveData = dataManager.saveData;

        this.GetComponent<Text>().text = "최고의" + "\n" + "여름 피서 : " + saveData.bestScore.ToString() + "분";
    }
}
