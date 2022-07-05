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
    public bool OnWallLeft;
    public bool OnWallRight;
    
    [SerializeField]
    Gauge MaxBufferJumpTime;

    [SerializeField]
    Gauge WallHangWalkTime;
    bool WaitWallHangEndWalkTime;
    bool WaitJumpBuffer;

    bool Holding;
    bool bOnFloor;
    bool WaitWalk;

    Vector2 WallHangPos;

    [SerializeField]
    BODY body;
    public override void Init()
    {
        base.Init();
        base.AddRigid();


        State = STATE.STAND;
        JumpCount.Max = DEBUG_JUMP_COUNT;
        bOnWall=false;
        bOnFloor=false;
        mDash.canDash=true;
        WaitWalk=true;
        Holding=false;
        SetMotion(ACTION.IDLE);
        ReSetJumpCount();

    }



    protected override void Update()
    {
        base.Update();
        if(action==ACTION.CLIMB_WALL)
        {
            if(mLookDir == ISLOOK.LEFT)
            MoveToVal(-body.WalkSpeed);
            else
            {
            MoveToVal(body.WalkSpeed);
            }
        }
        if(doMotion.isCanDoOther)
        {
            if(WaitJumpBuffer)
            {
                MaxBufferJumpTime.Val += Time.deltaTime;
                if(MaxBufferJumpTime.Val > MaxBufferJumpTime.Max)
                {
                    WaitJumpBuffer = false;
                }
            }
            if(WaitWallHangEndWalkTime)
            {
                WallHangWalkTime.Val +=Time.deltaTime;
                if(WallHangWalkTime.Val > WallHangWalkTime.Max)
                {
                    WaitWallHangEndWalkTime=false;
                }
            }
            
            if(Input.anyKey)
            {
                if(Manager.Key.Inst.GetAction(INPUT.LOOK_UP))
                    {
                        lookup();
                        Manager.MoveCam.Inst.LookUp = true;
                    }
                if(Manager.Key.Inst.GetAction(INPUT.LOOK_DOWN))
                    {
                        lookdown();
                        Manager.MoveCam.Inst.LookDown = true;
                    }
                
                if (Manager.Key.Inst.GetActionDown(INPUT.JUMP))
                {
                    if(bOnWall && State == STATE.WALLHANG)
                    {
                        State = STATE.FLOAT;
                        jump();
                        Crigid.velocity = new Vector2(Crigid.velocity.x, body.JumpPower);
                        if(OnWallRight)
                        {
                            if(mLookDir == ISLOOK.RIGHT)
                            {
                                Crigid.velocity = new Vector2(-body.WalkSpeed, Crigid.velocity.y);
                            }
                            else
                            {
                                Crigid.velocity = new Vector2(body.WalkSpeed, Crigid.velocity.y);
                            }
                        }
                        bOnWall=false;
                        action = ACTION.WALK;
                        WallHangWalkTime.Val = 0;
                        WaitWallHangEndWalkTime=true;
                        StartCoroutine(WalkCool(WallHangWalkTime.Max*0.5f));
                    }
                    else if(bOnWall && State == STATE.FLOAT &&!bOnFloor)
                    {
                        ReSetJumpCount();
                        action = ACTION.CLIMB_WALL;
                        State = STATE.WALLHANG;
                    }
                    else
                    {
                    jump();
                    }
                    
                    
                    
                }
                if(Manager.Key.Inst.GetActionDown(INPUT.DASH))
                {
                    dash();
                }
               
            }
            else if(action == ACTION.CLIMB_WALL)
            {

            }
            else
            {
                
                idle();
            }
            if(Manager.Key.Inst.GetActionUp(INPUT.LOOK_UP))
            {
                lookup_relase();
            }
            if(Manager.Key.Inst.GetActionUp(INPUT.LOOK_DOWN))
            {
                lookdown_relase();
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
            if(action !=ACTION.CLIMB_WALL && !WaitWallHangEndWalkTime)
            {
                Crigid.AddForce(Vector2.down * gravityScale*Time.deltaTime);
            
                if(Crigid.velocity.y < -MaxDownVelocity)
                {
                Crigid.velocity = new Vector2(Crigid.velocity.x,-MaxDownVelocity);
                }
            }
        }
        else if(State ==STATE.STAND){
            if(bOnWall)
            {
                Crigid.AddForce(Vector2.down * gravityScale*Time.deltaTime);
            
                if(Crigid.velocity.y < -MaxDownVelocity)
                {
                Crigid.velocity = new Vector2(Crigid.velocity.x,-MaxDownVelocity);
                }
            }
        }
        
        
    }

    protected virtual void OnTriggerStay2D(Collider2D other) 
    {
        if (other.transform.tag == Tags.Floor)
        {
            bOnFloor=true;
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.transform.tag == Tags.Wall)
        {
            bOnWall=true;
        }
        else if (other.transform.tag == Tags.Floor)
        {
            bOnFloor=true;
            State = STATE.STAND;
            ReSetJumpCount();
            if(WaitJumpBuffer)
            {
                jump();
                WaitJumpBuffer=false;
            }
        }
        
    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.transform.tag == Tags.Floor)
        {
            bOnFloor=false;
            if(State==STATE.STAND)
            {
            State= STATE.FLOAT;
            JumpCount.Val--;
            }
        }
        else if (other.transform.tag == Tags.Wall)
        {
            bOnWall=false;
            if(State != STATE.STAND)
            {
                ReSetJumpCount();
                JumpCount.Val--;
                State = STATE.FLOAT;
            }
        }
        

    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == Tags.AreaTrigger)
        {
            Manager.Area.Inst.ChangeTargetArea(other.gameObject);
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
    protected virtual IEnumerator WalkCool(float Time)
    {
        WaitWalk = false;
        yield return new WaitForSecondsRealtime(Time);
        WaitWalk = true;
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
            Crigid.velocity = new Vector2(0,Crigid.velocity.y);
        }
    }

    public virtual void walk() 
    {
        
        float MoveX = Input.GetAxisRaw(AXIS.Horizontal) * body.WalkSpeed;
        if(WaitWalk && !Holding)
        {
            if(bOnWall)
                MoveToForceX(MoveX);
            else
                MoveToVal(MoveX);
            action = ACTION.WALK;
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
            action=ACTION.JUMP;
            Debug.Log(Crigid.velocity);
        }
        else
        {
            WaitJumpBuffer = true;
            MaxBufferJumpTime.Val=0;
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









    public virtual void lookup_relase()
    {
            
            Holding=false;
            Manager.MoveCam.Inst.LookUp = false;
            Manager.MoveCam.Inst.RelaseDelay();
    }
    public virtual void lookdown_relase()
    {
            Holding=false;
            Manager.MoveCam.Inst.LookDown = false;
            Manager.MoveCam.Inst.RelaseDelay();
    }

    public virtual void lookup()
    {
            action = ACTION.LOOK_UP;
            Holding=true;
            Crigid.velocity = new Vector2(0,Crigid.velocity.y);
            Manager.MoveCam.Inst.startDelay();
    }
    public virtual void lookdown()
    {
            action = ACTION.LOOK_DOWN;
            Holding=true;
            Crigid.velocity = new Vector2(0,Crigid.velocity.y);
            Manager.MoveCam.Inst.startDelay();
    }


    protected virtual void EndMotion()
    {
        action=ACTION.IDLE;
    } 
}