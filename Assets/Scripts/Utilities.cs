using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {
    // Necessary when canvas render mode is Screen Space - Camera
    // Ref: https://forum.unity.com/threads/mouse-position-for-screen-space-camera.294458/
    public static Vector2 ConvertMousePosToWorldPoint() {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10f;
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }
}
