using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class CannonController : ComponentController {   

    public float max_step = 25f;

    protected Vector3 thrust_vector;
    private float throttle_level = 0;

    public override float Action (float input) 
    {
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
    public override string ToString()
    {
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

        string output = "Thruster1\nThrottle: [";
        for (int i = 0; i < (throttle_level / 10) - 1; i++)
        {
            output += "█";
        }
        float dif = throttle_level % 10;
        if (dif > 0) {
            if (dif < 2.5f) output += "░";
            else if (dif < 5f) output += "▒";
            else if (dif < 7.5f) output += "▓";
            else if (dif < 10f) output += "█";
        }
        if (throttle_level == 100) output += "█";
        for (int i = (int)((throttle_level / 10) + 1); i < 10; i++)
        {
            output += " ";
        }
        output += "] " + throttle_level.ToString("0.0") + "%";
        // return "Thruster: " + 
        return output;
    }
}