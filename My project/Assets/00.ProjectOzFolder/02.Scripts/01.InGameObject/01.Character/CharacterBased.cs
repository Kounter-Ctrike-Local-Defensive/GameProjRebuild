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
    public MotionDict Motion;
    
     public override void Init()
    {
        base.Init();
        SetSpeed(0);
        SetLookDir(ISLOOK.RIGHT);
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