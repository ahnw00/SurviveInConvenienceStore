using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackCrasher : MonoBehaviour
{
    Move data;
    GameManager gameManager;
    SoundManager soundManager;

    public int hitCount = 0; //test용


    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<Move>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = SoundManager.inst;
    }

    int[,] touchNum = { { 1, 1, 2, 2 }, { 2, 2, 3, 3 }, { 1, 2, 3, 3 } };   //몇번 눌러야 과자가 부셔지는지
    int index = 0; 
    public void Smash()
    {
        if (data.hit.transform == gameObject.transform && data.hit == true && data.hit.collider.tag == "Obstacle")
        {
            
            if(hitCount == 0)
            {
                //Obstacle_1 이름에서 _뒤에 있는 숫자를 뽑아온다
                index = name.LastIndexOf('_');
                string indexString;

                if (index >= 0)
                {
                    indexString = this.name.Substring(index + 1, 1);
                }
                else return;

                //Debug.Log(indexString);
                index = int.Parse(indexString);
            }

            hitCount++; //때린횟수 증가

            switch (gameManager.stage)
            {
                case 0: soundManager.SnackTouchEffectPlay(); break;
                case 2: 
                {
                        if (index == 0 || index == 3)
                            soundManager.CanTouchEffectPlay();
                        else if (index == 1 || index == 2)
                            soundManager.BottleTouchEffectPlay();
                        break;
                }
                default: break;
            }
            

            if(hitCount == touchNum[gameManager.stage, index] + (int)gameManager.incVal) //때린횟수 도달!
            {
                gameManager.numOfSnackDestroyed++;
                gameManager.PutYourHandsUp();
                Destroy(this.gameObject);
                hitCount = 0;

                switch (gameManager.stage)
                {
                    case 0: soundManager.SnackFallEffectPlay(); break;
                    case 2:
                        {
                            if (index == 0 || index == 3)
                                soundManager.CanFallEffectPlay();
                            else if (index == 1 || index == 2)
                                soundManager.BottleFallEffectPlay();
                            break;
                        }
                    default: break;
                }
            }
            
        }

    }
}
