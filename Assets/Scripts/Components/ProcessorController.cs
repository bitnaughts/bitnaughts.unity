using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
        Add = "add", /* $1 += $2, or $1 = $1 + $2 */
        Subtract = "sub", /* $1 -= $2, or $1 = $1 - $2 */
        Multiply = "mul", /* $1 *= $2, or $1 = $1 * $2 */
        Divide = "div", /* $1 /= $2, or $1 = $1 / $2 */
        Modulo = "mod", /* $1 %= $2, or $1 = $1 % $2 */
        /* Trigonometry                                */
        Sine = "sin", /* $res = Sin($1)             */
        Cosine = "cos", /* $res = Cos($1)             */
        Tangent = "tan", /* $res = Tan($1)             */
        /* Stack Manipulation                          */
        Jump = "jum", /* Goto $1 */
        Jump_If_Greater = "jig", /* Goto $3 if $1 > $2 */
        Jump_If_Equal = "jie", /* Goto $3 if $1 = $2 */
        Jump_If_Not_Equal = "jin", /* Goto $3 if $1 = $2 */
        Jump_If_Less = "jil", /* Goto $3 if $1 < $2 */

        /* Interactivity                               */
        Component = "com", /* $res = component[$1].Action($2) */
        Get = "get", /* $res = Cmp[$1].Get($2)   */
        Print = "pri"; /* Print($1)                */

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
}
public class Instruction
{
    int red_color = 0;
    public string label = ""; //If "", default to showing line number
    public string label_divider = ":";
    public string op_code;
    public string dest_reg, src_reg, src_reg_2;
    // public string[] operands;
    public string operand_divider = "  ";
    // string[] ops;
    public Instruction(string line)
    {
        var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        label = parts.Length > 0 ? parts[0] : null;
        op_code = parts.Length > 1 ? parts[1] : null;
        dest_reg = parts.Length > 2 ? parts[2] : null;
        src_reg = parts.Length > 3 ? parts[3] : null;
        src_reg_2 = parts.Length > 4 ? parts[4] : null;
    }
    public Instruction Get()
    {
        red_color = 255;
        return this;
    }
    public override string ToString()
    {
        string hex = Convert.ToString(red_color, 16);
        if (hex.Length == 1) hex = "0" + hex;
        string output = "<color=#" + hex + "0000>" + label + " " + op_code + " " + dest_reg + " " + src_reg + " " + src_reg_2 + "</color>";
        red_color = (int)(red_color * .5f);
        return output;
    }
}

public class ProcessorController : ComponentController
{
    private List<Instruction> instructions; // Size restricted by static memory
    protected Dictionary<string, float> variables; // Size restricted by random access memory

