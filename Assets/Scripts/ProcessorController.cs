using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class Processor 
{
    /* "Analog" Assembly Language */
    public const string Operations = "ops",
        /* Operations Format: $0 $1 $2                 */
        /* $0 == opcode                                */
        /* $1 == variable reference                    */
        /* $2 == variable reference, or float constant */
        /* Variable Declarations                       */
        Set = "set", /* $1 = $2                        */
        /* Arithmetic                                  */
        Add      = "add", /* $1 += $2, or $1 = $1 + $2 */ 
        Subtract = "sub", /* $1 -= $2, or $1 = $1 - $2 */
        Multiply = "mul", /* $1 *= $2, or $1 = $1 * $2 */
        Divide   = "div", /* $1 /= $2, or $1 = $1 / $2 */
        Modulo   = "mod", /* $1 %= $2, or $1 = $1 % $2 */
        /* Trigonometry                                */
        Sine    = "sin", /* $res = Sin($1)             */
        Cosine  = "cos", /* $res = Cos($1)             */
        Tangent = "tan", /* $res = Tan($1)             */
        /* Stack Manipulation                          */
        Jump_If_Greater   = "jig", /* Goto $3 if $1 > $2 */
        Jump_If_Equal     = "jie", /* Goto $3 if $1 = $2 */
        Jump_If_Not_Equal = "jin", /* Goto $3 if $1 = $2 */
        Jump_If_Less      = "jil", /* Goto $3 if $1 < $2 */

        /* Interactivity                               */
        Function  = "fun", /* $res = Cmp[$1].Fx($2)    */
        Get       = "get", /* $res = Cmp[$1].Get($2)   */
        Print     = "pri"; /* Print($1)                */

    public const string Variables = "var",
        Pointer = "ptr",
        Result = "res",
        /* Input                                       */
        Input_W = "inw", /* Get Input of 'W'          */
        Input_A = "ina", /* Get Input of 'A'           */
        Input_S = "ins", /* Get Input of 'S'           */
        Input_D = "ind", /* Get Input of 'D'           */
        Input_Horz = "inh",
        Input_Vert = "inv";
        

    public const char Space = ' ',
        New_Line = '\n';

    public class Script
    {
        public string[] lines;

        public Script (string lines) { this.lines = lines.Split(New_Line); }
        public Script (string[] lines) { this.lines = lines; }

    }
}


public class ProcessorController : ComponentController 
{   

    protected Processor.Script script;
    protected string[] ops;

    protected Dictionary<string, float> variables;
    private float ptr_val = 0;

    public void Flash(Processor.Script script) 
    {
        this.script = script;
        this.script = new Processor.Script( // All Variables must be 3 letters long... because it make the code look good...
            new string[] {
                "set lef 5",
                "set rig 6",
                
                "jil inv 0 ",
                "jig inv 0 ",
                
                "fun lef -5",

                "fun lef 5",

                "jin inv 0 5", //no thrust level change, check gimbal opts
                "jin inh 0 9",

                "set ptr 2"
            }
        );
        variables = new Dictionary<string, float>();
        variables.Add(Processor.Pointer, 0);
        variables.Add(Processor.Result, 0); 
    }
    public override float Action(float input) 
    {
        return input;
    }
    public void Action (ComponentController[] components)
    {
        if (script == null) Flash(null);
        print (
            "Debugging line " + variables[Processor.Pointer] + ": " + script.lines[Mathf.RoundToInt(variables[Processor.Pointer])] + "\n" +
            string.Join(";", variables.Select(kvp => kvp.Key + ": " + kvp.Value))
        );
        ops = script.lines[Mathf.RoundToInt(variables[Processor.Pointer])].Split(Processor.Space);
        ptr_val = variables[Processor.Pointer];
        switch (ops[0])
        {
            case Processor.Set:
                if (variables.ContainsKey(ops[1]))
                    variables[ops[1]] = Parse(ops[2]);
                else
                    variables.Add(ops[1], Parse(ops[2]));
                break;
            case Processor.Add:
                variables[ops[1]] += Parse(ops[2]);
                break;
            case Processor.Subtract:
                variables[ops[1]] -= Parse(ops[2]); 
                break;
            case Processor.Multiply:
                variables[ops[1]] *= Parse(ops[2]);
                break;
            case Processor.Divide:
                variables[ops[1]] /= Parse(ops[2]); 
                break;
            case Processor.Sine:
                variables[ops[1]] = Mathf.Sin(Parse(ops[2])); 
                break;
            case Processor.Cosine:
                variables[ops[1]] = Mathf.Cos(Parse(ops[2])); 
                break;
            case Processor.Tangent:
                variables[ops[1]] = Mathf.Tan(Parse(ops[2])); 
                break;
            case Processor.Jump_If_Greater:
                if (Parse(ops[1]) > Parse(ops[2]))
                    variables[Processor.Pointer] = Parse(ops[3]);
                break;
            case Processor.Jump_If_Equal:
                if (Parse(ops[1]) == Parse(ops[2]))
                    variables[Processor.Pointer] = Parse(ops[3]);
                break;
            case Processor.Jump_If_Not_Equal:
                if (Parse(ops[1]) != Parse(ops[2]))
                    variables[Processor.Pointer] = Parse(ops[3]);
                break;
            case Processor.Jump_If_Less:
                if (Parse(ops[1]) < Parse(ops[2]))
                    variables[Processor.Pointer] = Parse(ops[3]);
                break;
            case Processor.Print:
                print ("print " + Parse(ops[1]));
                break;
            case Processor.Function:
                variables[Processor.Result] = components[Mathf.RoundToInt(Parse(ops[1]))].Action(Parse(ops[2]));
                break;
        }
        // If line pointer is unchanged, step to next instruction
        if (ptr_val == variables[Processor.Pointer]) variables[Processor.Pointer]++;
    }

