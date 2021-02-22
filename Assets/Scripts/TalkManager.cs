using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; //Key - Value 한 쌍의 데이터 변수
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {

        //Talk Data
        //NPC    A : 1000,  B : 2000
        //Object Box : 100, Flower : 200
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳에 처음 왔구나?:2" }); //대화 하나에 여러 문장
        talkData.Add(2000, new string[] { "난 이방인이 제일 싫어.:3", "넌 뭐야?:1" });
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "예쁜 꽃이다." });

        //Quest Talk : 퀘스트번호 + NPC Id에 해당하는 데이터 작성
        talkData.Add(10 + 1000, new string[] {"어서와.:0", "이 마을의 전설을 아니?:1", "저 밑의 루도에게 가봐.:2"});
        talkData.Add(11 + 2000, new string[] { "뭐야 넌.:0", "전설을 듣고싶다고?:3", "일 좀 하나 하자.:1", "금화를 구해와.:1" });

        talkData.Add(20 + 1000, new string[] { "금화를 찾으라고 했다고?:1","호수 주변에서 하나 본 것 같아.:2"});
        talkData.Add(20 + 2000, new string[] { "돈이 없어서가 아니라고..:1" });
        talkData.Add(20 + 5000, new string[] { "루도가 원하는 것이다." });
        talkData.Add(21 + 2000, new string[] { "정말 구해왔구나?:2" });


        //Portrait Data
        //0 : Idle, 1 : Talk, 2 : Smile, 3 : Angry
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);

        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);

    }

    public Sprite GetPortarit(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(!talkData.ContainsKey(id)) //에외 처리
        {
            if(!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex); //Get First Talk
            //퀘스트 맨 처음 대사 마저 없을 때 기본 대사.
            else
                return GetTalk(id - id % 10, talkIndex); //Get First Quest Talk
            //해당 퀘스트 진행 중 순서 대사가 없을 때 대화순서 제거 후 재탐색 (퀘스트 맨 처음 대사)

        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];

        //변환 값이 있는 재귀함수는 return까지 꼭 써주기.
    }
}
