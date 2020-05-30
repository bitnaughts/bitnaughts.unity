using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   

    public float max_step = 45f;

    protected Vector3 thrust_vector;
    private float throttle_level = 0;

    public override float Action (float input) {
        throttle_level = Mathf.Clamp(throttle_level + Mathf.Clamp(input, -max_step, max_step), 0, 100);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = throttle_level;

        return throttle_level;
    }
    public Vector3 GetThrustVector() 
    {
        return transform.forward * throttle_level / 100f;
    }
    public Vector3 GetPosition() 
    {
        return transform.position;
    }
}