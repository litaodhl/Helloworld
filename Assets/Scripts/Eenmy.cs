using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eenmy : MonoBehaviour
{
    private float _speed = 5f;
    GameObject _missile;
    public void Inst()
    {
        //找到敌机子弹
        _missile = transform.Find("missile").gameObject;
        InvokeRepeating("Shooting", 0.5f, 1.5f);//可以把后面两个数作为变量调整难度
    }
    //子弹类维护在这里
    GameObject _tempMis;
    EnemyMissile _tempEnemMis;

    //射击方法
    void Shooting()
    {
        //实例化_missile
        _tempMis = Instantiate(_missile);
        _tempMis.SetActive(true);//设置可显式
        //在Unity加一个新标签
        _tempMis.tag = "EnemyMissile";
        //敌机子弹放在父物体上
        _tempMis.transform.SetParent(transform);
        _tempMis.transform.localPosition = Vector3.zero;
        _tempMis.transform.localScale = Vector3.one;
        _tempMis.transform.localRotation = new Quaternion();
        _tempEnemMis = _tempMis.AddComponent<EnemyMissile>();
        //调用敌机死亡后 子弹继续存在的方法
        _tempEnemMis.Inst();
    }
   
   
    //敌机的移动
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        //销毁敌机
        if(transform.position.y<-35f)
        {
            Destroy(gameObject);
        }
    }

    //角色与敌机碰撞，播放音效并全部销毁
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //释放特效
            GameMgr.Instance.EffectMgr.BuildEffect(EffectMgr.playerDie, other.transform.position);//敌机播放特效
            GameMgr.Instance.EffectMgr.BuildEffect(EffectMgr.enemyDie, transform.position);//玩家播放特效
            //播放音效
            GameMgr.Instance.AudioMgr.PlayAudio(AudioMgr.playerDie, GameMgr.Instance.AudioMgr.subAuido_p);
            Destroy(other.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    //消停时 取消 InvokeRepeating
    private void OnDestroy()
    {
        CancelInvoke("Shooting");
    }
}
