using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveEvent : BusEvent {
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
    enum Lane {
        Left,
        Right
    }
    Lane currentLane;
    Lane expectedLane;

    private void Awake() {
        type = EventType.Drive;
        currentLane = Lane.Left;
        timeToWait = 5f;
    }

    private void Start() {
        if (currentLane == Lane.Left) {
            Background.OnInitLeftLane?.Invoke();
        } else {
            Background.OnInitRightLane?.Invoke();
        }
    }

    public void BeginEvent() {
        SetupEvent();
        SelectLane();
        DisplayPrompt();
        getPlayerResponse = StartCoroutine(GetPlayerResponse());
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
        ChangeLane(false, true);
    }

    void SelectLane() {
        if (currentLane == Lane.Left) {
            expectedLane = Lane.Right;
            expectedKey = KeyCode.D;
        } else {
            expectedLane = Lane.Left;
            expectedKey = KeyCode.A;
        }
    }

    void ChangeLane(bool skipAnimation = false, bool isRushed = false) {
        if (expectedLane == Lane.Left) {
            Background.OnLeftLaneChange?.Invoke(skipAnimation, isRushed);
            currentLane = Lane.Left;
        } else {
            Background.OnRightLaneChange?.Invoke(skipAnimation, isRushed);
            currentLane = Lane.Right;
        }
    }

    void DisplayPrompt() {
        isPrompted = true;
        if (expectedLane == Lane.Left) {
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
