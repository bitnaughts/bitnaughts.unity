using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class SensorController : ComponentController {   //RangeFinder == 1D, Scanner == 2D?

    // public float max_step = 25f;

    private float distance = 0, distance_min = 0, distance_max = 999, scan_range = 0;
    private Vector3 prevPosition;
    private GameObject ship, shipRotator;

    public void Start() {
        ship = GameObject.Find("/Ship");
        shipRotator = GameObject.Find("/Ship/Rotator");
        prevPosition = ship.transform.position;
    }

    public override float Action (float input) 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 999))
        {
            distance = hit.distance;
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * distance, Color.yellow, .1f, false);
            return distance;
        }
        else 
            distance = distance_max;
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward), Color.green, .1f, false);
            return distance_max;
        // return 0;
    }

    public float GetRotation () { // Probably will be put into a new sensor component later
        float rot = shipRotator.transform.eulerAngles.y; // Degrees clockwise from the negative z axis
        return rot;
    }

    public Vector2 GetVelocity() {
        // Without a rigidbody component, velocity is found from rate of change of distance
        Vector3 deltaPosition = ship.transform.position - prevPosition;
        prevPosition = ship.transform.position;
        Vector3 vel = deltaPosition / Time.deltaTime;
        return new Vector2(vel.x, vel.z) * -1; // (-left|+right, +forward|-backward) world space
    }

    public override string ToString()
    {   
        
        // Debug.Log(GetRotation());
        // Debug.Log(GetVelocity());

          //Move to "audio sensor" LOL, could actually be legit useful if actions cause sounds, local audio listener picks it up and can target it? sound-tracking missile...
        // string output = "";
        // float[] spectrum = new float[64];
        // var listener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        // AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        // for (int i = 0; i < 64; i++) {
        //     for (float j = 0; j < spectrum[i]; j += .01f)
        //     {
        //         output += "█";
        //     }
        //     output += "\n";
        // }
        return "╠╕ Sensor Component: " + this.name + "\n╟┘ Distance: " + distance_min.ToString("000") + " " + Plot("ProgressBar", distance, distance_min, distance_max, 20) + distance_max.ToString("\t000");
    }
}