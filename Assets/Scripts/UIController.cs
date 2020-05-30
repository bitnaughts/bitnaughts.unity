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
                transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = processor.GetScript().ToString();
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
                processor.SetScript(transform.GetChild(0).GetChild(0).GetComponent<InputField>().text);
                break;
        }
        
    }
}

// public class UIContext() 
// {

// }