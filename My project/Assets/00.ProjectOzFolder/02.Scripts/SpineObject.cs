using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class SpineObject : MonoBehaviour
{
    protected SkeletonAnimation SkeletonAnimation; 

    void Start()
    {
        SkeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    public void ChangeAnim(Motion Anim)
    {
        if(SkeletonAnimation.name.Equals(Anim.animset))
                return;
            SkeletonAnimation.state.SetAnimation(0,Anim.animset,Anim.isLoop);
            SkeletonAnimation.loop=Anim.isLoop;
            if(Anim.isReverse)
                SkeletonAnimation.timeScale = -1f;
            else
                SkeletonAnimation.timeScale = 1f;
    }
}
