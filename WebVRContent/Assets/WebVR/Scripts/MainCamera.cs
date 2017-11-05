using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : WebVRCamera {
    
    void Start () {
        CameraInit();
	}
    
    void Update()
    {
        UpdatePose();
    }
}
