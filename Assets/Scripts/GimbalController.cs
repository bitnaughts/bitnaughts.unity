using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class GimbalController : ComponentController {   

    public float max_step = 5f;
    private Transform rotator;

    public override float Action (float input) {
        return Rotate(input);
    }

    public float Rotate (float degrees) //All Component Interfaces should operate with "analog" values (floats)
    {
        if (rotator == null) rotator = transform.GetChild(0);

        float rotation = rotator.localEulerAngles.y + degrees;//Mathf.Clamp(degrees, -max_step, max_step); 
            
        if (rotation > 180f) rotation -= 360f;
        if (rotation > 90f) rotation = 90f;
        if (rotation < -90f) rotation = -90f;

        rotator.localEulerAngles = new Vector3(0, rotation, 0);

        return rotation;
    }
}