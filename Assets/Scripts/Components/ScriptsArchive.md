new Processor.Script( 
    new string[] {
        "set gim 5",     //0
        "set thr 6",     //1
        "fun gim 5",     //2
        "jie res 90 6",  //3
        "fun thr res",   //4
        "set ptr 2",     //5
        "fun gim -5",    //6
        "jie res -90 2", //7
        "fun thr res",   //8
        "set ptr 6",     //9
        "set ptr 0"      // Base loop
    }
);