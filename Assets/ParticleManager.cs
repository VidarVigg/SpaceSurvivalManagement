using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public Quaternion particleRotation;

    private void Start()
    {
        gameObject.transform.rotation = particleRotation;
        StartCoroutine(RotateParticlesRoutine());
    }


    private IEnumerator RotateParticlesRoutine()
    {
        while (true)
        {
            particleRotation.z++;
            yield return null;
        }
        yield break;
    }



}
