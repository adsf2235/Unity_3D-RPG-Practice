using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    int i = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public AudioClip audioClip;
    private void OnTriggerEnter(Collider other)
    {
        
        i++;

        if (i % 2 == 0)
        {
            Managers.Sound.Play("UnityChan/univ0001", Define.Sound.Bgm);
        }
        else
        {
            Managers.Sound.Play("UnityChan/univ0002", Define.Sound.Bgm);
        }
       
        
        Debug.Log("Clear");

    }
}
