using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//陨石管理模块 (作用：（1）维护数据 （2）提供访问或者编辑数据的方法)
public class AsteridMgr : MonoBehaviour
{
   //提供容器装陨石
   private List<GameObject> Asteroids = new List<GameObject>();
   //一个临时存放
    GameObject _tempAst;
    //实例方法
    public void Inst()
    {
        Asteroids.Clear();
        _tempAst = Resources.Load("AsteroidPrefabs/asteroid_a") as GameObject;
        Asteroids.Add(_tempAst);
        _tempAst = Resources.Load("AsteroidPrefabs/asteroid_b") as GameObject;
        Asteroids.Add(_tempAst);
        _tempAst = Resources.Load("AsteroidPrefabs/asteroid_c") as GameObject;
        Asteroids.Add(_tempAst);
    }

    //创建陨石
    private float buildTime = 3F;

    public void StrtBuild()
    {
        InvokeRepeating("BuildAsteroid", 0, buildTime);
    }
    //生成陨石的方法
    private float _ast_y = -10;
    private float _ast_maxside = 3.5f;
    GameObject _ast;
    Asteroid _curAst;

    void BuildAsteroid()
    {
        //随机生成不同的陨石
        int _index = Random.Range(0, Asteroids.Count);
        //实例化
        _ast = Instantiate(Asteroids[_index], transform);//通过容器找到位置
        _ast.transform.localPosition = new Vector3(Random.Range(-_ast_maxside, _ast_maxside),
            _ast_y,0);
        _ast.transform.localScale = Vector3.one;
        _ast.transform.localRotation = new Quaternion();
        //通过_ast添加陨石类脚本
        _curAst = _ast.AddComponent<Asteroid>();
        _curAst.Inst();
    }
    //停止 InvokeRepeating
    private void OnDestroy()
    {
        CancelInvoke("BuildAsteroid");
    }
}


