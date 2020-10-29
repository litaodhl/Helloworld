using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//特效管理
public class Effect : MonoBehaviour
{
   
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

}
