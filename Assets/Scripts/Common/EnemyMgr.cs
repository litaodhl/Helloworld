using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//敌机管理
public class EnemyMgr : MonoBehaviour
{
    private List<GameObject> EnemyGroup = new List<GameObject>();

    GameObject _tempEnemy;
    //在GameMgr调用加载
    public void Inst()
    {
        EnemyGroup.Clear();
        _tempEnemy = Resources.Load("Enemy/enemy_a") as GameObject;
        EnemyGroup.Add(_tempEnemy);
        _tempEnemy = Resources.Load("Enemy/enemy_b") as GameObject;
        EnemyGroup.Add(_tempEnemy);
    }

    public void StartBuild()
    {
        InvokeRepeating("BuildEnemy", 0, _buildTime);
    }

    //创建飞创的位置
    private float _enemy_y = -13;
    private float _enemy_maxSide = 3.2f;
    GameObject _enemy;
    //创建飞创的事件
    private const float _buildTime = 1f;
    private Eenmy _cureeEnemy;

    //创建敌机方法
    void BuildEnemy()
    {
        int _index = Random.Range(0, EnemyGroup.Count);//随机整数包前不包后。(0~~EnemyGroup.Count-1)所以不会越界
        _enemy = Instantiate(EnemyGroup[_index], transform);//敌机放入
        _enemy.transform.localPosition = new Vector3(Random.Range(-_enemy_maxSide, _enemy_maxSide),
            _enemy_y, 0);
        _enemy.transform.localScale = Vector3.one;
        _enemy.transform.localRotation = new Quaternion();

        //加入脚本
        _cureeEnemy = _enemy.AddComponent<Eenmy>();
        _cureeEnemy.Inst();

    }
}
