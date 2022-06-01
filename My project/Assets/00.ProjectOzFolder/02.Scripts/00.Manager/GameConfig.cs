using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : ManagedMentBased<GameConfig>
{
    public int MaxFrames=240;
    protected GameConfig(){}
    void Start()
    {
        SetMaxFrame();
    }

    void Update()
    {
        
    }


    void SetMaxFrame()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxFrames;
    }
}
