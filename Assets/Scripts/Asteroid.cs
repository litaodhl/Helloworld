using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//陨石类
public class Asteroid : MonoBehaviour
{
    private float _speed;
    Transform _mesh;//拿到子物体
    public void Inst()
    {
        _mesh = transform.Find("mesh");
        //做一个陨石随机数度值
        _speed = Random.Range(1, 4);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);//移动
        _mesh.Rotate(Vector3.left * _speed);//陨石滚动
        //销毁陨石
        if (transform.position.y < -35f)
        {
            Destroy(gameObject);
        }
    }

    //陨石与玩家撞击
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            //播放音效 
            GameMgr.Instance.AudioMgr.PlayAudio(AudioMgr.playerDie, GameMgr.Instance.AudioMgr._BgmAudioSource_p);
            //播放特效 要在销毁前
            GameMgr.Instance.EffectMgr.BuildEffect(EffectMgr.playerDie, other.transform.position);
            GameMgr.Instance.EffectMgr.BuildEffect(EffectMgr.asteroidDes,transform.position);
            Destroy(other.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
