//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ParticleManager : MonoBehaviour
//{
//    public Quaternion particleRotation;
//    float test;

//    private void Start()
//    {
//        particleRotation = transform.rotation;
//        StartCoroutine(RotateParticles());
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.N))
//        {
//            particleRotation.z += 1;
//        }
//    }

//    private IEnumerator RotateParticles()
//    {

//        while (true)
//        {
//            particleRotation.z += 1;
//            gameObject.transform.rotation = particleRotation;
//            yield return null;
//        }
//        yield break;
//    }



//}
