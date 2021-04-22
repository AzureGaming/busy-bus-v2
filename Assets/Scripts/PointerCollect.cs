using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollect : MonoBehaviour {
    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Draggable.OnRightClickDown?.Invoke();
        }

        if (Input.GetMouseButtonUp(1)) {
            Draggable.OnRightClickUp?.Invoke();
        }
    }
}
