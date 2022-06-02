using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(BasedActor),true)]
public class ApplyObjectButton : Editor
{
    public override void OnInspectorGUI()
    {

        BasedActor TargetActor = (BasedActor)target;
        if (GUILayout.Button("Apply Object"))
        { 
            TargetActor.ApplyDataOnInGameEditor();
        }
        base.OnInspectorGUI();
        
    }
}


