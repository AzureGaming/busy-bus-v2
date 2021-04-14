using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {
    public delegate void BusLaneChange();
    public static BusLaneChange OnBusLaneChange;
    public Sprite tiny;
    public Sprite small;
    public Sprite medium;
    public Sprite large;

    public Transform blockBus;
    public Transform pastBus;
    public Transform mediumPoint;
    public Transform largePoint;

    Image image;
    bool startedEvent = false;
    bool isBusBlocking;
    Vector2 startBlockBusPos;
    Vector2 startPastBusPos;

    private void Awake() {
        image = GetComponent<Image>();

        startBlockBusPos = blockBus.position;
        startPastBusPos = pastBus.position;
    }

    private void Start() {
        Flip();
        image.sprite = tiny;
        isBusBlocking = true;
        StartCoroutine(MoveInFrontOfBus());
    }

    void OnEnable() {
        OnBusLaneChange += MovePastBus;
    }

    private void OnDisable() {
        OnBusLaneChange -= MovePastBus;
    }

    IEnumerator MoveInFrontOfBus() {
        float totalTime = 15f;
        float timeElapsed = 0f;
        Vector2 startPos = transform.position;

        image.sprite = tiny;
        yield return new WaitForSeconds(2f);
        image.sprite = small;
        yield return new WaitForSeconds(2f);

        while (timeElapsed < totalTime) {
            // Prompt the driving event to player
            if (timeElapsed / totalTime >= 0.8f && !startedEvent) {
                startedEvent = true;
                DriveEvent.OnInitEvent?.Invoke();
            }

            //if (transform.localPosition.x <= -17) {
            //    image.sprite = medium;
            //}

            //if (transform.localPosition.x <= -80) {
            //    image.transform.localScale = new Vector2(0.8f, 0.8f);
            //    image.sprite = large;
            //}
            if (transform.position.y <= largePoint.position.y) {
                image.sprite = large;
            } else if (transform.position.y <= mediumPoint.position.y) {
                image.sprite = medium;
            }

            timeElapsed += Time.deltaTime;

            // move toward bus
            transform.position = Hermite(startPos, blockBus.position, timeElapsed / totalTime);
            yield return null;
        }

        // move past bus
        yield return new WaitUntil(() => !isBusBlocking);
        yield return StartCoroutine(MovePastBusRoutine());

        Destroy(gameObject);
    }

    void MovePastBus() {
        isBusBlocking = false;
    }

    void Flip() {
        if (StateManager.currentLane == StateManager.Lane.Left) {
            blockBus.position = startBlockBusPos;
            pastBus.position = startPastBusPos;
        } else {
            Vector2 newBlockBusPos = blockBus.localPosition;
            Vector2 newPastBusPos = pastBus.localPosition;

            newBlockBusPos.x *= -1f;
            newPastBusPos.x *= -1f;

            blockBus.localPosition = newBlockBusPos;
            pastBus.localPosition = newPastBusPos;
            Vector3 newImageScale = image.GetComponent<RectTransform>().localScale;
            newImageScale.x *= -1;
            image.GetComponent<RectTransform>().localScale = newImageScale;
        }
    }

    IEnumerator MovePastBusRoutine() {
        float totalTime = 5f;
        float timeElapsed = 0f;
        Vector2 pos = transform.position;

        while (timeElapsed < totalTime) {
            timeElapsed += Time.deltaTime;
            transform.position = Vector2.Lerp(pos, pastBus.position, timeElapsed / totalTime);
            yield return null;
        }
    }

    float Hermite(float start, float end, float value) {
        return Mathf.Lerp(start, end, value * value * ( 3.0f - 2.0f * value ));
    }

    Vector2 Hermite(Vector2 start, Vector2 end, float value) {
        return new Vector2(Hermite(start.x, end.x, value), Hermite(start.y, end.y, value));
    }
}