    public void Init(string instructions) { Init(instructions.Split('\n')); }
    public void Init(string[] instructions)
    {
        this.instructions = new List<Instruction>();
        foreach (var inst in instructions) this.instructions.Add(new Instruction(inst));
        variables = new Dictionary<string, float>();
        variables.Add(Processor.Pointer, 0);
        variables.Add(Processor.Result, 0);
    }
    public override float Action(float input)
    {
        return input;
    }
    float pointer;
    public void Action(Dictionary<string, ComponentController> components)
    {
        if (instructions == null) Init(
            new string[] {
                // "cww com gim 5",     //2: Turn Gimbal 5 degrees
                // "004 jie res 90 ccw",  //3: If resulting Gimbal angle is equal to 90, go to line 6
                // "005 com thr 1",   //4: Increase/decrease thrust based on resulting angle (arbitrary line)
                // "006 jum cww",     //5: Go to line 2 (keep turning gimbal until its equal to 90)
                // "ccw com gim -5",    //6: Turn Gimbal -5 degrees
                // "008 jie res -90 cww", //7: If resulting Gimbal angle is equal to -90, go to line 2
                // "009 com thr -1",   //8: Increase/decrease thrust based on resulting angle (arbitrary line)
                // "010 jum ccw"      //9: Go to line 6 (keep turning gimbal until tis equal to -90)

                // Super Simple Self-Controlled Flier
                // "STR com thr inv",
                // "001 com thr_gim inh",
                // "002 com sen 10",
                // "003 com sen_gim inh",
                // "004 jum STR",

                "STR com sen_gim 5",
                "--- com sen 1",
                "--- jin inv 0 VRT",
                "--- jie inv 0 V_R",
                "L_C jig inh 0 LFT",
                "R_C jil inh 0 RGT",
                "--- jie inh 0 H_R",
                "VRT com thr 10", 
                "--- jum L_C",
                "V_R com thr -20", 
                "--- jum L_C",
                "LFT com l_thr 10", 
                "--- jum R_C",
                "RGT com r_thr 10", 
                "--- jum STR",
                "H_R com l_thr -20",
                "--- com r_thr -20",
                "--- jum STR",
                // "004 mul vert 5",
                // "005 fun 6 vert",
                // "006 jum STR",
                // "HRZ set horz inh",
                // "008 mul horz 5",
                // "009 fun 5 horz",
                // "010 jum STR",

                // "STR set vert inv",
                // "001 mul vert 5",
                // "002 fun 6 vert",
                // "003 set horz inh",
                // "004 mul horz 5",
                // "005 fun 5 horz",
                // "END set ptr STR"

                // "001 set gimbal 5",     //0: Gimbal Id = 5, its the 6th element in Components
                // "002 set thruster 6",     //1: Thruster Id = 6, its the 7th element in Components 
                // "cww fun gimbal 5",     //2: Turn Gimbal 5 degrees
                // "004 jie res 90 ccw",  //3: If resulting Gimbal angle is equal to 90, go to line 6
                // "005 fun thruster res",   //4: Increase/decrease thrust based on resulting angle (arbitrary line)
                // "006 set ptr cww",     //5: Go to line 2 (keep turning gimbal until its equal to 90)
                // "ccw fun gimbal -5",    //6: Turn Gimbal -5 degrees
                // "008 jie res -90 cww", //7: If resulting Gimbal angle is equal to -90, go to line 2
                // "009 fun thruster res",   //8: Increase/decrease thrust based on resulting angle (arbitrary line)
                // "010 set ptr ccw"      //9: Go to line 6 (keep turning gimbal until tis equal to -90)
            }
        );
        var inst = GetInstruction(Mathf.RoundToInt(variables[Processor.Pointer]));
        pointer = variables[Processor.Pointer];
        switch (inst.op_code)
        {
            case Processor.Set:
                if (variables.ContainsKey(inst.dest_reg))
                    variables[inst.dest_reg] = Parse(inst.src_reg);
                else
                    variables.Add(inst.dest_reg, Parse(inst.src_reg));
                break;
            case Processor.Add:
                if (inst.src_reg_2 == null)
                    variables[inst.dest_reg] += Parse(inst.src_reg);
                else
                    variables[inst.dest_reg] = Parse(inst.src_reg) + Parse(inst.src_reg_2);
                break;
            case Processor.Subtract:
                if (inst.src_reg_2 == null)
                    variables[inst.dest_reg] -= Parse(inst.src_reg);
                else
                    variables[inst.dest_reg] = Parse(inst.src_reg) - Parse(inst.src_reg_2);
                break;
            case Processor.Multiply:
                if (inst.src_reg_2 == null)
                    variables[inst.dest_reg] *= Parse(inst.src_reg);
                else
                    variables[inst.dest_reg] = Parse(inst.src_reg) * Parse(inst.src_reg_2);
                break;
            case Processor.Divide:
                if (inst.src_reg_2 == null)
                    variables[inst.dest_reg] /= Parse(inst.src_reg);
                else
                    variables[inst.dest_reg] = Parse(inst.src_reg) / Parse(inst.src_reg_2);
                break;
            case Processor.Sine:
                variables[inst.dest_reg] = Mathf.Sin(Parse(inst.src_reg));
                break;
            case Processor.Cosine:
                variables[inst.dest_reg] = Mathf.Cos(Parse(inst.src_reg));
                break;
            case Processor.Tangent:
                variables[inst.dest_reg] = Mathf.Tan(Parse(inst.src_reg));
                break;
            case Processor.Jump:
                variables[Processor.Pointer] = Parse(inst.dest_reg);
                break;
            case Processor.Jump_If_Greater:
                if (Parse(inst.dest_reg) > Parse(inst.src_reg))
                    variables[Processor.Pointer] = Parse(inst.src_reg_2);
                break;
            case Processor.Jump_If_Equal:
                if (Parse(inst.dest_reg) == Parse(inst.src_reg))
                    variables[Processor.Pointer] = Parse(inst.src_reg_2);
                break;
            case Processor.Jump_If_Not_Equal:
                if (Parse(inst.dest_reg) != Parse(inst.src_reg))
                    variables[Processor.Pointer] = Parse(inst.src_reg_2);
                break;
            case Processor.Jump_If_Less:
                if (Parse(inst.dest_reg) < Parse(inst.src_reg))
                    variables[Processor.Pointer] = Parse(inst.src_reg_2);
                break;
            case Processor.Component:
                variables[Processor.Result] = components[inst.dest_reg].Action(Parse(inst.src_reg));
                break;
        }
        if (pointer == variables[Processor.Pointer]) variables[Processor.Pointer]++; // If line pointer is unchanged, step to next instruction

        // print(ToString());

        // var lines = variables.Select(kvp => kvp.Key + ": " + kvp.Value);
        // print(string.Join(Environment.NewLine, lines));
        // print (
        //     "Debugging line " + variables[Processor.Pointer] + ": " + script.lines[Mathf.RoundToInt(variables[Processor.Pointer])] + "\n" +
        //     string.Join(";", variables.Select(kvp => kvp.Key + ": " + kvp.Value))
        // );
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
            default:
                for (int i = 0; i < instructions.Count; i++) 
                    if (input == instructions[i].label) return i;
                if (variables.ContainsKey(input))
                    return parse_value * variables[input];
                return 0;                
        }
    }
    public Instruction GetInstruction(int index) 
    {
        return instructions[index].Get();   
    }
    public override string ToString()
    {
        string output = "╠╕ Processor Component: " + this.name + "\n"; 
        // string output = "╠╕" + this.name + "\n";
        for(int i = 0; i < instructions.Count; i++) 
        {
            output += "╟┤ │ " + instructions[i].ToString() + "\n";
        }
        output += "╟┤ ║ ptr:  000 " + Plot("Marker", variables[Processor.Pointer], 0, instructions.Count, instructions.Count)  + " " + instructions.Count.ToString("000") + "\n";
        output += "╟┤ ║ res: -100 " + Plot("Marker", variables[Processor.Result], -100, 100, instructions.Count)  + " 100";
        // var lines = variables.Select(kvp => kvp.Key + ": " + kvp.Value.ToString("0.000"));
        // output += string.Join("\n╟┤", lines);
        return output + "\n╟┘";
    }
}