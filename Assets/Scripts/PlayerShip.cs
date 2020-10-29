using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    //玩家飞船类
    private float _hor_moveSpeed = 0.2f;
    private float _ver_moveSpeed = 0.15f;

    //飞弹发射的位置
    private Transform _missilePoint;
  

    void Awake()
    {
        //初始化
        _missilePoint = transform.Find("shootpoint");//飞弹发射的位置。
    }

    //飞船的控制输入方法
    void ControlPlane()
    {
        //获得键盘输入
        float _hor = Input.GetAxis("Horizontal");
        float _ver = Input.GetAxis("Vertical");
        transform.position += new Vector3(_hor  * _hor_moveSpeed, _ver  * _ver_moveSpeed, 0);

    }

    //限制飞机移动范围
    private float _maxSide = 3.3f;
    private float _maxUp = -15.2f;
    private float _maxDown = -28.6f;
    void ShowLimit()
    {
        if (transform.position.x < -_maxSide)
        {
            transform.position = new Vector2(-_maxSide, transform.position.y);
        }

        if (transform.position.x > _maxSide)
        {
            transform.position = new Vector2(_maxSide, transform.position.y);
        }
        if (transform.position.y > _maxUp)
        {
            transform.position = new Vector2(transform.position.x, _maxUp);
        }
        if (transform.position.y < _maxDown)
        {
            transform.position = new Vector2(transform.position.x, _maxDown);
        }
    }
    //处理飞机直接倾斜
   // private float _incline_angle = 30f;

    //插值倾斜变量
    private Quaternion _targetAngle_left = Quaternion.Euler(0, 35f, 0);
    private Quaternion _targetAngle_right = Quaternion.Euler(0, -35f, 0);
    private float _roSpeed = 3f;
    void ctrIncLine()
    {
        ////直接转
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    transform.Rotate(new Vector3(0, _incline_angle, 0));
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    transform.Rotate(new Vector3(0, -_incline_angle, 0));
        //}

        //if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        //{
        //    transform.rotation = new Quaternion();
        //}
        ////插值旋转
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetAngle_left, Time.deltaTime * _roSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetAngle_right, Time.deltaTime * _roSpeed);
        }

        if (Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D))
        {
            transform.rotation = Quaternion.identity;
        }
    }

    //在船体控制脚本中 实现子弹发射方法。
    private GameObject _tempmiss;
    void Shooting()
    {
        //获取用户输入
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //用一个临时空间存放子弹。
            _tempmiss = GameMgr.Instance.MissilePoolMgr.GetMissile();
            //将它的父物体设置成暂存池。
            _tempmiss.transform.SetParent(GameMgr.Instance.MissilePoolMgr._missileTempParent);
            //激活前把位置修正
            _tempmiss.transform.localPosition = _missilePoint.position;
            //激活
            _tempmiss.SetActive(true);
            //播放射击音效
            GameMgr.Instance.AudioMgr.PlayAudio(AudioMgr.playerShooting, GameMgr.Instance.AudioMgr.playerShipAudio_p);

        }
    }

    void Update()
    {
        Shooting();
        ctrIncLine();
    }

    void FixedUpdate()
    {
        ControlPlane();
        ShowLimit();
       
    }
}
