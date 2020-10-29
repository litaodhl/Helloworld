using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSystem //不用继承MOno 使用静态工具
{
    //处理得分的消息
    //（1）第一步：定义一个委托 无返回值无参数的委托方法
    public delegate void ScoreChangeHandle();
    //实例化 用ScoreChange 容器 接收 方法
    static public event ScoreChangeHandle ScoreChange;

    //定义一个方法，可以在外部进行调用。
    static public void RaiseScoreChange()
    {
        if (ScoreChange != null)
        {
            ScoreChange();
        }
    }

    //新邮件
    //购买道具成功
    //捡到Boss掉落物品
    //有人加你好友
}
