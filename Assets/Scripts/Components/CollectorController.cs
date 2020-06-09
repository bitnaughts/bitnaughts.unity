using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class CollectorController : ComponentController {   

    public override float Action (float input) {
        // connected_components[0].Heat(.001f);
        return input;
    }
}