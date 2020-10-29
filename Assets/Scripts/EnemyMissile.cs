using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//敌机的子弹
public class EnemyMissile : MonoBehaviour
{
    //敌机消亡后，子弹继续存在的方法
    public void Inst()
    {
        transform.SetParent(GameMgr.Instance.transform);
        Destroy(gameObject, 2f);
    }
    //速度  
    private float _speed = 10f;
    //子弹移动
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }

    //子弹碰撞到玩家，播放音效，销毁自己
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            //释放特效
            GameMgr.Instance.EffectMgr.BuildEffect(EffectMgr.playerDie, other.transform.position); //玩家死亡特效
            //播放音效并销毁敌我
            GameMgr.Instance.AudioMgr.PlayAudio(AudioMgr.playerDie, GameMgr.Instance.AudioMgr.subAuido_p);
            Destroy(other.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
