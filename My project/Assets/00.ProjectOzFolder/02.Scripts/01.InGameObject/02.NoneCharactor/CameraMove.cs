using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    protected CameraMove() { }
    public Camera C_Camera;
    public GameObject ColliderToCamera;
    public GameObject Player;

    [Range(0, 5)]
    public float MarginPosSpeed;

    [Range(-15, 15)]
    public float CameraMarginPosY;
    [Range(-15, 15)]
    public float CameraMarginSizeX;
    [Range(-15, 15)]
    public float CameraMarginSizeY;

    [Range(0, 15)]
    public float MaxMarginPosX;
    [Range(0, 15)]
    public float MaxMarginPosY;

    [Range(0, 5)]
    public float MaxLookUpDistance;

    [Range(0, 5)]
    public float MaxLookDownDistance;

    [Range(0, 5)]
    public float LookSpeed;

    float CameraMarginPosX;

    Rigidbody2D ObjectRigidBody;
    Vector2 Range;

    public float PlayerLookVec;

    
    public bool EnableL;
    public bool EnableR;
    public bool EnableB;
    public bool EnableT;
    void Start()
    {
        EnableL = true;
        EnableR = true;
        EnableB = true;
        EnableT = true;
        PlayerLookVec =0;
    }

    private void OnValidate()
    {
        ColliderToCamera.transform.localPosition = new Vector3(CameraMarginPosX, CameraMarginPosY, 0);
        ColliderToCamera.transform.localScale = new Vector3(CameraMarginSizeX, CameraMarginSizeY, 0);
    }
}
