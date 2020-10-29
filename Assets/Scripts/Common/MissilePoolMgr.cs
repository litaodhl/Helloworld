using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePoolMgr : MonoBehaviour

//飞弹管理
{
    //临时父物体存放子弹
    private Transform _missiPoolobj;

    public Transform _missileTempParent
    {
        private set;
        get;
    }

    private GameObject _missileSeed;
    private const int _maxCount = 20;
    public Queue<GameObject> MissileGroup = new Queue<GameObject>();

    //实例方法 在GameMgr中调用该方法。
    public void Inst()
    {
        //生成前先清空一次队列
        MissileGroup.Clear();
        //初始化成员
        _missiPoolobj = GameMgr.Instance.curPlayShip_p.transform.Find("MissileTempGroup");
        _missileTempParent = GameMgr.Instance.transform.Find("MissileTempGroup");

        _missileSeed = _missiPoolobj.transform.Find("missileSeed").gameObject;
        //调用创建子弹方法
        CreatMissile();
    }


    //创建子弹的方法
    private GameObject _temp;//创建出来后用临时的接收。
    void CreatMissile()
    {
        //循环创建子弹到最大数。
        for (int i = 0; i <= _maxCount; i++)
        {
            _temp = Instantiate(_missileSeed);
            //设置_missiPoolobj为父物体
            _temp.transform.SetParent(_missiPoolobj);
            //设置子弹的 位置，大小，旋转。
            _temp.transform.localPosition = Vector3.zero;
            _temp.transform.localScale = Vector3.one;
            _temp.transform.localRotation=Quaternion.identity;
            //设置子弹关闭
            _temp.gameObject.SetActive(false);
            //添加子弹脚本
            _temp.AddComponent<PlayerMissile>();
            //用Enqeueue方法放入队列末尾。
            MissileGroup.Enqueue(_temp);
        }
    }
    //创建独立的子弹，解决子弹还没回流的问题。
    GameObject CreatAlon()
    {
        //新建一个不是子弹池内的子弹
        _temp = Instantiate(_missileSeed);
        _temp.transform.SetParent(_missiPoolobj);
        _temp.transform.localPosition=Vector3.zero;
        _temp.transform.localScale=Vector3.one;
        _temp.transform.localRotation=Quaternion.identity;
        _temp.gameObject.SetActive(false);
        //添加子弹脚本
        _temp.AddComponent<PlayerMissile>();
        //返回一个物体给方法。
        return _temp;
    }

    //通过判断 获取子弹。
    public GameObject GetMissile()
    {
        //当队列中的数量大于0时
        if (MissileGroup.Count>0)
        {
            return MissileGroup.Dequeue();//返回，并移除队列中的元素。
        }
        else
        {
            //如果队列里没有元素，通过新建子弹方法，添加单独的子弹。
            return CreatAlon();
        }
    }
    //取出来后需要放回去。返回对象池的方法。
    public void ComeBackToPool(GameObject missile)
    {
        //先关闭状态
        missile.SetActive(false);
        //再设置回父物体,重置一次。
        missile.transform.SetParent(_missiPoolobj);
        missile.transform.localPosition = Vector3.zero;
        missile.transform.localScale = Vector3.one;
        missile.transform.localRotation = Quaternion.identity;
        //再次把子弹放回队列
        MissileGroup.Enqueue(missile);

    }
}
