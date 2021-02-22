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
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳에 처음 왔구나?:2" }); //대화 하나에 여러 문장
        talkData.Add(2000, new string[] { "난 이방인이 제일 싫어.:3", "넌 뭐야?:1" });
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "예쁜 꽃이다." });
        
        portraitData.Add(1000 + 0, portraitArr[0]); //0 Idle
        portraitData.Add(1000 + 1, portraitArr[1]); //1 Talk
        portraitData.Add(1000 + 2, portraitArr[2]); //2 Smile
        portraitData.Add(1000 + 3, portraitArr[3]); //3 Angry

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
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
