using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text talkText;
    public GameObject talkPanel;
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if(isAction) //Exit Action
            isAction = false;
        else //Enter Action
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = scanObj.name + "이다.";
        }

        talkPanel.SetActive(isAction);
    }
}