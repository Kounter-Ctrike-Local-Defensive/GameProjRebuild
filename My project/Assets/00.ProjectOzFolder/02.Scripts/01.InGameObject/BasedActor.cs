using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasedActor : MonoBehaviour
{
    
    protected SpriteRenderer  CspriteRender; 
    //모든 BasedActor은 SpriteReneder을 가집니다.
    bool isEnable = false;

    [SerializeField]
    public float applySpeed;                
    //물체의 현재 적용중인 스피드입니다.
    protected ISLOOK mLookDir;         
    //오브젝트가 어디를 보는지 지정합니다.
    protected Rigidbody2D Crigid;      
    //Rigidbody 컴포넌트는 존재하지만, AddRigid를 이용하여 따로 선언해주어야 합니다.
    public bool AutoSave;

    public virtual void Init()
    {
        CspriteRender = GetComponent<SpriteRenderer>();
        //Init은 Start()나 Awake()를 이용하지 않고 별도로 초기화하는 함수입니다.
        if(AutoSave)
        {
            GetComponent<Rito.UnityLibrary.EditorPlugins.PlayModeSaver>().AddTargetComponentToList(this);
        }
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
        if(mLookDir !=Dir)
        {
            mLookDir = Dir;
            if(mLookDir == ISLOOK.LEFT)
            {
                transform.eulerAngles = new Vector3(0,180,0);
            }
            else if(mLookDir == ISLOOK.RIGHT)
            {
                transform.eulerAngles = new Vector3(0,0,0);
            }
            
        }
        
    }

    protected virtual void OnValidate()
    {
        //에디터에서 변경시 실행되는 함수
    }

    public void MoveToForceX(float MoveVector,ForceMode2D Mode = ForceMode2D.Force)
    {
        Crigid.AddForce(Vector2.right * MoveVector * applySpeed ,Mode);
        if(Crigid.velocity.x > MoveVector)
            {
                Crigid.velocity = new Vector2(MoveVector,Crigid.velocity.y);
            }
        else if(Crigid.velocity.x < -MoveVector)
            {
                Crigid.velocity = new Vector2(MoveVector,Crigid.velocity.y);

            }

    }
    public void MoveToVal(float MoveVector)
    {
        Crigid.velocity = new Vector2(MoveVector,Crigid.velocity.y);

    }
    public virtual void ApplyDataOnInGameEditor()
    {
        
    }
}
