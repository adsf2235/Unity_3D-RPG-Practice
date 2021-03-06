using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 delta = new Vector3(0.0f,6.0f,-7.5f);


    [SerializeField]
    GameObject player = null;





    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    void Start()
    {
     

    }

    void LateUpdate()
    {


        RaycastHit hit;


        
        if (mode == Define.CameraMode.QuarterView)
        {
            if (player ==null || player.activeSelf == false)
            {
                return;
            }
            if (Physics.Raycast(player.transform.position, delta, out hit, delta.magnitude, LayerMask.GetMask("Block")))
            {
                float dist = (hit.point - player.transform.position).magnitude * 0.8f;
                transform.position = player.transform.position + delta.normalized * dist;
            }
            else
            {
                transform.position = player.transform.position + delta;
                transform.LookAt(player.transform);
            }
            

        }
      
    }
}
