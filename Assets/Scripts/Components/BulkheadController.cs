using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BulkheadController : ComponentController {   
    
    public override float Action (float input) {
        return input;
    }
}