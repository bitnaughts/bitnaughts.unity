using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class GimbalController : ComponentController 
{   
    private Transform rotator;
    float rotation = 0, rotation_min = 0, rotation_max = 0, max_step = 25f;


    
    public override float Action (float input) 
    {
        if (rotator == null) rotator = transform.GetChild(0);

        rotation = rotator.localEulerAngles.y + Mathf.Clamp(input, -max_step, max_step); 
            
        if (rotation > 180f) rotation -= 360f;
        rotation = Mathf.Clamp(rotation, -179f, 179f);
        // if (rotation > 90f) rotation = 90f;
        // if (rotation < -180f) rotation = -180f;

        // Updating local max and min for graphing purposes
        if (rotation < rotation_min) rotation_min = rotation;
        if (rotation > rotation_max) rotation_max = rotation;

        rotator.localEulerAngles = new Vector3(0, rotation, 0);

        return rotation;
    }
    // Eventually move this ToString code to a dedicated formatting library call to structure everything into ascii tables neatly and usably (if running in no-textures mode (for pushing game to the limit...))
    public override string ToString()
    {
        string output = "╠╕ Gimbal Component: " + this.name + "\n╟┘ Rotation: " + rotation_min.ToString("00° ") + Plot("Marker", rotation, rotation_min, rotation_max, 10) + rotation_max.ToString("\t+00°");
        //+ "°"; //.ToString("0.0")
        return output;
    }
    
}