using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    public Stack<UI_Popup> PopupStack = new Stack<UI_Popup>();
    public UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_root" };
            }
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utill.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }

    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name =null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
        go.name = name;
        if (parent != null)
        {
            go.transform.SetParent(parent);
        }
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Utill.GetOrAddComponent<T>(go);

    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T scene = Utill.GetOrAddComponent<T>(go);


        go.transform.SetParent(Root.transform);
        _sceneUI = scene;

        return scene;

    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Utill.GetOrAddComponent<T>(go);
        PopupStack.Push(popup);


        go.transform.SetParent(Root.transform);

        return popup;

    }

    public void ClosePopupUI()
    {
        if (PopupStack.Count == 0)
        {
            return;
        }
        UI_Popup popup = PopupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        
    }

    public void Clear()
    {
        ClosePopupUI();
        _sceneUI = null;
    }
}
