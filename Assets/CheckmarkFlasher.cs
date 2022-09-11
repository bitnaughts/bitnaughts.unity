using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckmarkFlasher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float timer = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.enabled) {
            timer += Time.deltaTime;
            GetComponent<Image>().color = new Color(1f, 1f, 0, (timer * 2) % 1);
        }
    }
}
