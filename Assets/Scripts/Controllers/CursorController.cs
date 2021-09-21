using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Texture2D _attack;
    Texture2D _basic;
    Define.CursorType _cursorType;
    int _mask = (1 << (int)Define.Layer.Floor | 1 << (int)Define.Layer.Monster);
    void Start()
    {
        _attack = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _basic = Managers.Resource.Load<Texture2D>("Textures/Cursor/Basic");
        _cursorType = Define.CursorType.None;
    }

    void Update()
    {
        UpdateMouseCursor();
    }
    void UpdateMouseCursor()
    {

        if (Input.GetMouseButton(0))
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != Define.CursorType.Attack)
                {
                    Cursor.SetCursor(_attack, Vector2.zero, CursorMode.Auto);
                    _cursorType = Define.CursorType.Attack;
                }

            }
            else
            {
                if (_cursorType != Define.CursorType.Basic)
                {
                    Cursor.SetCursor(_basic, Vector2.zero, CursorMode.Auto);
                    _cursorType = Define.CursorType.Basic;
                }

            }


        }
    }
   
}
