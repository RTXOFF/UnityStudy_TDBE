using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public GameObject andcursor;
    public bool isAnim;
    public int CharPerSeconds;

    AudioSource audioSource;
    Text msgText;

    string targetMsg;
    float interval;
    int index;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if(isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    //애니메이션을 코드로 처리할 때, <Start, Ing, End>로 정리하자.
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        andcursor.SetActive(false);

        isAnim = true;

        //Start Animation
        interval = 1.0f / CharPerSeconds;
        Invoke("EffectIng", interval);
    }

    void EffectIng()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; //문자열도 배열처럼 char 값에 접근 가능

        //Sound
        if (targetMsg[index] != ' ' || targetMsg[index] != '.') //공백과 마침표는 사운드 제외
            audioSource.Play();

        index++;

        //Recursive
        Invoke("EffectIng", interval);
    }

    void EffectEnd()
    {
        isAnim = false;
        andcursor.SetActive(true);
    }
}
