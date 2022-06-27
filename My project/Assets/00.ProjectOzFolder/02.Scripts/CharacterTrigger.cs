using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{

    [SerializeField]
    bool LeftTrigger;
    [SerializeField]
    bool RightTrigger;
    void Start()
    {
    }

    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.transform.name);
        if(other.tag==Tags.Wall)
        {
            if(LeftTrigger)
            {
                Manager.Player.Inst.curPlayer.GetComponent<PlayerAbleCharacter>().OnWallLeft=true;
            }
            else if(RightTrigger)
            {
               Manager.Player.Inst.curPlayer.GetComponent<PlayerAbleCharacter>().OnWallRight=true;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag==Tags.Wall)
        {
            if(LeftTrigger)
            {
                Manager.Player.Inst.curPlayer.GetComponent<PlayerAbleCharacter>().OnWallLeft=false;
            }
            else if(RightTrigger)
            {
                Manager.Player.Inst.curPlayer.GetComponent<PlayerAbleCharacter>().OnWallRight=false;
            }
        }
    }
}
