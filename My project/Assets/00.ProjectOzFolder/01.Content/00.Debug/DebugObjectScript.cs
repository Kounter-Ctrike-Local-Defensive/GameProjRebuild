using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DebugObjectScript : MonoBehaviour
{
    public Color color = Color.white;

    bool ToggleOutLine = true;
    [Range(0, 16)]
    public int outlineSize = 1;
    private SpriteRenderer spriteRenderer;
    private void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DebugSettingManagers.Inst.DebugColliderObject.Add(gameObject);
    }
    void Update() 
    {
        UpdateOutline(ToggleOutLine);
    }

    void UpdateOutline(bool outline) 
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }
    public void SpriteToggle()
    {
        if(ToggleOutLine)
        {
            ToggleOutLine = false;
            spriteRenderer.enabled = false;
        }
        else
        {
            ToggleOutLine = true;
            spriteRenderer.enabled = true;
        }
        UpdateOutline(ToggleOutLine);
    }
}
