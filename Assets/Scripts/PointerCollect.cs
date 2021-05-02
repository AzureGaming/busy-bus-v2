using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollect : MonoBehaviour {
    public RectTransform boundingArea;

    bool isValidRightClick {
        get {
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            // Rect transform anchor must be centered for this to work.
            float minBoundX = boundingArea.offsetMin.x;
            float minBoundY = boundingArea.offsetMin.y;
            float maxBoundX = boundingArea.offsetMax.x;
            float maxBoundY = boundingArea.offsetMax.y;
            Vector3 minPt = transform.TransformPoint(minBoundX, minBoundY, 0);
            Vector3 maxPt = transform.TransformPoint(maxBoundX, maxBoundY, 0);
            if (mouseX > minPt.x && mouseY > minPt.y && mouseX < maxPt.x && mouseY < maxPt.y) {
                Debug.Log("Vlaid right click");
                return true;
            }
            return false;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1) && isValidRightClick) {
            Draggable.OnRightClickDown?.Invoke();
        }

        if (Input.GetMouseButtonUp(1)) {
            Draggable.OnRightClickUp?.Invoke();
        }
    }
}
