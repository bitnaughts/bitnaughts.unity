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
        /* Operations Format: $0 $1 $2 (opt. $3)        */
        /* $0 == opcode                                 */
        /* $1 == variable reference                     */
        /* $2 == variable reference, or float constant  */
        /* $3 == component reference, for api calls     */
        Set       = "set", /* $1 = $2                   */
        /* Arithmetic                                   */
        Add       = "add", /* $1 += $2, or $1 = $1 + $2 */ 
        Subtract  = "sub", /* $1 -= $2, or $1 = $1 - $2 */
        Multiply  = "mul", /* $1 *= $2, or $1 = $1 * $2 */
        Divide    = "div", /* $1 /= $2, or $1 = $1 / $2 */
        Modulo    = "mod", /* $1 %= $2, or $1 = $1 % $2 */
        /* Trigonometry                                 */
        Sine      = "sin", /* $1 = Sin($2)              */
        Cosine    = "cos", /* $1 = Cos($2)              */
        Tangent   = "tan", /* $1 = Tan($2)              */
        /* Stack Manipulation via Pointer variable      */
        /* Interactivity                                */
        /* Components establish an "API Endpoint" using */
        /* an assigned Component ID, all Endpoints take */
        /* one analog parameter                         */
        /* given reference id                           */
        Function  = "fun", /* $1 = Cmp[$2].Function($3) */
        Get       = "get", /* $1 = Cmp[$2].Get($4)      */
        Print     = "pri"; /* Print($1)                 */

    public const string Variables = "var",
        Pointer = "ptr",
        Result = "res";

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

    public void Flash(Processor.Script script) 
    {
        this.script = script;

        this.script = new Processor.Script(
            "set rotation 10\n" + 
            "set gimbal 5\n" + 
            "set z 10\n" + 
            "add z 1\n" + 
            "fun gimbal 5\n" + 
            "pri res\n" +
            "\n" +
            "set ptr 3\n"
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
        // print (
        //     "Debugging line " + variables[Processor.Pointer] + ": " + script.lines[Mathf.RoundToInt(variables[Processor.Pointer])] + "\n" +
        //     string.Join(";", variables.Select(kvp => kvp.Key + ": " + kvp.Value))
        // );

        ops = script.lines[Mathf.RoundToInt(variables[Processor.Pointer])].Split(Processor.Space);
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
            case Processor.Print:
                print ("print " + Parse(ops[1]));
                break;
            case Processor.Function:
                variables[Processor.Result] = components[Mathf.RoundToInt(Parse(ops[1]))].Action(Parse(ops[2]));
                break;
        }
        variables[Processor.Pointer]++;
    }

    float parse_value = 0f;
    private float Parse(string input) 
    {
        if (float.TryParse(input, out parse_value)) 
        {
            return parse_value;
        }
        return variables[input];
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



