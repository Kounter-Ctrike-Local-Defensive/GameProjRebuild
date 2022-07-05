using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCameraCollider : MonoBehaviour
{
    public bool L;
    public bool R;
    public bool B;
    public bool T;
    public bool Drag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Enter()
    {
        if(L)
        {
            Manager.MoveCam.Inst.EnableL = false;
        }else if(R)
        {
            Manager.MoveCam.Inst.EnableR = false;
        }
        else if(B)
        {
            Manager.MoveCam.Inst.EnableB = false;
        }
        else if(T)
        {
            Manager.MoveCam.Inst.EnableT = false;
        }

    }
    void Exit()
    {
        if(L)
        {
            Manager.MoveCam.Inst.EnableL = true;
        }else if(R)
        {
            Manager.MoveCam.Inst.EnableR = true;
        }
        else if(B)
        {
            Manager.MoveCam.Inst.EnableB = true;
        }
        else if(T)
        {
            Manager.MoveCam.Inst.EnableT = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == Tags.CameraLimitBackGround)
            Enter();

    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.tag == Tags.CameraLimitBackGround){
            Exit();
        }
    }



}
