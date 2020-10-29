using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//星空背景处理
public class Bg_Group : MonoBehaviour
{
    private float _moveSpeed = 0.5f;//移动速度
    private float _tileSizeZ = 20.45f;//通过摄像机顶部和图片的无缝分对比获取此值。
    private Vector3 _startPostion;

    void Awake()
    {
        //记录图片组的开始位置。
        _startPostion = transform.position;
    }

    void Update()
    {
        //使用Mathf.Repeat（）截取函数：重复T不大于 Length，不小于0.
        float newpos = Mathf.Repeat(Time.time * _moveSpeed, _tileSizeZ);
        //float newpos = Mathf.PingPong(Time.time * _moveSpeed, 3);
        transform.position = _startPostion + Vector3.down * newpos;
    }
}
