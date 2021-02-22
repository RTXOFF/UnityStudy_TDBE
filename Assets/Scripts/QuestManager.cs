using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //퀘스트 대화 순서 변수
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("금화를 구하기", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("모든 퀘스트를 완수했다!", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //Next Talk target
        if(id == questList[questId].npcId[questActionIndex]) //GenerateData 함수 안의 1000.
            questActionIndex++;

        //Control Quest Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();
        //Quest Name
        return questList[questId].questName;
    }

    public string CheckQuest() //First Quest Name 오버로딩 : 같은 함수명 다른 매개변수!
    {
        //Quest Name
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if(questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}