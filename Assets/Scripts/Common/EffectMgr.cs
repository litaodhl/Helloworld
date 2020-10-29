using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//特效管理类
public class EffectMgr : MonoBehaviour
{
     //使用字典容器来装特效数据；使用常量定义
     public const string playerDie = "playerDie";//玩家死亡
     public const string enemyDie = "enemyDie";//敌机死亡
     public const string asteroidDes = "asteroidDes";//陨石销毁
     //特效库
     private Dictionary<string, GameObject> EffectGroup = new Dictionary<string, GameObject>();
    GameObject _tempEff;
    //实例化方法,在GameMgr调用
   public void Inst()
    {
        EffectGroup.Clear();
        _tempEff = Resources.Load("Explosions/explosion_player")as GameObject;
        EffectGroup.Add(playerDie, _tempEff);
        _tempEff = Resources.Load("Explosions/explosion_enemy") as GameObject;
        EffectGroup.Add(enemyDie, _tempEff);
        _tempEff = Resources.Load("Explosions/explosion_asteroid") as GameObject;
        EffectGroup.Add(asteroidDes, _tempEff);

    }
    //创建
    GameObject _eff;

    public void BuildEffect(string effname, Vector3 pos)//播放什么，播放位置在哪里
    {
        if (EffectGroup.ContainsKey(effname))//如果库中有，就实例化
        {
            _eff = Instantiate(EffectGroup[effname]);
            //设置父物体在GameMgr下面
            _eff.transform.SetParent(transform);
            _eff.transform.localPosition = pos;//生成在自己指定的位置
            _eff.transform.localScale = Vector3.one;
            _eff.transform.localRotation = new Quaternion();
            _eff.AddComponent<Effect>();
        }
        else
        {
            Debug.Log("没有找到effect" + effname);
        }
    }

}
