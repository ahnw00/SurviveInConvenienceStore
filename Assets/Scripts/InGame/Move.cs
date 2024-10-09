using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Move : MonoBehaviour
{
    Image bug;
    public RaycastHit2D hit;
    public GameObject Bug;
    float maxDistance = 9.6f;
    bool isBlocked;
    public bool toggle;
    Vector2 bugPosition;
    Vector2 bugAnchoredPosition;
    GameManager gameManager;
    public GameObject scoreText;

    public RaycastHit2D hitOpstacle;
    DataManager dataManager;
    SaveDataClass saveData;


    // Start is called before the first frame update
    void Start()
    {
        dataManager = DataManager.singleTon;
        saveData = dataManager.saveData;
        toggle = true;
        isBlocked = false;
        bug = Bug.GetComponent<Image>();
        bugAnchoredPosition = Bug.GetComponent<RectTransform>().anchoredPosition;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bugPos = Bug.transform.position + new Vector3(96f, 0, 0);
        Debug.DrawRay(bugPos, new Vector3(100, 0, 0), Color.green, 5f) ;
        hit = Physics2D.Raycast(bugPos, new Vector3(1, 0, 0), maxDistance);
        if (hit == true && hit.collider.tag == "Obstacle")
        {
            isBlocked = true;
            hitOpstacle = hit;
        }

        /*
        Debug.DrawRay(new Vector3(Bug.GetComponent<RectTransform>().position.x + 9.6f, Bug.GetComponent<RectTransform>().position.y, 0), new Vector3(1, 0, 0) * maxDistance, Color.blue, 5f);
        hit = Physics2D.Raycast(new Vector3(Bug.GetComponent<RectTransform>().position.x + 9.6f, Bug.GetComponent<RectTransform>().position.y, Bug.GetComponent<RectTransform>().position.z), new Vector3(1, 0, 0), maxDistance);

        if(hit == true && hit.collider.tag == "Obstacle")
        {
            isBlocked = true;
            hitOpstacle = hit;
        }*/
    }

    public void MoveOn()
    {
        bugAnchoredPosition = Bug.GetComponent<RectTransform>().anchoredPosition;
        if (hit == false)
        {
            if(bugAnchoredPosition.x < 864)
            {
                if(toggle)
                {
                    toggle = false;
                }
                else
                {
                    toggle = true;
                }
                Bug.GetComponent<RectTransform>().anchoredPosition = bugAnchoredPosition + new Vector2(192f, 0f);

                gameManager.resultScore += 1;
                scoreText.GetComponent<Text>().text = gameManager.resultScore.ToString();
                if(gameManager.resultScore > saveData.bestScore)
                {
                    scoreText.GetComponent<Text>().color = Color.red;
                }
            }
        }
    }
}
