using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebVRCamera : WebVRUtil {
    
    private GameObject cameraM;
    private GameObject cameraR;
    private GameObject cameraL;

    protected Vector3 newAngle;
    protected Vector3 lastMousePosition;

    public Mode mode;
    
    protected bool Freeze;
    private float heightoffset;
    private Vector3 HeightOffset { get { return Vector3.up * heightoffset; } }
    public Vector3 cameraPosition { get { return position; } }
    
    protected void ChangeMode(int index)
    {
        int cameramode = 2;
        if (index == 0) cameramode = 0;
        else if (index == 1) cameramode = 1;
        Debug.Log("Change Mode");
        switch (cameramode)
        {
            case 0:
                cameraM.GetComponent<Camera>().enabled = true;
                cameraL.GetComponent<Camera>().enabled = false;
                cameraR.GetComponent<Camera>().enabled = false;
                mode = Mode.Normal;
                break;
            case 1:
                cameraM.GetComponent<Camera>().enabled = false;
                cameraL.GetComponent<Camera>().enabled = true;
                cameraR.GetComponent<Camera>().enabled = true;
                mode = Mode.VR;
                break;
            default:
                Debug.Log("Undifined commannd. Please enter 0: Normal mode, 1: VR mode");
                break;
        }
    }

    protected void FreezePosition()
    {
        Freeze = true;
    }

    protected void CameraInit()
    {
        heightoffset = 1f;
        mode = new Mode();
        Application.ExternalCall("getVRDevices");
        cameraM = GameObject.Find("NormalCamera");
        cameraR = GameObject.Find("RightCamera");
        cameraL = GameObject.Find("LeftCamera");
    }
    protected override void UpdatePose()
    {
        if (mode == Mode.Normal)
        {
            if (Input.GetMouseButtonDown(0))
            {
                newAngle = this.transform.localEulerAngles;
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * 0.1f;
                newAngle.x += (Input.mousePosition.y - lastMousePosition.y) * 0.1f;
                newAngle.z = 0;
                this.gameObject.transform.localEulerAngles = newAngle;
                lastMousePosition = Input.mousePosition;
            }
        }
        else if (mode == Mode.VR)
        {
            if (Freeze) this.transform.localPosition = Vector3.up * position.y + HeightOffset;
            else this.transform.localPosition = position + HeightOffset;
            this.transform.localRotation = rotation;
            StartCoroutine(Render());

            if (this.transform.localPosition.y < 0) heightoffset += this.transform.localPosition.y;
        }
    }
}
