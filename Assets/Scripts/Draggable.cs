using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
    public RectTransform boundingArea;
    public RectTransform canvas;

    RectTransform rectTransform;
    Vector3 startDragPos;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        boundingArea = GameObject.Find("Bounding Area").GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        StartCoroutine(GoToPointer());
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!IsCoinWithinBounds()) {
            transform.position = startDragPos;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startDragPos = transform.position;
    }

    IEnumerator GoToPointer() {
        for (; ; ) {
            if (IsMouseWithinBounds() && Input.GetMouseButtonDown(1)) {
                startDragPos = transform.position;
                while (Input.GetMouseButton(1)) {
                    transform.position = Input.mousePosition;
                    yield return null;
                }
                transform.position = startDragPos;
            } else {
                yield return null;
            }
        }
    }

    bool IsCoinWithinBounds() {
        float boundStartX = boundingArea.position.x - ( boundingArea.rect.width / 2 );
        float boundStartY = boundingArea.position.y - ( boundingArea.rect.height / 2 );
        float boundEndX = boundingArea.position.x + ( boundingArea.rect.width / 2 );
        float boundEndY = boundingArea.position.y + ( boundingArea.rect.height / 2 );

        if (transform.position.x < boundStartX || transform.position.y < boundStartY || transform.position.x > boundEndX || transform.position.y > boundEndY) {
            return false;
        }
        return true;
    }

    bool IsMouseWithinBounds() {
        float boundStartX = boundingArea.position.x - ( boundingArea.rect.width / 2 );
        float boundStartY = boundingArea.position.y - ( boundingArea.rect.height / 2 );
        float boundEndX = boundingArea.position.x + ( boundingArea.rect.width / 2 );
        float boundEndY = boundingArea.position.y + ( boundingArea.rect.height / 2 );

        if (Input.mousePosition.x < boundStartX || Input.mousePosition.y < boundStartY || Input.mousePosition.x > boundEndX || Input.mousePosition.y > boundEndY) {
            return false;
        }
        return true;
    }
}
