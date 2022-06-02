using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ISLOOK
{
    LEFT=-1,
    RIGHT=1
}

public enum INPUT   //캐릭터가 실행하려는 행동입니다.
{
    TOGGLE_DEBUG,
    IDLE,WALK,JUMP,WALLRUN,DASH,JUMP_DOUBLE
    ,LOOK_UP,LOOK_DOWN,LEFT,RIGHT,TAG,INTERACTION, TAGDOROTHY, ISCOMBAT
    //ISCOMBAT 은 ACTION ENUM 에서 ISCOMBAT 보다 높은 열거형만 전투에 관련됨을 나타냅니다.
    //실제로 쓰이지 않습니다.
    , ATTACK,ATTACK_CHARGE,END
}

public enum STATE
    {STAND,FLOAT,WALLHANG,HIT}

    public enum CONDITION
    {NONE,PEACE,COMBAT}

    public enum ACTION//캐릭터가 실제로 실행하는 행동입니다.
    {
    IDLE,WALK,JUMP,INTERACTION
    ,CLIMB_WALL,DASH,JUMP_DOUBLE,LOOK_UP,LOOK_DOWN,DOTAG,TARGETTAG,ISCOMBAT
    //ISCOMBAT 은 ACTION ENUM 에서 ISCOMBAT 보다 높은 열거형만 전투에 관련됨을 나타냅니다.
    //실제로 쓰이지 않습니다.
    ,ATTACK_A,ATTACK_B,ATTACK_C,ATTACK_CHARGE,HIT,END
    }

    public enum CHARACTER
    {
    DOROTHY=0,LION,SCARECROW,WOODCUTTER
    }
[System.Serializable]
    public struct BODY
    {
    [Range(0,10)]
    public float WalkSpeed;
    [Range(0,10)]
    public float DashSpeed;
    [Range(0,10)]
    public float JumpPower;
    }

namespace MONSTER
{
    public enum ACTION
    {
    IDLE,WALK,JUMP,INTERACTION
    ,CLIMB_WALL,DASH,JUMP_DOUBLE,LOOK_UP,LOOK_DOWN,DOTAG,TARGETTAG,ISCOMBAT
    ,ATTACK_A,ATTACK_B,ATTACK_C,ATTACK_CHARGE,HIT,END
    }
    public enum STATE//몬스터의 변형된 행동입니다. 몬스터가 가지는 STATES입니다.
    {
    IDLE,TRACE,COMBAT 
    }
    public struct BODY
    {
    public float WalkSpeed;
    public float JumpPower;
    }
}

[System.Serializable]
public struct Gauge
{
    [Range(0,3)]
    public float Max;
    [Range(0,3)]
    public float Min;
    [Range(0,3)]
    public float Val;
}