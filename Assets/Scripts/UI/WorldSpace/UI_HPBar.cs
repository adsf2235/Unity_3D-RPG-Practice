using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    Stat _stat;
    enum GameObjects
    {
        HPBar,

    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.gameObject.GetComponent<Collider>().bounds.size.y + 0.3f);
        transform.rotation = Camera.main.transform.rotation;
        if (_stat.hp <= 0)
        {
            _stat.hp = 0;
        }
        float ratio = _stat.hp / (float)_stat.maxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
 
    }
}
