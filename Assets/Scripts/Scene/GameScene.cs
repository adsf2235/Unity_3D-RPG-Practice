using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // Managers.UI.ShowPopupUI<UI_Button>("UI_Button");

        //temp
        // Managers.UI.ShowSceneUI<UI_Inven>();



        GameObject player = Managers.Game.Spawn(Define.ObjectType.Player, "unitychan");
        Managers.Game.Spawn(Define.ObjectType.Monster, "Bear");

        Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);


      
    }

    void Start()
    {
        Init();

    }

    void Update()
    {
        
    }

    public override void Clear()
    {
     
    }
}
