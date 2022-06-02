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
        if (GUILayout.Button("Save Object"))
        { 
            TargetActor.ApplyDataOnInGameEditor();
            TargetActor.gameObject.GetComponent<Rito.UnityLibrary.EditorPlugins.PlayModeSaver>().AddTargetComponentToBasedActor(
                TargetActor.GetComponent<BasedActor>());
        }
        base.OnInspectorGUI();
        
    }
}


