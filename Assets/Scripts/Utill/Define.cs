using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum State
    {
        Idle,
        Die,
        Moving,
        Skill,

    }
    public enum Layer
    {
        Monster = 8,
        Floor = 10,
        Block = 11,
    }

    public enum CursorType
    {
        None,
        Basic,
        Attack,

    }

    public enum MouseEvent
    {
        Press,
        Click,
        PointerUp,
        PointerDown,
    }
    public enum CameraMode
    {
        QuarterView,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

}
