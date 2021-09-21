using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index;
            index = name.LastIndexOf("/");
            if (index >= 0 )
            {
                name = name.Substring(index + 1);
            }
            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
            {
                return go as T;
            } 
            

        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        Poolable poolable = original.GetComponent<Poolable>();
        if (poolable != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        return Object.Instantiate(original, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
      
    }
   
}
