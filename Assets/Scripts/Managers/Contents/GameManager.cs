using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    GameObject _player;


    public GameObject Spawn(Define.ObjectType type,string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        switch (type)
        {
            case Define.ObjectType.Player:
                _player = go;
                break;
            case Define.ObjectType.Monster:
                _monsters.Add(go);
                break;
        }

        return go;
    }
}
