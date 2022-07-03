using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MulitInfo
{
    public float X;
    public float Y;
}
public class MultiLayers : MonoBehaviour
{
    [SerializeField]
    GameObject[]Layers;
    [SerializeField]
    MulitInfo[] Pos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScrollLayer(float x,float y)
    {
        for(int i=0; i<Layers.Length;i++)
        {
            Layers[i].transform.position += new Vector3(x*Pos[i].X,y*Pos[i].Y,0);
        }
    }
}
