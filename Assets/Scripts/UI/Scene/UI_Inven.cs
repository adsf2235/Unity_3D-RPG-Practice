using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        for (int i = 0; i < 8; i++)
        {
            GameObject go = Managers.Resource.Instantiate($"UI/Scene/UI_Inven_Item");
            go.transform.SetParent(gridPanel.transform);

            UI_Inven_Item Invenitem = Utill.GetOrAddComponent<UI_Inven_Item>(go);
            if (Invenitem != null)
            {
                Invenitem.SetInfo($"Hot Dog{i}");
            }
            else
            {
                Debug.Log(Invenitem);
            }
           
          
        }

    }

    void Update()
    {
        
    }
}
