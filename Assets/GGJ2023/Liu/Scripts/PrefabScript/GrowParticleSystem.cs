using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowParticleSystem : MonoBehaviour
{


    ParticleSystem particle;
    float growthDuration = 5.0f;
    float timer = 0.0f;

    void Start()
    {

        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / growthDuration);

        var main = particle.main;
        main.startSpeed = Mathf.Lerp(0, 20, t);
        main.startSize = Mathf.Lerp(0.1f, 5.0f, t);
    }

}
