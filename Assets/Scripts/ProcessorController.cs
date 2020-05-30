using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;




public class ProcessorController : ComponentController 
{   

    protected Processor.Script script;


    public Processor.Script GetScript() 
    {
        return script;
    }
    public void SetScript(string script)
    {
        this.script = new Processor.Script(script);
    }

    public void Flash(Processor.Script script) 
    {
        this.script = script;
        this.script = new Processor.Script( // All Variables must be 3 letters long... because it make the code look good...
            new string[] {
                "set gim 5",     //0: Gimbal Id = 5, its the 6th element in Components
                "set thr 6",     //1: Thruster Id = 6, its the 7th element in Components 
                "fun gim 5",     //2: Turn Gimbal 5 degrees
                "jie res 90 6",  //3: If resulting Gimbal angle is equal to 90, go to line 6
                "fun thr res",   //4: Increase/decrease thrust based on resulting angle (arbitrary line)
                "set ptr 2",     //5: Go to line 2 (keep turning gimbal until its equal to 90)
                "fun gim -5",    //6: Turn Gimbal -5 degrees
                "jie res -90 2", //7: If resulting Gimbal angle is equal to -90, go to line 2
                "fun thr res",   //8: Increase/decrease thrust based on resulting angle (arbitrary line)
                "set ptr 6"      //9: Go to line 6 (keep turning gimbal until tis equal to -90)
            }
        );

    }
    public override float Action(float input) 
    {
        return input;
    }
    public void Action (ComponentController[] components)
    {
        if (script == null) Flash(null);

        script.Step(components);

        // print (
        //     "Debugging line " + variables[Processor.Pointer] + ": " + script.lines[Mathf.RoundToInt(variables[Processor.Pointer])] + "\n" +
        //     string.Join(";", variables.Select(kvp => kvp.Key + ": " + kvp.Value))
        // );
    }
}