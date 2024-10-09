using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider slider;
    float time;

    public GameObject EndPopUp;
    public Text resultText;
    GameManager gameManager;
    public GameObject FogMask;
    public GameObject FrostMask;
    public GameObject mask;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        time = 15;
    }

    public IEnumerator CheckTime()
    {
        bool isUnderTwoThird = false;
        bool isUnderOneThird = false;
        
        slider.value = 1; time = 15 * gameManager.decVal2;
        while (time > 0)
        {
            yield return null;
            time -= Time.deltaTime;
            
            slider.value = time / (15 * gameManager.decVal2);

            if (slider.value < 0.6666f) 
            {
                //Debug.Log("enter");
                if(!isUnderTwoThird)
                {
                    isUnderTwoThird = true;
                    if(gameManager.stage == 1)
                    {
                        FogMask.SetActive(true);
                        mask = FogMask;
                    }
                    else
                    {
                        FrostMask.SetActive(true);
                        mask = FrostMask;
                    }
                }

                if (slider.value < 0.3333f)
                {
                    if(!isUnderOneThird)
                    {
                        isUnderOneThird = true;
                        if(gameManager.stage == 1)
                        {
                            FogMask.GetComponent<Animator>().SetTrigger("NextMask");
                        }
                        else
                        {
                            FrostMask.GetComponent<Animator>().SetTrigger("NextMask");
                        }
                    }   
                }
            }
            //isRunning = false;
        }
        //죽는 지점
        if(gameManager.stage == 1) // 뜨거운 물
        {
            gameManager.dieText.text = "벌레가 사람들에게 들켜 그만 굳어버리고 말았습니다.";
        }
        else // 냉장고
        {
            gameManager.dieText.text = "벌레가 너무 추워 저체온증에 걸리고 기절했습니다.";
        }
        //Debug.Log("GameOver");
        gameManager.resultText.text = gameManager.resultScore.ToString();
        gameManager.resultText2.text = gameManager.resultScore.ToString();
        gameManager.GameOver();
    }
}
