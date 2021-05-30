using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public delegate void RightClickDown();
    public static RightClickDown OnRightClickDown;
    public delegate void RightClickUp();
    public static RightClickUp OnRightClickUp;

    public RectTransform boundingArea;
    public RectTransform canvas;

    const float SPEED = 20f;

    Coroutine dragRoutine;
    Coroutine followRoutine;
    Rigidbody2D rb;
    CircleCollider2D collider;
    Vector2 startPos;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable() {
        OnRightClickDown += GoToMouse;
        OnRightClickUp += ReturnToStartPosition;
    }

    private void OnDisable() {
        OnRightClickDown -= GoToMouse;
        OnRightClickUp -= ReturnToStartPosition;
    }

    private void Start() {
        boundingArea = GameObject.Find("Bounding Area").GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        startPos = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            dragRoutine = StartCoroutine(FollowMouse());
            transform.SetAsLastSibling();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        StopAllCoroutines();
    }

    void GoToMouse() {
        // Bug: when spamming this function, startPos will update
        startPos = transform.position;
        GetComponentInChildren<Image>().raycastTarget = false;
        collider.enabled = false;
        followRoutine = StartCoroutine(FollowMouse());
    }

    void ReturnToStartPosition() {
        // Bug: causes some movement even when coins have not moved
        GetComponentInChildren<Image>().raycastTarget = true;
        StopAllCoroutines();
        StartCoroutine(LerpToPosition(startPos));
        collider.enabled = true;
    }

    IEnumerator FollowMouse() {
        for (; ; ) {
            Vector2 mousePos = Utilities.ConvertMousePosToWorldPoint();
            Vector2 newVelocity = ( mousePos - (Vector2)transform.position ) * SPEED;
            rb.velocity = newVelocity;
            yield return null;
        }
    }

    IEnumerator LerpToPosition(Vector2 endPos) {
        const float totalTime = 0.09f;
        float timeElapsed = 0f;
        Vector2 startPos = transform.position;

        rb.velocity = Vector2.zero;

        while (timeElapsed < totalTime) {
            transform.position = Vector2.Lerp(startPos, endPos, ( timeElapsed / totalTime ));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }
}
