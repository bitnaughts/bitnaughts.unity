/*
|| All components/subcomponents can be of radius 1, 2, 4, 8, 16, ...
|| A subcomponent's radius must be <= parent component's radius
|| A component must connect to other components of similiar relative radii  
||
|| Components:
||   Building blocks of ships and stations
|| = = = = = = = 
|| - Processor, runs code to control all other components (one open direction, all others sides can mount subcomponents)
|| - Scaffold, can control item flow between connected Bulkheads (all open directions, or can mount subcomponents)
|| - Bulkhead, holds items (two open sides, other sides can mount subcomponents)
|| - Factory, converts items into other items (two open sides [input, output], other sides can mount subcomponents)
|| - Constructor, materializes items into concrete shapes
||
|| Subcomponents:
||   Attaches to sides of things, giving better effects and abilities
|| = = = = = = =
|| - Gimbal, holds a second subcomponent, giving rotational control to it
|| - Collector, absorbs heat from sunlight
|| - Radiator, disperses heat
|| - Dehumidifier, absorbs water from space humidity
|| - Gun, breaks apart asteroids and ships alike
|| - Engine, provides thrust from steam (or water... or explosives...)
|| - Shield, protects against abrasive space dust and enemies alike
|| - Dock, links ships together
|| 
|| Perfect damage visualization system with marching cubes updating on taking damage? each matrix value is -1 -> 1 (health) As damaged, chunks of ship are eaten away.
||

*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public abstract class ComponentController : MonoBehaviour
{

    // public ComponentController[] connected_components;
    protected float temperature = .5f;
    protected float hitpoints;
    protected float action_speed = .1f, action_cooldown = 0f;

    protected string prefab_path;
    Color material_color;

    UIController component_panel;

    // Visualized via marching cubes... On update, update cubes to any granular damage
    void Start()
    {
        // var panel = GameObject.Find("ComponentPanel");
        component_panel = GameObject.Find("ComponentPanel")?.GetComponent<UIController>();
    }
    public Transform GetTransform()
    {
        return this.transform;
    }

    // void Update () {

    //     if (action_cooldown > 0) { /* Updates Component's action cooldown timer */
    //         action_cooldown = Mathf.Clamp (action_cooldown, 0, action_cooldown - Time.deltaTime);
    //     } else {
    //         action_cooldown = action_speed;
    //         Action();
    //     }
    //     //  print (this.temperature + ", " + action_cooldown + ", " + action_speed);
    //     // Visualize (); /* Updates Component's visual representation to current frame */
    // }
    public abstract float Action(float input);

    public void Remove()
    {
        // foreach (var part in parts) Destroy (part.Value);
        Destroy(this.gameObject);
    }

    void OnMouseUp()
    {
        component_panel.Set(this);
        // if (componentPanel == null) 
        // var structure_controller = GetComponentInParent(typeof(StructureController)) as StructureController;

    }

    public string Plot(string type, float value, float min, float max, int length)
    {
        string output = "";
        string style = "░▒▓█" + "─┼├┤" + "╔╚";
        float marker;
        int i = 0;
        switch (type)
        {
            case "ProgressBar":
                marker = Mathf.Clamp(value.Remap(min, max, 0, length), 0, length);
                if (float.IsNaN(marker)) return "";
                // if (marker == length) return style[3].ToString().Repeat(length);
                return (marker % 1 > 0) ? 
                    style[3].ToString().Repeat(Mathf.FloorToInt(marker)) + style[(int) (4 * (marker % 1))] + ' '.ToString().Repeat(Mathf.FloorToInt(length - marker)) : 
                    style[3].ToString().Repeat(Mathf.FloorToInt(marker)) + ' '.ToString().Repeat(Mathf.FloorToInt(length - marker));
            case "Marker":
                marker = Mathf.Clamp(value.Remap(min, max, 0, length), 0, length - .01f);
                if (float.IsNaN(marker)) return "";
                return new StringBuilder(style[4].ToString().Repeat(length)) 
                { 
                    [(int)marker] = style[5] 
                }.ToString();
        }
        return "";
    }
}