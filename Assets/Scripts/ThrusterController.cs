using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   
    protected Vector3 thrust_vector;
    public override float Action (float input) {
        return input;
        
        // temperature = connected_components[0].Cool(0f);
        // var main = GetComponent<ParticleSystem>().main;
        // main.startSize = temperature * 20f;



        // thrust_vector = new Vector3(
        //     -(float)Math.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * temperature,
        //     0,
        //     (float)Math.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * temperature
        // );

        // Debug.DrawLine(
        //     transform.position, 
        //     transform.position + thrust_vector, Color.white, 10f, false
        // );
    }
    public Vector3 GetThrustVector() 
    {
        return transform.forward * temperature;
    }
    public Vector3 GetPosition() 
    {
        return transform.position;
    }
}