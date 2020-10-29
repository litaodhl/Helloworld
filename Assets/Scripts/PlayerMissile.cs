using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    private float _speed = 10f;

    //解决子弹提前消失的问题
    void OnDisable()
    {
        CancelInvoke("ComeBackPool");
    }

    //在触发到物体时调用 返回池方法。
    void OnEnable()
    {
        Invoke("ComeBackPool",1.5f);
    }

    //调用返回池的方法。
    void ComeBackPool()
    {
        GameMgr.Instance.MissilePoolMgr.ComeBackToPool(gameObject);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up*Time.deltaTime*_speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy")
        {
            //播放音效
            GameMgr.Instance.AudioMgr.PlayAudio(AudioMgr.exp_asteroid, GameMgr.Instance.AudioMgr.subAuido_p);

            //释放特效
            GameMgr.Instance.EffectMgr.BuildEffect(EffectMgr.enemyDie, other.transform.position);
            //击中敌机后加分
            GameMgr.Instance.Score += 5;
            //（3）委托事件第三步：在数据改变时调用，通知注册。
            EventSystem.RaiseScoreChange();
            Destroy(other.gameObject);
            //返回对象池  打到敌人后子弹返回对象池
            GameMgr.Instance.MissilePoolMgr.ComeBackToPool(gameObject);
        }

        if (other.gameObject.tag=="stone")
        {
            //播放音效
            GameMgr.Instance.AudioMgr.PlayAudio(AudioMgr.exp_asteroid, GameMgr.Instance.AudioMgr.subAuido_p);

            //释放特效
            GameMgr.Instance.EffectMgr.BuildEffect(EffectMgr.enemyDie, other.transform.position);
            //击中陨石后加分
            GameMgr.Instance.Score += 2;
            EventSystem.RaiseScoreChange();
            Destroy(other.gameObject);
            //返回对象池  打到陨石后子弹返回对象池
            GameMgr.Instance.MissilePoolMgr.ComeBackToPool(gameObject);
        }
       
    }

}
