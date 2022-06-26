using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorothy : PlayerAbleCharacter
{
    protected void Start()
    {
        Init();
        
    }

    protected override void Update()
    {
        base.Update();
    }
     protected override void FixedUpdate()
     {
         base.FixedUpdate();
     }
    public override void Init()
    {
        base.Init();
    }

    protected override void dash()
    {
        if (mDash.canDash)
        {
            StartCoroutine(DashMove(action));
            StartCoroutine(DashCool());
            if (transform.rotation.y == 0)
            {
                Crigid.AddForce(Vector2.right * mDash.applyforce*Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                Crigid.AddForce(Vector2.left * mDash.applyforce*Time.deltaTime, ForceMode2D.Impulse);
            }
            
            Debug.Log(Crigid.velocity);
        }
    }
    
}
