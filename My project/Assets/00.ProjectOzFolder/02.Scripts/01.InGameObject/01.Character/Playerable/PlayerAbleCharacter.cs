using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbleCharacter : CharacterBased
{
    
    [Range(1,10)]
    public float HP;
    [Range(1,90)]
    public float DEBUG_JUMP_POWER;
    [Range(1,90)]
    public float DEBUG_WALK_SPEED;
    [Range(0,0.5f)]
    public float DEBUG_MULTI_JUMP_WALK;

    [SerializeField]
    protected static int canJumpCount; //남은 점프횟수

    [SerializeField]
    Gauge JumpCount;    

    public float gravityScale; //플레이어 중력값
    public float dashTimer;//대쉬 유지되는 시간
    public float dashForce;//대쉬 나가는 힘
    public float dashCool;//대쉬 쿨


    public static STATE State;            //플레이어의 외부 상태입니다.
                                            //(공중에 뜸, 벽타기 중, 땅을 딛음)
    public static ACTION Action;          //플레이어의 행동 상태입니다.
                                            //(공격중 , 점프 중,)
    public static ACTION OldAction;        //플레이어의 이전 액션을 저장하는 변수입니다.
    public static float attackDelay;       //플레이어의 연속 공격을 판단할 함수.
    public static int attackLev;           //플레이어의 연속 공격 횟수

    BODY body;
    public override void Init()
    {
        base.Init();
        base.AddRigid();

        OldAction = ACTION.IDLE;
        State = STATE.STAND;

        JumpCount.Max = canJumpCount;

        ReSetJumpCount();
    }

    public void ReSetJumpCount()
    {
        JumpCount.Val = JumpCount.Max;
    }

    protected virtual void Update()
    {
        body.JumpPower    = DEBUG_JUMP_POWER;
        body.WalkSpeed    = DEBUG_WALK_SPEED;
        if(OldAction != Action)
        {

        }
    }
}



