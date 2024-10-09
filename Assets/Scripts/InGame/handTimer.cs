using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handTimer : MonoBehaviour
{
    float timer = 0f;
    float cutLine;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    IEnumerator handCoroutine()
    {
        this.gameObject.GetComponent<Animator>().speed = 0f;
        // 물 시간
        cutLine = Random.Range(0.8f * gameManager.decVal, 1.5f * gameManager.decVal);
        //Debug.Log(cutLine);

        while(timer < cutLine)
        {
            yield return null;
            timer += Time.deltaTime;
            //Debug.Log(timer);
        }
        this.gameObject.GetComponent<Animator>().speed = 1f;
        timer = 0f;
    }

    public void handPause()
    {
        StartCoroutine(handCoroutine());
    }
}
