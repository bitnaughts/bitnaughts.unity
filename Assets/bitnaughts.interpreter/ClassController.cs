using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassController
{
    Dictionary<string, string> fields; // primitive data types, objects
    List<Method> methods; // constructor (initializes fields), getter/setter (modifies fields), main (Processor's start method)

    public ClassController(ComponentController component) {
        fields["test"] = component.GetTypeClass().ToString();
    }

    // public override string ToString() {
    //     return 
    // }

}


public class Method 
{
    public string definition;
}