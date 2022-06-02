using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;


[System.Serializable]
    public class MotionDict : SerializableDictionary<ACTION,Motion>
    {

    }

public class CharacterBased : BasedActor
{
    [SerializeField]
    SpineObject spineObject;

    [SerializeField]
    public MotionDict Motions;
    public Motion doMotion;           //실제로 실행하는 모션입니다.
    public ACTION action;          
    public ACTION oldAction;        

    public STATE State;       
    

     public override void Init()
    {
        base.Init();
        SetLookDir(ISLOOK.RIGHT);
        oldAction = ACTION.IDLE;
        action = ACTION.IDLE;
        
    }

    public void SetMotion(ACTION input)
    {
        doMotion = Motions[input];
    }
    public void DoChangeAnim()
    {
        if(doMotion.Limit == ACTION_LIMIT.ONLY_TIME)
        {
            doMotion.isActiveMotion = true;
            StartCoroutine(DoActionOfCorutine(doMotion));
        }
        else
        {
        spineObject.ChangeAnim(doMotion);
        }
    }
    protected virtual void Update()
    {
        if(oldAction != action)
        {
            if(doMotion.Limit == ACTION_LIMIT.FREE)
            {
                oldAction = action;
                
                SetMotion(action);
                DoChangeAnim();
            }
            else if(doMotion.Limit == ACTION_LIMIT.ONLY_RELASE)
            {
                if(doMotion.CanConnectMotion == action)
                {
                    SetMotion(action);
                    oldAction = action;
                    DoChangeAnim();
                }
            }
            
        }
        else 
        {
            
        }
        
        
    }
    
        public virtual void DoTag()
    {
        Crigid.velocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
    public virtual void TargetTag() 
    {
        Crigid.velocity = Vector2.zero;
        this.gameObject.SetActive(true);
    }
    protected virtual IEnumerator DoActionOfCorutine(Motion CurMotion) 
    {
        spineObject.ChangeAnim(CurMotion);
        if(CurMotion.Limit == ACTION_LIMIT.ONLY_TIME)
        {
            yield return new WaitForSecondsRealtime(CurMotion.delay.Max);
            EndMotion(CurMotion);
        }
        else if(CurMotion.Limit == ACTION_LIMIT.RELASE_AND_TIME)
        {
            yield return new WaitForSecondsRealtime(doMotion.delay.Max);
        }
    }
    protected virtual void EndMotion(Motion CurMotion)
    {
        Debug.Log("End");
        SetMotion(CurMotion.EndAction);
        CurMotion.isActiveMotion =false;
        action = CurMotion.EndAction;
    } 
    public override void ApplyDataOnInGameEditor()
    {
        
    } 
}


[System.Serializable]
public struct Motion
{
    public Gauge delay;
    public bool isCanDoOther;
    public bool isLoop;
    public bool isReverse;
    public AnimationReferenceAsset animset;
    public ACTION CanConnectMotion;
    public ACTION EndAction;
    public ACTION_LIMIT Limit;
    public bool isActiveMotion;
}