using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Dashinfo
{
    public bool canDash;                    //대쉬 쿨인지
    public float applyTime;                 //대쉬 유지되는 시간
    public float applyforce;                 //대쉬 나가는 힘
    public float coolTime;              //대쉬 쿨
}


public partial class PlayerAbleCharacter : CharacterBased
{
    
    protected Manager.Player PLAYER() {return Manager.Player.Inst;}
    
    [Range(1,90)]
    public float DEBUG_JUMP_POWER;
    [Range(1,10)]
    public float HP;
    [Range(1,90)]
    public float DEBUG_WALK_SPEED;
    [Range(0,0.5f)]
    public float DEBUG_MULTI_JUMP_WALK;

    [Range(0,3)]
    public int DEBUG_JUMP_COUNT;

    [SerializeField]
    Gauge JumpCount;    

    [Range(0f,5)]
    public float gravityScale;              //플레이어 중력값
    [Range(0,30)]
    public float MaxDownVelocity;              //최대 낙하속도
    public Dashinfo mDash;
    
    public static float attackDelay;       //플레이어의 연속 공격을 판단할 변수.
    public static int attackLev;           //플레이어의 연속 공격 횟수

    public bool bOnWall;                    //플레이어가 벽에 붙었는지 확인하는 변수

    [SerializeField]
    BODY body;
    public override void Init()
    {
        base.Init();
        base.AddRigid();


        State = STATE.STAND;
        JumpCount.Max = DEBUG_JUMP_COUNT;
        bOnWall=false;
        mDash.canDash=true;
        SetMotion(ACTION.IDLE);
        ReSetJumpCount();

    }



    protected override void Update()
    {
        base.Update();
        if(doMotion.isCanDoOther)
        {
            if(Input.anyKey)
            {
                if (Manager.Key.Inst.GetActionDown(INPUT.JUMP))
                {
                    jump();
                }

                if(Manager.Key.Inst.GetAction(INPUT.LOOK_UP))
                {
                    lookup();
                }
                
                if(Manager.Key.Inst.GetAction(INPUT.LOOK_DOWN))
                {
                    lookdown();
                }
                if(Manager.Key.Inst.GetActionDown(INPUT.DASH))
                {
                    dash();
                }
                
                
            }
            else
            {
                idle();
            }
        }
        

    }

    
    protected virtual void FixedUpdate()
    {
        
        if(doMotion.isCanDoOther)
        {
            if (Input.anyKey)
            {
                if(Manager.Key.Inst.GetAction(INPUT.LEFT) && !Manager.Key.Inst.GetAction(INPUT.RIGHT))
                {
                    SetLookDir(ISLOOK.LEFT);
                    walk();
                }
                if(Manager.Key.Inst.GetAction(INPUT.RIGHT) && !Manager.Key.Inst.GetAction(INPUT.LEFT))
                {
                    SetLookDir(ISLOOK.RIGHT);
                    walk();
                }
                
            }
        }
        
        if(State == STATE.FLOAT && action != ACTION.DASH)
        {
            Crigid.AddForce(Vector2.down * gravityScale*Time.deltaTime);
            if(Crigid.velocity.y < -MaxDownVelocity)
            {
                Crigid.velocity = new Vector2(Crigid.velocity.x,-MaxDownVelocity);
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.transform.tag == Tags.Wall)
        {
            bOnWall=true;
            ReSetJumpCount();
        }
        else if (other.transform.tag == Tags.Floor)
        {
            State = STATE.STAND;
            ReSetJumpCount();
        }
        
    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.transform.tag == Tags.Floor 
        &&State==STATE.STAND)
        {
            State= STATE.FLOAT;
            JumpCount.Val--;
        }
        else if (other.transform.tag == Tags.Wall)
        {
            bOnWall=false;
            if(State != STATE.STAND)
            JumpCount.Val--;
        }
        

    }
    public override void ApplyDataOnInGameEditor()
    {
        base.ApplyDataOnInGameEditor();
        body.WalkSpeed = DEBUG_WALK_SPEED;
        body.JumpPower = DEBUG_JUMP_POWER;
    } 
    
}





















