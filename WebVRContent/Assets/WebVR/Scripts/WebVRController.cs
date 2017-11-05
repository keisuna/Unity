using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebVRController: WebVRUtil
{
    private WebVRCamera cam;
    protected Vector2 axis;
    protected float triggerValue;

    protected void Undefined() {
        this.GetComponent<MeshRenderer>().enabled = false;
    }
    protected void DisplayController()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
    }
    protected void AxisX(float x) {
        axis.x = x;
    }
    protected void AxisY(float y) {
        axis.y = y;
    }
    protected void ButtonValue(float value) {
        triggerValue = value;
    }
    protected virtual void ButtonEnter(int index)
    {
        Debug.Log("Button " + index.ToString() + " pushed");
    }
    protected virtual void ButtonExit(int index)
    {
        Debug.Log("Button " + index.ToString() + " released");
    }
    protected virtual void ButtonStay(int index) { }
    
    protected override void UpdatePose()
    {
        if(cam == null) cam = GameObject.Find("CameraSet").GetComponent<MainCamera>();
        this.transform.localPosition = position + cam.gameObject.transform.localPosition - cam.cameraPosition;
        this.transform.rotation = rotation;
    }
}
