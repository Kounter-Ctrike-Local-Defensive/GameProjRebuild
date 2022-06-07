using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manager
{
    public class Core : ManagedMentBased<Core>
    {

        [SerializeField]
        GameObject Player;

        void Start()
        {
            Manager.Key.Inst.Init();
            Manager.Config.Inst.Init();
            Manager.Area.Inst.Init();
            Manager.Player.Inst.Init();
        }

        void Update()
        {
            Manager.Player.Inst.curPlayer = Player;
        }
        
    }
}