    float parse_value = 0f;
    private float Parse(string input) 
    {
        if (float.TryParse(input, out parse_value)) 
        {
            return parse_value;
        }
        parse_value = 1f;
        if (input[0] == '-') 
        {
            parse_value = -1f;
            input = input.Substring(1);
        }
        switch (input) 
        {
            case Processor.Input_W:
                return Input.GetKey(KeyCode.W) ? parse_value : 0;
            case Processor.Input_A:
                return Input.GetKey(KeyCode.A) ? parse_value : 0;
            case Processor.Input_S:
                return Input.GetKey(KeyCode.S) ? parse_value : 0;
            case Processor.Input_D:
                return Input.GetKey(KeyCode.D) ? parse_value : 0;
            case Processor.Input_Vert:
                return Input.GetAxis("Vertical");
            case Processor.Input_Horz:
                return Input.GetAxis("Horizontal");
        }
        return parse_value * variables[input];
    }

    // public bool Action () {
    //     if (obj.action_cooldown == 0) { /* Component has finished cooling down from previous action */
    //         obj.action_cooldown = obj.action_speed;
    //         return true;
    //     }
    //     return false;
    // }
    // public void Remove () {
    //     foreach (var part in parts) Destroy (part.Value);
    //     Destroy (this.gameObject);
    // }
}

    // public Component (PointF center, SizeF size, int rotation) {
        // this.hitpoints = 1;
        // this.action_speed = 10;
        // this.prefab_path = "Prefabs/Components/Hardpoint";
        // this.color = ColorF.white;
    // }
    // public Component (dynamic json) {
        // this.center = new PointF (json.center);
        // this.size = new SizeF (json.size);
        // this.rotation = json.rotation;
        // this.hitpoints = json.hitpoints;
        // this.action_speed = json.action_speed;
        // this.action_speed = json.action_cooldown;
        // this.prefab_path = json.prefab_path;
        // this.color = new ColorF (json.color);
    // }
    // public JObject ToJObject () {
    //     dynamic json = new JObject ();
    //     json.type = this.GetType ().ToString ();
    //     json.center = center.ToJObject ();
    //     json.size = size.ToJObject ();
    //     json.rotation = rotation;
    //     json.hitpoints = hitpoints;
    //     json.action_speed = action_speed;
    //     json.prefab_path = prefab_path;
    //     json.color = color.ToJObject ();
    //     return json;
    // }
    // public override string ToString () { return ToJObject ().ToString (); }
// }
// public abstract class Controller<T> : MonoBehaviour {

//     public T obj;

//     public abstract void Visualize ();
//     public abstract void Initialize (T obj);
// }

// public abstract class ComponentController<T> : Controller<T> where T : Component {


//     // void GetGameObject() {
//     //     return this.gameObject;
//     // }
// }



