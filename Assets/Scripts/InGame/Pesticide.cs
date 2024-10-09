using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pesticide : MonoBehaviour
{
    public GameObject EndPopUp;

    public RaycastHit2D hit_;
    float findDistance = 20f;
    Vector2 handPosition;

    GameManager gameManager;
    Hide hide;
    SoundManager soundManager;

    void Start()
    {
        handPosition = this.gameObject.GetComponent<RectTransform>().position;
        gameManager = FindObjectOfType<GameManager>();
        hide = FindObjectOfType<Hide>();
        soundManager = SoundManager.inst;

    }

    public void kill()
    {
        handPosition = this.gameObject.GetComponent<RectTransform>().position;
        Debug.DrawRay(handPosition + new Vector2(0, 10), new Vector3(0, 0, 1) * findDistance, Color.red, 5f);
        hit_ = Physics2D.Raycast(handPosition + new Vector2(0, 10), new Vector3(0, 0, 1), findDistance);

        if ((hit_ == true) && (hide.OnHide == false) && (hit_.collider.tag == "Bug")) 
        {
            //죽는 지점
            gameManager.dieText.text = "벌레가 짝 소리에 쿵 짝 쿵 짝 춤을 추다 잡혔습니다.";
            //Debug.Log("GameOver");
            gameManager.resultText.text = gameManager.resultScore.ToString();
            gameManager.resultText2.text = gameManager.resultScore.ToString();
            gameManager.GameOver();
        }
    }

    public void KillSound()
    {
        soundManager.ClapEffectPlay();
    }

    public void HandSound()
    {
        soundManager.HandEffectPlay();
    }

    public void FootStepSound()
    {
        soundManager.FootStepEffectPlay();
    }

    public void HmmSound()
    {
        SnackCrasher snackCrasher = FindObjectOfType<SnackCrasher>();
        //Debug.Log(gameManager.numOfSnackDestroyed);
        if(gameManager.numOfSnackDestroyed == 1)
        {
            soundManager.HmmEffectPlay();
        }
    }
}
    
