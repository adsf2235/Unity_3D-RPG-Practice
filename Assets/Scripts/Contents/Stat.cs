using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat :MonoBehaviour
{
    [SerializeField]
    int _level;
    [SerializeField]
    int _hp;
    [SerializeField]
    int _maxHp;
    [SerializeField]
    int _attack;
    [SerializeField]
    int _defense;
    [SerializeField]
    float _moveSpeed;

    public int level { get { return _level; } set { _level = value; } }
    public int hp { get { return _hp; } set { _hp = value; } }
    public int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int attack { get { return _attack; } set { _attack = value; } }
    public int defense { get { return _defense; } set { _defense = value; } }
    public float moveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        level = 1;
        hp = 100;
        maxHp = 100;
        attack = 10;
        defense = 5;
        moveSpeed = 5.0f;
    }
}
