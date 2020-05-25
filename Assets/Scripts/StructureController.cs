using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour
{

    const float debug_duration = .2f;


    ComponentController[] components;

    protected Vector3 center_of_mass;

    protected int child_count;

    protected Transform rotator;
        
    // Start is called before the first frame update
    void Start()
    {
        print ("Structure Started!");
        components = GetComponentsInChildren<ComponentController>();

        rotator = transform.Find("Rotator");
        child_count = rotator.childCount;
        
    }

    float gimbal_test = 0f;
    float gimbal_step = 10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (child_count != rotator.childCount) Start();

        center_of_mass = Vector3.zero;
        float active_component_count = 0;
        foreach (var controller in components) {
           if (controller.enabled) {
                center_of_mass += controller.GetTransform().position;
                active_component_count++;
                switch (controller) {
                    case ProcessorController processor:
                        processor.Action(components);
                        break;
                }
           }
        }
        center_of_mass /= active_component_count;

        // Testing Center of Mass:
        // Debug.DrawLine(center_of_mass, center_of_mass + Vector3.up, Color.white, debug_duration, false);

        float rotation = 0f;
        Vector3 translation = new Vector3(0, 0, 0);


        for (int i = 0; i < components.Length; i++) 
        {
            if (components[i].enabled) switch (components[i]) {
                case ThrusterController thruster:

                    // Testing Thrust:
                    // Debug.DrawLine(thruster.GetPosition(), thruster.GetPosition() + thruster.GetThrustVector(), Color.red, debug_duration, false);
                    // Debug.DrawLine(thruster.GetPosition(), center_of_mass, Color.blue, debug_duration, false);

                    translation -= thruster.GetThrustVector();

                    float thrust_rotation = 15 * thruster.GetThrustVector().magnitude * Mathf.Sin(
                        Vector3.SignedAngle(
                            thruster.GetThrustVector(), 
                            thruster.GetPosition() - center_of_mass,
                            Vector3.up
                        ) * Mathf.Deg2Rad
                    );

                    rotation += thrust_rotation;
                    // print (i);
                    break;
                    
                // Testing Gimbals:
                // case GimbalController gimbal:
                //     gimbal_test = gimbal.Rotate(gimbal_step);
                //     if (gimbal_test >= 85f) gimbal_step = -10f;
                //     if (gimbal_test <= -85f) gimbal_step = 10f;
                //     break;
            }
        }
        rotator.Rotate(new Vector3(0, rotation, 0));
        transform.Translate(translation);
    }
}
