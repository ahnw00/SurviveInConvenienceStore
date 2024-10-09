using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide : MonoBehaviour
{
    public Image bug;
    public GameObject Bug;
    bool isOnCoroutine;
    public bool OnHide;
    //public Button btn;..
    SoundManager soundManager;
    public float WaitNum = 2;
    public Text NumTxt;

    // Start is called before the first frame update
    void Start()
    {
        bug = Bug.GetComponent<Image>();  
        soundManager = SoundManager.inst;
        //Button btn = GameObject.Find("HideButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeInOut() {
        isOnCoroutine = true;
        StartCoroutine(HideBtnTimer());
        Color c = bug.GetComponent<Image>().color;
        c.a = 0;
        bug.GetComponent<Image>().color = c;
        OnHide = true;
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < 10; i++){
            float f = i / 10.0f;
            c.a = f;
            bug.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.05f);
        }
        isOnCoroutine = false;
        OnHide = false;
    }

    IEnumerator HideBtnTimer()
    {
        NumTxt.text = WaitNum.ToString();
        while (WaitNum >= 0)
        {
            yield return null;
            WaitNum -= Time.deltaTime;
            NumTxt.text = WaitNum.ToString("F1");
        }
        NumTxt.gameObject.SetActive(false);
        WaitNum = 2;
        GetComponent<Button>().interactable = true;
    }

    public void HideOnOff() {
        soundManager.HideEffectPlay();
        if(!isOnCoroutine) {
            StartCoroutine("FadeInOut");
            NumTxt.gameObject.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
    }

}
