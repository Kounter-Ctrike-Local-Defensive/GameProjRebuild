using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class Key : ManagedMentBased<Key>
    {
        protected   Key(){}

        public Dictionary<KeyCode,INPUT> dictKeys;
        //여러개의 키보드로 하나의 행동에 오버라이딩 할 수 있도록 만듭니다.
        public Dictionary<INPUT,KeyCode> inverse;
        //키보드로 Input타입을 찾기 위해서 inverse 딕셔너리 입니다.

        public override void Init()
        {
            base.Init();

            dictKeys = new Dictionary<KeyCode, INPUT>();
            inverse = new Dictionary<INPUT, KeyCode>();

            SettingToKeyBoardInput();
        }

        void SettingToKeyBoardInput()
        {
            AddKey(KeyCode.UpArrow      ,INPUT.LOOK_UP   );
            AddKey(KeyCode.DownArrow    ,INPUT.LOOK_DOWN );
            AddKey(KeyCode.LeftArrow    ,INPUT.LEFT     );
            AddKey(KeyCode.RightArrow   ,INPUT.RIGHT    );
            AddKey(KeyCode.Space        ,INPUT.JUMP         );
            AddKey(KeyCode.Z            ,INPUT.ATTACK       );
            AddKey(KeyCode.X            ,INPUT.INTERACTION  );
            AddKey(KeyCode.C            ,INPUT.TAG          );
            AddKey(KeyCode.Q            ,INPUT.TOGGLE_DEBUG );
            AddKey(KeyCode.LeftShift    ,INPUT.DASH         );
            AddKey(KeyCode.LeftControl, INPUT.TAGDOROTHY);
        }
            
        void AddKey(KeyCode AddKey, INPUT AddOptions)
        {
            dictKeys.Add(AddKey   ,AddOptions);
            inverse.Add(AddOptions,AddKey);
        }

        public bool GetAction(INPUT input)
        {
            return Input.GetKey(inverse[input]);
        }
        public bool GetActionDown(INPUT input)
        {
            return Input.GetKeyDown(inverse[input]);
        }
        public bool GetActionUp(INPUT input)
        {
            return Input.GetKeyUp(inverse[input]);
        }

    }
}