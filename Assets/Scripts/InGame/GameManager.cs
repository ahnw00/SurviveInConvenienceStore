using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle0;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject drink0;
    public GameObject drink1;
    public GameObject drink2;
    public GameObject drink3;
    public GameObject waterzone0;
    public GameObject waterzone1;

    public GameObject parent;
    public GameObject hand;
    public GameObject background0;
    public GameObject background1;
    List<GameObject> backGroundList;

    public Sprite backGround1;
    public Sprite backGround2;
    public Sprite backGround3;
    List<Sprite> spriteList;
    int stageOrder = 0;


    Move data;
    int[,] obstacleOrder;
    int order;
    int numOfObstacles;
    GameObject nowBackground;
    GameObject nextBackground;
    GameObject tmpBackground;

    public GameObject tempWater1;
    public GameObject tempWater2;

    public int stage = 0; // 0스테이지 : 과자봉지, 1스테이지 : 뜨거운물, 2스테이지 : 냉장고
    public int stageNum = 3; // 스테이지 수
    public int resultScore = 0;
    public Timer timer;
    public GameObject EndPopUp;
    public Text resultText;
    public Text resultText2;
    public GameObject dyingPanel;
    public Text dieText;
    public Text bestText;
    public GameObject spirit;
    public Sprite deadImage;

    DataManager dataManager;
    SaveDataClass saveData;
    SoundManager soundManager;
    SnackCrasher snackCrasher;
    public int numOfSnackDestroyed;

    public float decVal = 1f;
    public float decVal2 = 1f;
    public float incVal = 0f;

    void Start()
    {
        dataManager = DataManager.singleTon;
        saveData = dataManager.saveData;
        soundManager = SoundManager.inst;

        spriteList = new List<Sprite>();
        spriteList.Add(backGround1);
        spriteList.Add(backGround2);
        spriteList.Add(backGround3);

        data = FindObjectOfType<Move>();
        timer = FindObjectOfType<Timer>();
        snackCrasher = FindObjectOfType<SnackCrasher>();
        numOfObstacles = 5;
        obstacleOrder = new int[5, 5]{ {1, 1, 1, 1, 5}, {1, 1, 1, 2, 4}, {1, 1, 1 ,3, 3}, {1, 1, 2, 2, 3}, {1, 2, 2, 2, 2} };
        StartCoroutine(InGameCoroutine());
        nowBackground = background0;
        nextBackground = background1;
    }



    IEnumerator InGameCoroutine()
    {
        numOfSnackDestroyed = 0;
        if(stage == 0)
            soundManager.IngameBGMPlay();
        if (stage == 2)
        {
            timer.slider.gameObject.SetActive(true);
            timer.StartCoroutine("CheckTime");
            soundManager.TimerBGMPlay(); //sound추가
        }
        else if (timer.slider != null) { 
            timer.slider.gameObject.SetActive(false);
        }
        
        order = 0;
        ShuffleList(obstacleOrder);
        int randomNum = Random.Range(0, 5);
        
        for(int i = 0; i < 5; i++)
        {
            GameObject obstacleObj = GetObstacle(stage);

            if (i != 0)
            {
                order += obstacleOrder[randomNum, i - 1];
            }
            
            obstacleObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-864 + 192 * (order + 1), -187);
        }
        data.Bug.GetComponent<RectTransform>().anchoredPosition = new Vector2(-864, -245);
        

        while (true)
        {
            yield return null;

            if(data.Bug.GetComponent<RectTransform>().anchoredPosition.x == 864)
            {
                timer.StopCoroutine("CheckTime");
                yield return new WaitForSeconds(2f);
                timer.mask.SetActive(false);
                StartCoroutine(ChangeBackground());
                if(stage == 0 || stage == 2)
                {
                    Destroy(tempWater1);
                    Destroy(tempWater2);
                    moreHarder();
                    StartCoroutine(InGameCoroutine());
                }
                else if(stage == 1)
                {
                    Destroy(tempWater1);
                    Destroy(tempWater2);
                    data.Bug.GetComponent<RectTransform>().anchoredPosition = new Vector2(-864, -245);
                    tempWater1 = GetObstacle(stage);
                    tempWater1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480, -260);
                    tempWater2 = GetObstacle(stage);
                    tempWater2.GetComponent<RectTransform>().anchoredPosition = new Vector2(388, -260);

                    timer.slider.gameObject.SetActive(true);
                    timer.StartCoroutine("CheckTime");
                    soundManager.TimerBGMPlay(); //sound추가
                    moreHarder();
                    StartCoroutine(waterCoroutine());
                }
                
                break;
            }
        }
    }

    public void PutYourHandsUp()
    {
        hand.GetComponent<RectTransform>().anchoredPosition 
        = new Vector2(data.hit.collider.gameObject.GetComponent<RectTransform>().anchoredPosition.x, hand.GetComponent<RectTransform>().anchoredPosition.y);
        hand.gameObject.SetActive(true);
    }

    IEnumerator waterCoroutine()
    {
        float timerr = 0f;
        StartCoroutine(waitingKillCoroutine());

        soundManager.WaterFallEffectPlay();
        tempWater1.transform.GetChild(1).GetComponent<Animator>().SetTrigger("WaterTrigger");
        tempWater2.transform.GetChild(1).GetComponent<Animator>().SetTrigger("WaterTrigger");
        while(timerr < 3.5f)
        {
            yield return null;
            timerr += Time.deltaTime;
            if(timerr < 2.0f)
            {
                float xPos = data.Bug.GetComponent<RectTransform>().anchoredPosition.x;
                //죽는 지점
                if((xPos >= -672 && xPos <= -288) || (xPos >= 96 && xPos <= 480))
                {
                    dieText.text = "벌레가 타오르는 뜨거움으로 그만 정신을 놓았습니다.";
                    //Debug.Log("gameOver");
                    resultText.text = resultScore.ToString();
                    resultText2.text = resultScore.ToString();
                    GameOver();
                }
            }
        }
            
        if(data.Bug.GetComponent<RectTransform>().anchoredPosition.x != 864)
        {
            StartCoroutine(waterCoroutine());
        }
        if(data.Bug.GetComponent<RectTransform>().anchoredPosition.x == 864)
        {
            while (true)
            {
                timer.StopCoroutine("CheckTime");
                yield return new WaitForSeconds(2f);
                timer.mask.SetActive(false);
                StartCoroutine(ChangeBackground());
                if(stage == 0 || stage == 2)
                {
                    Destroy(tempWater1);
                    Destroy(tempWater2);
                    moreHarder();
                    StartCoroutine(InGameCoroutine());
                }
                else if(stage == 1)
                {
                    Destroy(tempWater1);
                    Destroy(tempWater2);
                    data.Bug.GetComponent<RectTransform>().anchoredPosition = new Vector2(-864, -245);
                    tempWater1 = GetObstacle(stage);
                    tempWater1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480, -260);
                    tempWater2 = GetObstacle(stage);
                    tempWater2.GetComponent<RectTransform>().anchoredPosition = new Vector2(388, -260);

                    timer.slider.gameObject.SetActive(true);
                    timer.StartCoroutine("CheckTime");
                    soundManager.TimerBGMPlay(); //sound추가
                    moreHarder();
                    StartCoroutine(waterCoroutine());
                }
                
                break;
            }
        } 
    }

    IEnumerator waitingKillCoroutine()
    {
        float time = 0f;
        while(true)
        {
            yield return null;
            bool tempBool = data.toggle;
            time += Time.deltaTime;
            if(data.Bug.GetComponent<RectTransform>().anchoredPosition.x != 864)
            {
                if(tempBool != data.toggle)
                {
                    StartCoroutine(waitingKillCoroutine());
                    break;
                }
                else if(time >= 3.0f * decVal)
                {
                    hand.GetComponent<RectTransform>().anchoredPosition 
            = new Vector2(data.Bug.GetComponent<RectTransform>().anchoredPosition.x, hand.GetComponent<RectTransform>().anchoredPosition.y);
                    hand.SetActive(true);
                    StartCoroutine(waitingKillCoroutine());
                    break;
                }
            }
            if(data.Bug.GetComponent<RectTransform>().anchoredPosition.x == 864)
            {
                timer.StopCoroutine("CheckTime");
                break;
            }
        }
    }

    IEnumerator ChangeBackground()
    {
        int randomNum = Random.Range(0, 3);
        //Debug.Log("랜덤 스테이지: " + randomNum);
        stage = randomNum;
        nextBackground.GetComponent<Image>().sprite = spriteList[stage];
        

        nextBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(1920f, 0f);
        float lerpTime = 0;
        float waitTime = 2f;
        
        while (lerpTime < waitTime)
        {
            nowBackground.GetComponent<RectTransform>().anchoredPosition 
            = Vector2.Lerp(nowBackground.GetComponent<RectTransform>().anchoredPosition, new Vector2(-1920f, 0f), (lerpTime / waitTime));
            nextBackground.GetComponent<RectTransform>().anchoredPosition 
            = Vector2.Lerp(nextBackground.GetComponent<RectTransform>().anchoredPosition, new Vector2(0f, 0f), (lerpTime / waitTime));
            lerpTime += Time.deltaTime;

            yield return null;
        }

        tmpBackground = nowBackground;
        nowBackground = nextBackground;
        nextBackground = tmpBackground;
    }

    public void ShuffleList(int[,] array)
    {
        System.Random prng = new System.Random();
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                int randomIndex = prng.Next(i, 5);
                int temp = array[j, randomIndex];
                array[j, randomIndex] = array[j, i];
                array[j, i] = temp;
            }
        }
    }

    //스테이지에 따른 장애물 프리펩 변경
    public GameObject GetObstacle(int stage)
    {
        //랜덤한 과자 프리펩을 불러온다
        int randomObj = Random.Range(0, 4);
        int randomTwo = Random.Range(0, 2);
        GameObject obstacleObj;

        if(stage == 0)
        {
            switch (randomObj)
            {
                case 0: obstacleObj = Instantiate(obstacle0, parent.transform); break;
                case 1: obstacleObj = Instantiate(obstacle1, parent.transform); break;
                case 2: obstacleObj = Instantiate(obstacle2, parent.transform); break;
                case 3: obstacleObj = Instantiate(obstacle3, parent.transform); break;
                default: obstacleObj = Instantiate(obstacle0, parent.transform); break;
            }
        }
        else if(stage == 1)
        {
            switch (randomTwo)
            {
                case 0: obstacleObj = Instantiate(waterzone0, parent.transform); break;
                case 1: obstacleObj = Instantiate(waterzone1, parent.transform); break;
                default: obstacleObj = Instantiate(waterzone0, parent.transform); break;
                
            }
        }
        else if(stage == 2)
        {
            switch (randomObj)
            {
                case 0: obstacleObj = Instantiate(drink0, parent.transform); break;
                case 1: obstacleObj = Instantiate(drink1, parent.transform); break;
                case 2: obstacleObj = Instantiate(drink2, parent.transform); break;
                case 3: obstacleObj = Instantiate(drink3, parent.transform); break;
                default: obstacleObj = Instantiate(drink0, parent.transform); break;
            }
        }
        else
        {
            obstacleObj = null;
        }
        

        return obstacleObj;
    }

    public void GameOver()
    {
        soundManager.DieEffectPlay();
        data.Bug.GetComponent<Image>().sprite = deadImage;
        spirit.SetActive(true);
        dyingPanel.SetActive(true);
        timer.StopAllCoroutines();
        StopAllCoroutines();
        StartCoroutine(termUntilDie());
    }

    void moreHarder()
    {
        decVal *= 0.95f;
        if(decVal2 > 0.866f)
        {
            decVal2 *= 0.99f;
        }
        incVal += 0.3f;
    }

    IEnumerator termUntilDie()
    {
        float timer = 0;
        float limit = 2f;
        while(timer < limit)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        Time.timeScale = 0f;
        if(resultScore > saveData.bestScore)
        {
            bestText.gameObject.SetActive(true);
            resultText.color = Color.red;
            soundManager.BestOverBGMPlay();
            saveData.bestScore = resultScore;
            dataManager.Save();
        }
        else
        {
            resultText.color = Color.black;
            soundManager.GameOverBGMPlay();
        }
        dyingPanel.SetActive(false);
        EndPopUp.gameObject.SetActive(true);  
    }
}
