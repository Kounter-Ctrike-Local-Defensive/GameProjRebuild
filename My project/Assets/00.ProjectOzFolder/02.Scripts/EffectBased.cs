using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBased : MonoBehaviour
{
    public GameObject []C_Effect;
        public virtual IEnumerator EffectContinue(int i, Vector3 vec3)
    {
        C_Effect[i].SetActive(true);
        C_Effect[i].GetComponent<Transform>().position = vec3;
        C_Effect[i].GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(C_Effect[i].GetComponent<ParticleSystem>().duration);
        C_Effect[i].SetActive(false);
    }
    public virtual IEnumerator EffectContinue(int i)
    {
        C_Effect[i].SetActive(true);
        C_Effect[i].GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(C_Effect[i].GetComponent<ParticleSystem>().duration);
        C_Effect[i].SetActive(false);
    }
    public virtual IEnumerator EffectContinue(int i, float time)
    {
        C_Effect[i].SetActive(true);
        C_Effect[i].GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(C_Effect[i].GetComponent<ParticleSystem>().duration);
        C_Effect[i].SetActive(false);
    }
}
