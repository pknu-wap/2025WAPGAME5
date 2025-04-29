using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    public ParticleSystem foodParticle;
    // Start is called before the first frame update
    void Start()
    {
        foodParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Interaction.gameStart)
        {

            if (Input.GetKeyDown("space") && !DrinkWater.isDrinking )
            {
                foodParticle.Stop();
                StartCoroutine(ParticleDelay());
            }
        }
    }
    IEnumerator ParticleDelay()
    {
        foodParticle.Play();
        yield return new WaitForSeconds(0.05f);
        foodParticle.Stop();

    }
    void OnDisable()
    {
        foodParticle.Stop();
    }
}
