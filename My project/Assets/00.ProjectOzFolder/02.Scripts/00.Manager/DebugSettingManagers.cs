using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DebugSettingManagers : ManagedMentBased<DebugSettingManagers>
{
    protected DebugSettingManagers(){}

    [Range(1,1000)]
    public int mFontSize;

    public Text TextMessage;

    float mDeltaTime = 0.0f;

    //DebugSetting의 주 이용자는 프로그래밍적인 
    //기반이 거의 없는 기획자나, 디자이너가 주로 이용하므로
    //기존 명명 규칙을 무시한 공개적인 변수명을 사용합니다.
    public bool Frame;
    public bool Collider;
    public bool PlayerStat;
    public bool PlayerPos;

    public GameObject Players;

    public List<GameObject> DebugColliderObject;
    //콜리더 시각화 오브젝트

    string sFrame = "";
    string sCollider= "";
    string sPlayerStat= "";
    string sPlayerPos= "";
    private void Awake() {
        DebugColliderObject = new List<GameObject>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            foreach(GameObject A in DebugColliderObject)
            {
                A.GetComponent<DebugObjectScript>().SpriteToggle();
            }
        }

    }

}
