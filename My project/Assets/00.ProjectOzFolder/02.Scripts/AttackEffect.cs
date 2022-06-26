using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : EffectBased
{
    public int[] iDamage;
    public float[] fAttackDelay;
    public override IEnumerator EffectContinue(int i, float time)
    {
        C_Effect[i].SetActive(true);
        C_Effect[i].GetComponent<ParticleSystem>().Play();

        StartCoroutine(ColliderContinue(i));
        yield return new WaitForSeconds(C_Effect[i].GetComponent<ParticleSystem>().duration);
        C_Effect[i].SetActive(false);
    }
    IEnumerator ColliderContinue(int i)
    {
        C_Effect[i].GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(fAttackDelay[i]);
        C_Effect[i].GetComponent<BoxCollider2D>().enabled = false;
    }
}
