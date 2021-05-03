using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {
    public delegate void LeftLaneChange(bool skipAnimation, bool isRushed);
    public static LeftLaneChange OnLeftLaneChange;
    public delegate void RightLaneChange(bool skipAnimation, bool isRushed);
    public static RightLaneChange OnRightLaneChange;
    public delegate void InitLeftLane();
    public static InitLeftLane OnInitLeftLane;
    public delegate void InitRightLane();
    public static InitRightLane OnInitRightLane;
    public delegate void ShowBusStop();
    public static ShowBusStop OnShowBusStop;
    public delegate void ShowRegular();
    public static ShowRegular OnShowRegular;
    public delegate void CheckQueue();
    public static CheckQueue OnCheckQueue;
    public delegate void ResumeDriving();
    public static ResumeDriving OnResumeDriving;

    public GameObject leftLanePosition;
    public GameObject rightLanePosition;
    public GameObject image;
    public GameObject regularBackground;
    public GameObject busStopBackground;

    public Animator busStopAnimator;
    public Animator regularAnimator;

    Color regularBackgroundColor;
    Color busStopBackgroundColor;

    bool busStopQueued;

    private void Awake() {
        regularBackgroundColor = regularBackground.GetComponent<Image>().color;
        busStopBackgroundColor = busStopBackground.GetComponent<Image>().color;
    }

    private void OnEnable() {
        OnLeftLaneChange += GoToLeftLane;
        OnRightLaneChange += GoToRightLane;
        OnInitLeftLane += InitLeft;
        OnInitRightLane += InitRight;
        OnShowBusStop += QueueBusStop;
        OnCheckQueue += CheckIfChangeRequired;
        OnShowRegular += DisplayRegular;
        OnResumeDriving += Resume;
    }

    private void OnDisable() {
        OnLeftLaneChange -= GoToLeftLane;
        OnRightLaneChange -= GoToRightLane;
        OnInitLeftLane -= InitLeft;
        OnInitRightLane -= InitRight;
        OnShowBusStop -= QueueBusStop;
        OnCheckQueue -= CheckIfChangeRequired;
        OnShowRegular -= DisplayRegular;
        OnResumeDriving -= Resume;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            QueueBusStop();
        }
    }

    void QueueBusStop() {
        busStopQueued = true;
        //ReduceAnimationSpeed();
    }

    public void InitLeft() {
        image.transform.position = leftLanePosition.transform.position;
    }

    public void InitRight() {
        image.transform.position = rightLanePosition.transform.position;
    }

    public void GoToLeftLane(bool skipAnimation, bool isRushed) {
        StopAllCoroutines();
        if (isRushed) {
            StartCoroutine(GoToPositionRush(leftLanePosition.transform.position));
        } else {
            StartCoroutine(GoToPositionSmooth(leftLanePosition.transform.position, skipAnimation));
        }
    }

    public void GoToRightLane(bool skipAnimation, bool isRushed) {
        StopAllCoroutines();
        if (isRushed) {
            StartCoroutine(GoToPositionRush(rightLanePosition.transform.position));
        } else {
            StartCoroutine(GoToPositionSmooth(rightLanePosition.transform.position, skipAnimation));
        }
    }

    public void CheckIfChangeRequired() {
        if (busStopQueued) {
            busStopQueued = false;
            GetComponentInChildren<BusStopBackground>().shouldTriggerEvent = true;
            DisplayBusStop();
            //StartCoroutine(Resume());
        }
    }

    void Resume() {
        busStopQueued = false;
        GetComponentInChildren<BusStopBackground>().GetComponent<Animator>().speed = 1f;
        GetComponentInChildren<RegularBackground>().GetComponent<Animator>().speed = 1f;
    }

    void DisplayRegular() {
        regularBackground.GetComponent<Image>().color = regularBackgroundColor;
        busStopBackground.GetComponent<Image>().color = Color.clear;
    }

    void DisplayBusStop() {
        regularBackground.GetComponent<Image>().color = Color.clear;
        busStopBackground.GetComponent<Image>().color = busStopBackgroundColor;
    }

    void ReduceAnimationSpeed() {
        busStopAnimator.SetFloat("Multiplier", 0.5f);
        regularAnimator.SetFloat("Multiplier", 0.5f);
    }

    IEnumerator GoToPositionSmooth(Vector3 newPos, bool skipAnimation) {
        float timeElapsed = 0f;
        float totalTime = 1f;
        Vector3 pos = image.transform.position;

        if (!skipAnimation) {
            while (timeElapsed <= totalTime) {
                Vector3 nextPos = image.transform.position;
                float nextPosX = Mathf.Lerp(pos.x, newPos.x, ( timeElapsed / totalTime ));
                nextPos.x = nextPosX;
                image.transform.position = nextPos;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        image.transform.position = newPos;
    }

    IEnumerator GoToPositionRush(Vector3 newPos) {
        Debug.Log("TODO: Implement GoToPositionRush");
        float timeElapsed = 0f;
        float totalTime = 1f;
        Vector3 pos = image.transform.position;

        while (timeElapsed <= totalTime) {
            Vector3 nextPos = image.transform.position;
            float nextPosX = Mathf.Lerp(pos.x, newPos.x, ( timeElapsed / totalTime ));
            nextPos.x = nextPosX;
            image.transform.position = nextPos;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        image.transform.position = newPos;
    }
}
