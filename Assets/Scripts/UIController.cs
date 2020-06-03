using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    protected ComponentController component;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float count = 0f;
    float speed = .005f;
    void Update()
    {
        count += speed;
        if (count >= 2 || count <= -1) speed = -speed;
        if (component == null) {
            return;
        }
        GetComponent<RectTransform>().sizeDelta = Vector3.Slerp(new Vector2(200, 200), new Vector2(250, 300), count);
        transform.position = component.transform.position;
        transform.Translate(new Vector3(0,20,0));


            
    }
    public void Set(ComponentController component) 
    {
        this.component = component;
        
        //component.script.ToString();
        switch (component) 
        {
            case ProcessorController processor:
                // int line_number = 0;
                // string script = "";
                // foreach (string line in processor.GetInstructionSet().ToString().Split('\n')) 
                // {
                //     script += Pad(line_number++) + ": " + line + "\n";
                // }
                transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = processor.ToString();
                break;
            default:
                transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = component.name;
                break;
        }
    }
    public void OnValueChanged() 
    {
        switch (component) 
        {
            case ProcessorController processor:
                processor.Init(transform.GetChild(0).GetChild(0).GetComponent<InputField>().text);
                break;
        }
        
    }
    public string Parse(string line) 
    {
        string script_line = "";
        line = line.Substring(5);
        return script_line;
    }
    public string Pad(float value)
    {
        // 000  ||  SET  XXX -999.999       ||
        // YFX  ||  SET  YYY  999.999       ||
        // 002  ||  JIL  XXX  YYY  YFX      ||
        // 002  ||  JIG  XXX  20.000 YFX    ||
        if (value >  999) return " 999.999";
        if (value < -999) return "-999.999";
        string value_string = value % 1 == 0 ? value.ToString("0") : value.ToString("0.000");
        if (value < 10) value_string = "0" + value_string;
        if (value < 100) value_string = "0" + value_string;
        return value_string;
    }
}

// public class UIContext() 
// {

// }