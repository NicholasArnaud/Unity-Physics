using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    [SerializeField]
    private HookesLaw.Particle particle;
    // Use this for initialization
    void Start()
    {
        particle = new HookesLaw.Particle();
        particle.AddForce(Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = particle.Update(Time.fixedDeltaTime);
    }
}
