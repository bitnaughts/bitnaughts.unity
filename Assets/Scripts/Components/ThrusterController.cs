using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   

    public float max_step = 25f;
    protected Vector3 thrust_vector;
    private float thrust = 0, thrust_min = 0, thrust_max = 0;

    public override float Action (float input) 
    {
        thrust = Mathf.Clamp(thrust + Mathf.Clamp(input, -max_step, max_step), 0, 100);

        // Updating local max and min for graphing purposes
        if (thrust < thrust_min) thrust_min = thrust;
        if (thrust > thrust_max) thrust_max = thrust;

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust;

        return thrust;
    }
    public Vector3 GetThrustVector() 
    {
        return transform.forward * thrust / 100f;
    }
    public Vector3 GetPosition() 
    {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }
    public override string ToString()
    {
        return "╠╕ Thruster Component: " + this.name + "\n╟┘ Thrust:   " + thrust_min.ToString("000") + "% " + Plot("ProgressBar", thrust, thrust_min, thrust_max, 10) + thrust_max.ToString("\t000") + "%";
    }
}