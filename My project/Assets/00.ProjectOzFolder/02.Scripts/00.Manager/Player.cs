using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class Player : ManagedMentBased<Player>
    {
        protected Player(){}
        
        public bool hasScarecrow = true;
        public bool hasLion = false;
        public bool hasWoodcutter = true; //캐릭터 획득 유무
        public bool bCanTag {get;set;}           //태그 가능한 상황임을 나타냅니다.
    
        public int attackLev{get;set;}

        public GameObject curPlayer;
        public PlayerAbleCharacter curPlayerScript;

        void Start()
        {
            
        }
        void Update()
        {
            
        }
        public override void Init()
        {
            
        }
        public void SettingPlayerObj(GameObject Obj)
        {
            curPlayer = Obj;
            curPlayerScript = Obj.GetComponent<PlayerAbleCharacter>();
        }
        public void PlayerDoTag(string Tag)
        {
            if (bCanTag)
            {
                NoConditionTag(Tag);
            }
        }
        private void NoConditionTag(string Tag)
        {
            
        }

    }
}
