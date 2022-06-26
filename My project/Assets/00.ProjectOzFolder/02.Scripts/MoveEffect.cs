using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : EffectBased
{
    public static MoveEffect Instance;

    protected List<Queue<GameObject>> C_EffectPool;

    public bool[] bSenter;
    public float[] fYpos;

    private void Start()
    {
        Instance = this;

        C_EffectPool = new List<Queue<GameObject>>();

        for (int i = 0; i < C_Effect.Length; i++)
        {
            Queue<GameObject> _queue = new Queue<GameObject>();

            GameObject obj = Instantiate(C_Effect[i]);
            obj.transform.parent = GameObject.Find("PlayerMoveEffect").transform;
            _queue.Enqueue(obj);

            C_EffectPool.Add(_queue);
        }
    }

    public override IEnumerator EffectContinue(int i, Vector3 vec3)
    {
        if (C_EffectPool[i].Count == 0)
        {
            GameObject obj2 = Instantiate(C_Effect[i]);
            obj2.transform.parent = GameObject.Find("PlayerMoveEffect").transform;
            C_EffectPool[i].Enqueue(obj2);
        }

        GameObject obj;
        obj = C_EffectPool[i].Dequeue();

        obj.SetActive(true);
        obj.GetComponent<ParticleSystem>().Play();
        //obj.transform.rotation = Controller.instance.Player.transform.rotation;

        if (bSenter[i])
        {
            obj.GetComponent<Transform>().position
                = new Vector3(vec3.x, vec3.y + fYpos[i], vec3.z);
        }
        else//bottom 
        {
            Bounds boxBounds = Manager.Player.Inst.curPlayer.GetComponent<BoxCollider2D>().bounds;

            obj.GetComponent<Transform>().position
                = new Vector3(vec3.x,
                 boxBounds.center.y - boxBounds.extents.y + fYpos[i],
                 vec3.z);
        }

        if (i == 4) //공중 대쉬
        {
            float DashForce =
                Manager.Player.Inst.curPlayer.GetComponent<PlayerAbleCharacter>().mDash.applyforce;

            StartCoroutine(DashEffectCoroutine(obj));

            if (obj.transform.rotation.y == 0)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * DashForce, ForceMode2D.Impulse);
            }
            else
            {
                obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * DashForce, ForceMode2D.Impulse);
            }
        }
        yield return new WaitForSeconds(obj.GetComponent<ParticleSystem>().duration);
        obj.SetActive(false);

        C_EffectPool[i].Enqueue(obj);
    }
    private IEnumerator DashEffectCoroutine(GameObject _obj)
    {
        yield return new WaitForSeconds
            (Manager.Player.Inst.curPlayer.GetComponent<PlayerAbleCharacter>().mDash.applyTime);
        _obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
