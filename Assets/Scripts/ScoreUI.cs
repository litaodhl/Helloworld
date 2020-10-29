using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    //维护分数
    Text _score;

    //实例化方法
    public void Inst()
    {
        _score = transform.Find("value").GetComponent<Text>();
        //（2）委托事件第二步：UI实例化时注册事件
        EventSystem.ScoreChange += ShowScore;
    }
    //界面显式方法
    void ShowScore()
    {
        _score.text = GameMgr.Instance.Score.ToString();
    }
}
