using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollect : MonoBehaviour {
    public RectTransform boundingArea;

    bool isValidRightClick {
        get {
            Vector2 mousePos = Utilities.ConvertMousePosToWorldPoint();
            float mouseX = mousePos.x;
            float mouseY = mousePos.y;
            // Rect transform anchor must be centered for this to work.
            float minBoundX = boundingArea.offsetMin.x;
            float minBoundY = boundingArea.offsetMin.y;
            float maxBoundX = boundingArea.offsetMax.x;
            float maxBoundY = boundingArea.offsetMax.y;
            Vector3 minPt = transform.TransformPoint(minBoundX, minBoundY, 0);
            Vector3 maxPt = transform.TransformPoint(maxBoundX, maxBoundY, 0);
            if (mouseX > minPt.x && mouseY > minPt.y && mouseX < maxPt.x && mouseY < maxPt.y) {
                return true;
            }
            return false;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1) && isValidRightClick) {
            GameManager.isPlayerHoldingCoins = true;
            Draggable.OnRightClickDown?.Invoke();
        }

        if (Input.GetMouseButtonUp(1)) {
            GameManager.isPlayerHoldingCoins = false;
            Draggable.OnRightClickUp?.Invoke();
        }
    }
}
