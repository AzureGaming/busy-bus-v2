using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour {
    public delegate void LeftLaneChange(bool isRushed);
    public static LeftLaneChange OnLeftLaneChange;
    public delegate void RightLaneChange(bool isRushed);
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
    public delegate void Drive();
    public static Drive OnDrive;

    public GameObject leftLanePosition;
    public GameObject rightLanePosition;
    public GameObject image;
    public GameObject regularBackground;

    public Animator busStopAnimator;
    public Animator regularAnimator;

    Color regularBackgroundColor;

    bool busStopQueued;

    private void Awake() {
        regularBackgroundColor = regularBackground.GetComponent<Image>().color;
    }

    private void OnEnable() {
        OnLeftLaneChange += GoToLeftLane;
        OnRightLaneChange += GoToRightLane;
        OnInitLeftLane += InitLeft;
        OnInitRightLane += InitRight;
        OnShowBusStop += QueueBusStop;
        OnCheckQueue += CheckIfChangeRequired;
        OnShowRegular += DisplayRegular;
        OnDrive += SetSpeedNormal;
    }

    private void OnDisable() {
        OnLeftLaneChange -= GoToLeftLane;
        OnRightLaneChange -= GoToRightLane;
        OnInitLeftLane -= InitLeft;
        OnInitRightLane -= InitRight;
        OnShowBusStop -= QueueBusStop;
        OnCheckQueue -= CheckIfChangeRequired;
        OnShowRegular -= DisplayRegular;
        OnDrive -= SetSpeedNormal;
    }

    void QueueBusStop() {
        busStopQueued = true;
        //ReduceAnimationSpeed();
    }

    void InitLeft() {
        image.transform.position = leftLanePosition.transform.position;
    }

    void InitRight() {
        image.transform.position = rightLanePosition.transform.position;
    }

    void GoToLeftLane(bool isRushed) {
        StopAllCoroutines();
        if (isRushed) {
            StartCoroutine(GoToPositionRush(leftLanePosition.transform.position));
        } else {
            StartCoroutine(GoToPositionSmooth(leftLanePosition.transform.position));
        }
    }

    void GoToRightLane(bool isRushed) {
        StopAllCoroutines();
        if (isRushed) {
            StartCoroutine(GoToPositionRush(rightLanePosition.transform.position));
        } else {
            StartCoroutine(GoToPositionSmooth(rightLanePosition.transform.position));
        }
    }

    public void CheckIfChangeRequired() {
        if (busStopQueued) {
            busStopQueued = false;
            TriggerBusStop();
            DisplayBusStop();
        }
    }

    void TriggerBusStop() {
        BusStopBackgroundManager.OnQueueBusStop?.Invoke(Bus.currentLane);
    }

    void SetSpeedNormal() {
        busStopQueued = false;
        CityBackground.OnAnimate?.Invoke();
    }

    void SetSpeedZero() {
        busStopQueued = false;
        CityBackground.OnStopAnimation?.Invoke();
    }

    void DisplayRegular() {
        regularBackground.GetComponent<Image>().color = regularBackgroundColor;
        BusStopBackgroundManager.OnUpdateBackground?.Invoke(Bus.currentLane);
    }

    void DisplayBusStop() {
        regularBackground.GetComponent<Image>().color = Color.clear;
        BusStopBackgroundManager.OnUpdateBackground?.Invoke(Bus.currentLane);
    }

    void ReduceAnimationSpeed() {
        busStopAnimator.SetFloat("Multiplier", 0.5f);
        regularAnimator.SetFloat("Multiplier", 0.5f);
    }

    IEnumerator GoToPositionSmooth(Vector3 newPos) {
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
