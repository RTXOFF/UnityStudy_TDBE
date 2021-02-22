using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public TypeEffect talk;
    public Animator talkPanel;
    public Animator portraitAnim;
    public GameObject scanObject;
    public GameObject menuSet;
    public GameObject player;
    public Image portraitImg;
    public Sprite prevPortrait;
    public Text questText;

    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    private void Update()
    {

        if(Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
    }

    public void Action(GameObject scanObj)
    {

        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        //Set Talk Data
        int questTalkIndex;
        string talkData;

        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        //End Talk
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0; //대화가 끝났으니 0으로 초기화
            questText.text = questManager.CheckQuest(id);
            return; //void 함수에서 return은 강제 종료 역할
        }
        //Continue Talk
        if(isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]); 

            //Show Portrait
            portraitImg.sprite = talkManager.GetPortarit(id, int.Parse(talkData.Split(':')[1]));
            //int.Parse() : 문자열을 해당 타입으로 변환해주는 함수.
            portraitImg.color = new Color(1, 1, 1, 1);
            
            //Animation Portrait
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);

            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

    public void GameSave()
    {
        //player X
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);

        //player Y
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);

        //Quest Id
        PlayerPrefs.SetInt("QuestId", questManager.questId);

        //Quest Action Index
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);

        //PlayerPrefs 기능 이용, Company, Product Name 으로 컴퓨터 레지스트리에 저장
        PlayerPrefs.Save();
        
        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector2(x, y);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit(); //게임 종료 함수, 에디터에서는 실행되지 않음
    }
}