using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
public class Area : ManagedMentBased<Area>
{

    public MultiLayers Target;

    public void ChangeTargetArea(GameObject TargetArea)
    {
        Target = TargetArea.GetComponent<MultiLayers>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
}