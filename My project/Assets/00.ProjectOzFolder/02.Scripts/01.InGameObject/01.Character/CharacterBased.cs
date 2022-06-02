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
        spineObject.ChangeAnim(doMotion);
    }
    protected virtual void Update()
    {
        if(oldAction != action)
        {
            if(doMotion.isCanDoOther)
            {
                oldAction = action;
                SetMotion(action);
                spineObject.ChangeAnim(doMotion);
            }
            else
            {
                action = oldAction;
            }
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
        yield return new WaitForSeconds(doMotion.delay.Max);
    }
    protected virtual void EndMotion()
    {
        action = ACTION.IDLE;
        SetMotion(action);
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
    public AnimationReferenceAsset animset;
}