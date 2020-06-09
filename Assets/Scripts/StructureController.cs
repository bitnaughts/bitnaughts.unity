using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StructureController : MonoBehaviour
{
    const float debug_duration = .02f;
    Dictionary<string, ComponentController> components;
    protected Vector3 center_of_mass;
    protected int child_count;
    protected Transform rotator;       
    RaycastHit hit;
    void Start()
    {
        print ("Structure Starting!");
        components = new Dictionary<string, ComponentController>();
        foreach (var controller in GetComponentsInChildren<ComponentController>()) 
        {
            components.Add(controller.name, controller);
        }

        rotator = transform.Find("Rotator");
        child_count = rotator.childCount;

        // foreach (var controller in components) {
        //     controller.Init(components);
        // }
    }

    float gimbal_test = 0f;
    float gimbal_step = 10f;
    void Update()
    {
        if (child_count != rotator.childCount) Start();

        center_of_mass = Vector3.zero;
        float active_component_count = 0;
        foreach (var controller in components.Values) {
           if (controller.enabled) {
                center_of_mass += new Vector3(controller.GetTransform().position.x, 0, controller.GetTransform().position.z);
                active_component_count++;
                switch (controller) {
                    case ProcessorController processor:
                        processor.Action(components);
                        break;
                }
           }
        }
        GameObject.Find("Debug").GetComponent<Text>().text = string.Join("\n", components.Values);

        center_of_mass /= active_component_count;

        // Testing Center of Mass:
        Debug.DrawLine(center_of_mass, center_of_mass + Vector3.up, Color.white, debug_duration, false);

        //Checking surrounding components
        // if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            // print("Found an object - distance: " + hit.distance);


        float rotation = 0f;
        Vector3 translation = new Vector3(0, 0, 0);

        foreach (var controller in components.Values)
        {
            if (controller.enabled) switch (controller) {
                case ThrusterController thruster:
                    Debug.DrawLine(thruster.GetPosition(), thruster.GetPosition() + thruster.GetThrustVector(), Color.red, debug_duration, false);
                    Debug.DrawLine(thruster.GetPosition(), center_of_mass, Color.blue, debug_duration, false);

                    translation -= thruster.GetThrustVector();
                    float thrust_rotation = 15 * thruster.GetThrustVector().magnitude * Mathf.Sin(
                        Vector3.SignedAngle(
                            thruster.GetThrustVector(), 
                            thruster.GetPosition() - center_of_mass,
                            Vector3.up
                        ) * Mathf.Deg2Rad
                    );
                    rotation += thrust_rotation;
                    break;
            }
        }
        rotator.Rotate(new Vector3(0, rotation / active_component_count, 0));
        transform.Translate(translation / active_component_count);
    }
}

public static class ExtensionMethods {
    public static float Remap (this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    // Or IsNanOrInfinity
    public static bool HasValue(this float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }
    public static string Repeat(this string s, int n)
    {
        return String.Concat(Enumerable.Repeat(s, n));
    }
}