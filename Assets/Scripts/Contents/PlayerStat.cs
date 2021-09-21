using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    int _exp;


    public int exp { get { return _exp; } set { _exp = value; } }
}
