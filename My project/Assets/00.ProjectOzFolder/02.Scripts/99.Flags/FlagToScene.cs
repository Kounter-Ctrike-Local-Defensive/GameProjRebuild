using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class Tags
{
    public const string Enemy = "Enemy";
    
    public const string Floor = "Floor";
    public const string Wall = "Wall";
    public const string Debug = "Debug";
    public const string CameraLimitBackGround = "CameraLimitBackGround";
    public const string InGameGaugae = "InGameGaugae";

    public const string AreaTrigger = "Area";
}

static class Layer
{
    public const float DefaultCamZaxis = 20f;
}

static class AXIS{
    public const string Horizontal = "Horizontal";
}
public enum ViewOption
    {
        POSTION_AND_SIZE,
        SPEED,
        MOTION,
        ALL
    }