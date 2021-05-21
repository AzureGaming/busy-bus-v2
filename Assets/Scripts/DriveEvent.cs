using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveEvent : BusEvent {
    public delegate void InitEvent();
    public static InitEvent OnInitEvent;

    public string playerResponse {
        get;
        private set;
    }
    public bool isPrompted {
        get;
        private set;
    }

    KeyCode expectedKey;
    Coroutine getPlayerResponse;
    Bus.Lane expectedLane;

    private void OnEnable() {
        OnInitEvent += BeginEvent;
    }

    private void OnDisable() {
        OnInitEvent -= BeginEvent;
    }

    private void Awake() {
        type = EventType.Drive;
        timeToWait = 5f;
    }

    public void BeginEvent() {
        //SetupBaseEvent();
        //SelectLane();
        //DisplayPrompt();
        //getPlayerResponse = StartCoroutine(GetPlayerResponse());
    }

    protected override bool IsResponseCorrect() {
        return expectedKey.ToString().ToLower() == playerResponse;
    }

    protected override void OnEvaluate() {
        HidePrompt();
    }

    protected override void OnTimeout() {
        if (getPlayerResponse != null) {
            StopCoroutine(getPlayerResponse);
        }
        HidePrompt();
        ChangeLane(true);
    }

    void SelectLane() {
        if (Bus.currentLane == Bus.Lane.Left) {
            expectedLane = Bus.Lane.Right;
            expectedKey = KeyCode.D;
        } else {
            expectedLane = Bus.Lane.Left;
            expectedKey = KeyCode.A;
        }
    }

    void ChangeLane(bool isRushed = false) {
        Bus.OnLaneChange?.Invoke(expectedLane, isRushed);
        Car.OnBusLaneChange?.Invoke();
    }

    void DisplayPrompt() {
        isPrompted = true;
        if (expectedLane == Bus.Lane.Left) {
            DrivePrompt.OnLeftLaneChange?.Invoke();
        } else {
            DrivePrompt.OnRightLaneChange?.Invoke();
        }
    }

    IEnumerator GetPlayerResponse() {
        playerResponse = null;
        yield return new WaitUntil(() => {
            playerResponse = Input.inputString;
            return playerResponse != null && playerResponse != "";
        });
        hasResponded = true;
        HidePrompt();
        ChangeLane();
    }

    void HidePrompt() {
        isPrompted = false;
        DrivePrompt.OnCompleteLaneChange?.Invoke();
    }
}
