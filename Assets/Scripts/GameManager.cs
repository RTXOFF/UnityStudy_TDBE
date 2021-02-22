using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Text talkText;
    public GameObject talkPanel;
    public GameObject scanObject;
    public Image portraitImg;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {

        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        //Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        //End Talk
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0; //대화가 끝났으니 0으로 초기화
            Debug.Log(questManager.CheckQuest(id));
            return; //void 함수에서 return은 강제 종료 역할
        }
        //Continue Talk
        if(isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkManager.GetPortarit(id, int.Parse(talkData.Split(':')[1]));
            //int.Parse() : 문자열을 해당 타입으로 변환해주는 함수.
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}