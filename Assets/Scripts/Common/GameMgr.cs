using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private static GameMgr Inst;

    public static GameMgr Instance
    {
        get
        {
            return Inst;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        Inst = this;
        //添加船体的控制脚本
        curPlayShip = transform.Find("PlayerShipGroup").gameObject;
        curPlayShip.AddComponent<PlayerShip>();

        //添加所需模块组件的组件
        AudioMgr = gameObject.AddComponent<AudioMgr>();
        //调用音效初始化
        AudioMgr.Inst();
        AudioMgr.PlayAudio(AudioMgr.bgm, AudioMgr._BgmAudioSource_p);


        EnemyMgr = gameObject.AddComponent<EnemyMgr>();
        EnemyMgr.Inst();
        EnemyMgr.StartBuild();

        AsteridMgr = gameObject.AddComponent<AsteridMgr>();
        AsteridMgr.Inst();
        AsteridMgr.StrtBuild();

        EffectMgr = gameObject.AddComponent<EffectMgr>();
        EffectMgr.Inst();

        MissilePoolMgr = gameObject.AddComponent<MissilePoolMgr>();
        //调用对象池里的生成子弹方法
        MissilePoolMgr.Inst();

        _MainCancas = transform.Find("Canvas").GetComponent<Canvas>();
        
        _scoreUI = _MainCancas.transform.Find("ScoreUI").gameObject.AddComponent<ScoreUI>();
        _scoreUI.Inst();
    }

    private GameObject curPlayShip;
    public GameObject curPlayShip_p
    {
        get
        {
            return curPlayShip;
        }
    }

    //音效管理
    public AudioMgr AudioMgr
    {
        get;
        private set;
    }
    //敌人管理
    public EnemyMgr EnemyMgr
    {
        get;
        private set;
    }
    //陨石管理
    public AsteridMgr AsteridMgr
    {
        get;
        private set;
    }
    //特效管理
    public EffectMgr EffectMgr
    {
        get;
        private set;
    }
    //子弹对象池
    public MissilePoolMgr MissilePoolMgr
    {
        get;
        private set;
    }
    //分数维护管理
    public int Score
    {
        get;
        set;
    }


    Canvas _MainCancas;
    public Canvas MainCancas_p
    {
        get
        {
            return _MainCancas;
        }
    }
    
    ScoreUI _scoreUI;
    public ScoreUI scoreUI_p
    {
        get
        {
            return _scoreUI;
        }
    }
}
