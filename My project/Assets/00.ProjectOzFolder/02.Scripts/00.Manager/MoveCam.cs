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

        public bool LookUp;
        public bool LookDown;
        

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
            
            MoveToCameraFollowPlayer();             //플레이어의 위치에 따른 카메라 스크롤 이동

            MoveToCameraActionPlayer();

            C_Camera.transform.localPosition = new Vector3(C_Camera.transform.localPosition.x,PlayerLookVec,C_Camera.transform.localPosition.z);

        }


        void MoveToCameraFollowPlayer()
        {
            Vector3 PlayerPos = Player.Inst.curPlayer.transform.position;
            if (PlayerPos.y > transform.position.y + CameraMarginSizeY * 0.5f + CameraMarginPosY)
            {
                if (EnableT)
                {
                    Area.Inst.Target.ScrollLayer(0, PlayerPos.y - (transform.position.y + CameraMarginSizeY * 0.5f + CameraMarginPosY));
                    transform.Translate(0,PlayerPos.y- (transform.position.y + CameraMarginSizeY * 0.5f + CameraMarginPosY), 0);
                }
            }
            else if (PlayerPos.y < transform.position.y - CameraMarginSizeY * 0.5f + CameraMarginPosY)
            {
                if (EnableB)
                {
                    Area.Inst.Target.ScrollLayer(0, PlayerPos.y - (transform.position.y - CameraMarginSizeY * 0.5f + CameraMarginPosY));
                    transform.Translate(0, PlayerPos.y- (transform.position.y - CameraMarginSizeY * 0.5f + CameraMarginPosY), 0);
                }
            }


            if (PlayerPos.x > transform.position.x + CameraMarginSizeX * 0.5f + CameraMarginPosX)
            {
                if (EnableR)
                {
                    Area.Inst.Target.ScrollLayer(PlayerPos.x - (transform.position.x + CameraMarginSizeX * 0.5f + CameraMarginPosX), 0);
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
                    Area.Inst.Target.ScrollLayer(PlayerPos.x - (transform.position.x - CameraMarginSizeX * 0.5f + CameraMarginPosX), 0);
                    transform.Translate(PlayerPos.x - (transform.position.x - CameraMarginSizeX * 0.5f + CameraMarginPosX), 0, 0);
                    if (MaxMarginPosX > CameraMarginPosX)
                    {
                        CameraMarginPosX += Time.deltaTime * MarginPosSpeed;
                    }
                }
            }
        }

        void MoveToCameraActionPlayer()
        {
            if(LookUp)
            {
                if(PlayerLookVec< MaxLookDownDistance)
                {
                    PlayerLookVec += Time.deltaTime * LookSpeed;
                    
                }
                if(Manager.Player.Inst.curPlayer.GetComponent<CharacterBased>().action != ACTION.LOOK_UP)
                        LookUp=false;
                    
            }
        else if(LookDown)
        {
            if(PlayerLookVec> -MaxLookDownDistance)
            {
                PlayerLookVec -= Time.deltaTime * LookSpeed;
                
            }
            if(Manager.Player.Inst.curPlayer.GetComponent<CharacterBased>().action != ACTION.LOOK_DOWN)
                        LookDown=false;
                        
        }
        else
        {
            if(PlayerLookVec> 0.05)
            {
                PlayerLookVec -= Time.deltaTime * LookSpeed;
            }
            else if(PlayerLookVec< -0.05)
            {
                PlayerLookVec += Time.deltaTime * LookSpeed;
            }
            else
            {
                PlayerLookVec=0;
            }

        }
        }

        

    }
}