public partial class PlayerAbleCharacter: CharacterBased
{
    protected virtual IEnumerator DashMove(ACTION _action)
    {
        Crigid.gravityScale = 0;
        Crigid.velocity = Vector2.zero;

        //if (State == STATE.FLOAT)
            //StartCoroutine(MoveEffect.Instance.EffectContinue(4, transform.position));
        //else
            //StartCoroutine(MoveEffect.Instance.EffectContinue(3, transform.position));

        action = ACTION.DASH;

        yield return new WaitForSecondsRealtime(mDash.applyTime);

        action = _action;
        Crigid.gravityScale = gravityScale;
        Crigid.velocity = Vector2.zero;
        Debug.Log("End");

    }

    protected virtual IEnumerator DashCool()
    {
        mDash.canDash = false;
        yield return new WaitForSecondsRealtime(mDash.coolTime);
        mDash.canDash = true;
    }
    public virtual void idle() 
    {
        if(State != STATE.FLOAT && action != ACTION.DASH)
        {
            action = ACTION.IDLE;
        }
    }

    public virtual void walk() 
    {
        float MoveX = Input.GetAxisRaw(AXIS.Horizontal) * body.WalkSpeed;
        MoveToForceX(MoveX);
        action = ACTION.WALK;

        if(bOnWall)
        {
            Crigid.AddForce(Vector2.down * gravityScale*Time.deltaTime);
            if(Crigid.velocity.y < -MaxDownVelocity)
            {
                Debug.Log(Crigid.velocity.y);
                Crigid.velocity = new Vector2(Crigid.velocity.x,-MaxDownVelocity);
            }
        }
    }
    protected virtual void dash()
    {
        
    }
     public virtual void jump() 
    {
        if (JumpCount.Val > 0)
        {
            if(State == STATE.FLOAT)
            {
                JumpCount.Val--;
            }
            Crigid.velocity = new Vector2(Crigid.velocity.x, body.JumpPower);
            
        }

            //if (JumpCount.Val <= JumpCount.Max - 1 && JumpCount.Max > 0)
            //{
            //    if (PLAYER().hasLion && PLAYER().bCanTag)
            //    Debug.Log("a");
            //        //Controller.PlayerDoTag("Lion");
            //    else
            //        JumpCount.Val = 0;
            //}
            //else if ( JumpCount.Val > 0)
            //{
            //    Crigid.velocity = Vector2.zero;
            //}

            //    /*
            //    if (action == ACTION.CLIMB_WALL)
            //    {
            //        Vector2 dir = new Vector2(Mathf.Cos(70), Mathf.Sin(70)).normalized;
            //        if (transform.rotation.y == 0)
            //            dir = new Vector2(dir.x * -1, dir.y);
            //        Crigid.AddForce(Vector2.right * body.JumpPower, ForceMode2D.Impulse);
            //    }
            //    else
            //    {
            //        Crigid.AddForce(Vector2.up * body.JumpPower, ForceMode2D.Impulse);
            //    }
            //    */
            //    //if (Action == ACTION.JUMP)//더블점프 애니 등 추가로 생기면 ACTION.DOUBLEJUMP로
            //    //    StartCoroutine(MoveEffect.Instance.EffectContinue(2, transform.position));
            //    //if (State == STATE.FLOAT)
            //      //  StartCoroutine(MoveEffect.Instance.EffectContinue(2, transform.position));
            //      
            //    action = ACTION.JUMP;
            //    JumpCount.Val--;
    }
        
    public void ReSetJumpCount()
    {
        JumpCount.Val = JumpCount.Max;
    }
    
    public virtual void DoTag()
    {

    }
    public virtual void TargetTag() 
    {

    }












    public virtual void lookup()
    {
            action = ACTION.LOOK_UP;
    }
    public virtual void lookdown()
    {
            action = ACTION.LOOK_DOWN;
    }

    protected virtual void EndMotion()
    {
        action=ACTION.IDLE;
    } 
}