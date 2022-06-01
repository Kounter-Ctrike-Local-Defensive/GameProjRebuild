using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasedActor : MonoBehaviour
{
    
    protected SpriteRenderer  CspriteRender; 
    //모든 BasedActor은 SpriteReneder을 가집니다.
    bool isEnable = false;

    [SerializeField]
    float applySpeed;                
    //물체의 현재 적용중인 스피드입니다.
    protected ISLOOK mLookDir;         
    //오브젝트가 어디를 보는지 지정합니다.
    protected Rigidbody2D Crigid;      
    //Rigidbody 컴포넌트는 존재하지만, AddRigid를 이용하여 따로 선언해주어야 합니다.

    public virtual void Init()
    {
        CspriteRender = GetComponent<SpriteRenderer>();
        //Init은 Start()나 Awake()를 이용하지 않고 별도로 초기화하는 함수입니다.
    }
    public void SetSpeed(float speed)
    {
        applySpeed = speed;
    }
    public void MoveTranslate(Vector3 TranslateVector)
    {
        transform.Translate(TranslateVector);
    }
    public void AddRigid()
    {
        Crigid = GetComponent<Rigidbody2D>();
    }
    public void SetLookDir(ISLOOK Dir)
    {
        mLookDir =Dir;
    }

}
