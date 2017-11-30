using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    Normal = 0,
    VR
}
public class WebVRUtil : MonoBehaviour
{
    protected Vector3 position;
    protected Quaternion rotation;

    protected virtual void UpdatePose() { }

    protected void RotationX(float x) {
        rotation.x = x;
    }
    protected void RotationY(float y)
    {
        rotation.y = y;
    }
    protected void RotationZ(float z)
    {
        rotation.z = z;
    }
    protected void RotationW(float w)
    {
        rotation.w = w;
    }

    protected void PositionX(float x)
    {
        position.x = x;
    }
    protected void PositionY(float y)
    {
        position.y = y;
    }
    protected void PositionZ(float z)
    {
        position.z = z;
    }
    protected IEnumerator Render()
    {
        yield return new WaitForEndOfFrame();
        Application.ExternalCall("Render");
    }
}
