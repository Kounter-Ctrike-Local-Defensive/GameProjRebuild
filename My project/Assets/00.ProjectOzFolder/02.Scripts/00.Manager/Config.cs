using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Manager
{
    public class Config : ManagedMentBased<Config>
    {
        public int MaxFrames=240;
        protected Config(){}

        public override void Init()
        {
            SetMaxFrame();
        }

        void SetMaxFrame()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = MaxFrames;
        }
    }

}
