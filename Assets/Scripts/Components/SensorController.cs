using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class SensorController : ComponentController {   //RangeFinder == 1D, Scanner == 2D?

    // public float max_step = 25f;

    private float distance = 0, distance_min = 0, distance_max = 999, scan_range = 0;


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
    public override string ToString()
    {
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