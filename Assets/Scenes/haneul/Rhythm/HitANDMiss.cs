using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitANDMiss : MonoBehaviour
{
    public float lifeTime = 0.3f;

    void Start()
    {
        StartCoroutine(DestroyAfterLifeTime());
    }

    private IEnumerator DestroyAfterLifeTime()
    {

        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }

}
