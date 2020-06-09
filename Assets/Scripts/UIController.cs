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
        string display = "";
        string gradient = "abcdefghijklmnop";//░▒▓█▓▒░";
        for (int i = 0; i < 10; i++) 
        {
            display += gradient[(int)(count + i) % gradient.Length];
        }
        for (int i = 0; i < 10; i++) 
        {
            display += gradient[(int)(count + i + 1) % gradient.Length];
        }
        for (int i = 0; i < 10; i++) 
        {
            display += gradient[(int)(count + i + 2) % gradient.Length];
        }
        for (int i = 0; i < 10; i++) 
        {
            display += gradient[(int)(count + i + 3) % gradient.Length];
        }
        GameObject.Find("Label").GetComponent<InputField>().text = display;

        count += Time.deltaTime * 10;
        // if (count >= 1 || count <= 0) speed = -speed;
        if (component == null) {
            return;
        }
        GetComponent<RectTransform>().sizeDelta = Vector3.Slerp(new Vector2(300, 300), new Vector2(300, 300), count);
        transform.position = component.transform.position;
        transform.Translate(new Vector3(0,10,0));
        transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = component.ToString();


    }
    public void Set(ComponentController component) 
    {
        this.component = component;
        // transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = component.name;
    }
    public void OnValueChanged() 
    {
        // switch (component) 
        // {
        //     case ProcessorController processor:
        //         processor.Init(transform.GetChild(0).GetChild(0).GetComponent<InputField>().text);
        //         break;
        // }
        
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