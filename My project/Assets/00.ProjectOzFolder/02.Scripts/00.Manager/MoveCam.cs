using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MoveCam : ManagedMentBased<MoveCam>
    {
        public Camera C_Camera;
        public GameObject ColliderToCamera;

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

        void Update()
        {
            ColliderToCamera.transform.localPosition = new Vector3(CameraMarginPosX, CameraMarginPosY, 0);
            ColliderToCamera.transform.localScale = new Vector3(CameraMarginSizeX, CameraMarginSizeY, 0);
            Vector3 PlayerPos = Player.Inst.curPlayer.transform.position;
            if (PlayerPos.y > transform.position.y + CameraMarginSizeY * 0.5f + CameraMarginPosY)
            {
                if (EnableT)
                {
                    //LayerMoveManageMent.Instance.UpdateScroll(0, Player.transform.position.y - (transform.position.y + CameraMarginSizeY * 0.5f + CameraMarginPosY), 0);
                    transform.Translate(0,PlayerPos.y- (transform.position.y + CameraMarginSizeY * 0.5f + CameraMarginPosY), 0);
                }
            }
            else if (PlayerPos.y < transform.position.y - CameraMarginSizeY * 0.5f + CameraMarginPosY)
            {
                if (EnableB)
                {
                    //LayerMoveManageMent.Instance.UpdateScroll(0, Player.transform.position.y - (transform.position.y - CameraMarginSizeY * 0.5f + CameraMarginPosY), 0);
                    transform.Translate(0, PlayerPos.y- (transform.position.y - CameraMarginSizeY * 0.5f + CameraMarginPosY), 0);
                }
            }


            if (PlayerPos.x > transform.position.x + CameraMarginSizeX * 0.5f + CameraMarginPosX)
            {
                if (EnableR)
                {
                    //LayerMoveManageMent.Instance.UpdateScroll(Player.transform.position.x - (transform.position.x + CameraMarginSizeX * 0.5f + CameraMarginPosX), 0, 0);
                    transform.Translate(PlayerPos.x - (transform.position.x + CameraMarginSizeX * 0.5f + CameraMarginPosX), 0, 0);
                    if (-MaxMarginPosX < CameraMarginPosX)
                    {
                        CameraMarginPosX -= Time.deltaTime * MarginPosSpeed;
                    }
                }
            }


            else if (PlayerPos.x < transform.position.x - CameraMarginSizeX * 0.5f + CameraMarginPosX)
            {
                if (EnableL)
                {
                    //LayerMoveManageMent.Instance.UpdateScroll(PlayerPos.x - (transform.position.x - CameraMarginSizeX * 0.5f + CameraMarginPosX), 0, 0);
                    transform.Translate(PlayerPos.x - (transform.position.x - CameraMarginSizeX * 0.5f + CameraMarginPosX), 0, 0);
                    if (MaxMarginPosX > CameraMarginPosX)
                    {
                        CameraMarginPosX += Time.deltaTime * MarginPosSpeed;
                    }
                }
            }

        }

    }
}