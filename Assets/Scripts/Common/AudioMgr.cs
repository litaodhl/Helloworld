using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    //音频管理类
    //音源文件名 常量。
    public const string bgm = "bgm";
    public const string playerShooting = "playShooting";
    public const string playerDie = "playerDie";
    public const string exp_enumy = "exp_enumy";
    public const string exp_asteroid = "exp_asteroid";

    private AudioSource _BgmAudioSource;
    private AudioSource _subAuidoSource;
    private AudioSource _playerShipAudioSource;

    public AudioSource _BgmAudioSource_p
    {
        get
        {
            return _BgmAudioSource;
        }
    }

    public AudioSource subAuido_p
    {
        get
        {
            return _subAuidoSource;
        }
    }

    public AudioSource playerShipAudio_p
    {
        get
        {
            return _playerShipAudioSource;
        }
    }

    //使用字典存放音效的容器
    Dictionary<string, AudioClip> _Audios = new Dictionary<string, AudioClip>();

    

    //初始化方法，初始化组件，在GameMgr中调用。
    public void Inst()
    {
        _BgmAudioSource = GetComponent<AudioSource>();
        _subAuidoSource = transform.Find("AudioSub").GetComponent<AudioSource>();
        _playerShipAudioSource = GameMgr.Instance.curPlayShip_p.AddComponent<AudioSource>();
        LoadAudioRes();
    }
    

    //创建播放音效方法。
    //临时变量接收
    private AudioClip _tempClip;
    void LoadAudioRes()
    {
        //实例化前清空一次字典
        _Audios.Clear();
        _tempClip = Resources.Load<AudioClip>("Audio/music_background");
        _Audios.Add(bgm, _tempClip);
        _tempClip = Resources.Load<AudioClip>("Audio/weapon_player");
        _Audios.Add(playerShooting, _tempClip);
        _tempClip = Resources.Load<AudioClip>("Audio/explosion_player");
        _Audios.Add(playerDie, _tempClip);
        _tempClip = Resources.Load<AudioClip>("Audio/explosion_enemy");
        _Audios.Add(exp_enumy, _tempClip);
        _tempClip = Resources.Load<AudioClip>("Audio/explosion_asteroid");
        _Audios.Add(exp_asteroid, _tempClip);

    }

    //播放音效方法。
    public void PlayAudio(string audioClip, AudioSource As)
    {
        if (_Audios.ContainsKey(audioClip))
        {
            As.clip = _Audios[audioClip];
            As.Play();
        }
        else
        {
            Debug.LogError("No find thisAudio:" + audioClip);
        }
    }
}
