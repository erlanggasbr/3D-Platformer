using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool isPressed = false;
    public Transform knob, knobDown;
    public Vector3 knobUp;
    // Start is called before the first frame update
    void Start()
    {
        knobUp = knob.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isPressed)
            {
                knob.position = knobDown.position;
                isPressed = true;
            }
            else
            {
                knob.position = knobUp;
                isPressed = false;
            }
        }
    }
}
