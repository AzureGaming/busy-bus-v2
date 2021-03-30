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
    string expectedKey;
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
        timeToWait = 2f;
    }

    private void Start() {
        ChangeLane(true); // Set the bus to the correct lane
    }

    public void BeginEvent() {
        SetupEvent();
        SelectLane();
        DisplayPrompt();
        getPlayerResponse = StartCoroutine(GetPlayerResponse());
    }

    protected override bool IsResponseCorrect() {
        return expectedKey == playerResponse;
    }

    protected override void OnEvaluate() {
        HidePrompt();
    }

    protected override void OnTimeout() {
        if (getPlayerResponse != null) {
            StopCoroutine(getPlayerResponse);
        }
        HidePrompt();
    }

    void SelectLane() {
        if (currentLane == Lane.Left) {
            expectedLane = Lane.Right;
        } else {
            expectedLane = Lane.Left;
        }
    }

    void ChangeLane(bool skipAnimation) {
        if (expectedLane == Lane.Left) {
            Background.OnLeftLaneChange?.Invoke(skipAnimation);
            currentLane = Lane.Left;
        } else {
            Background.OnRightLaneChange?.Invoke(skipAnimation);
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
        HidePrompt();
        ChangeLane(false);
    }

    void HidePrompt() {
        isPrompted = false;
        DrivePrompt.OnCompleteLaneChange?.Invoke();
    }
}
