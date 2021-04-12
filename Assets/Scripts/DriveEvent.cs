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
    StateManager.Lane expectedLane;

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

    private void Start() {
        // Move to somewhere else?
        if (StateManager.currentLane == StateManager.Lane.Left) {
            Background.OnInitLeftLane?.Invoke();
        } else {
            Background.OnInitRightLane?.Invoke();
        }
    }

    public void BeginEvent() {
        SetupBaseEvent();
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
        if (StateManager.currentLane == StateManager.Lane.Left) {
            expectedLane = StateManager.Lane.Right;
            expectedKey = KeyCode.D;
        } else {
            expectedLane = StateManager.Lane.Left;
            expectedKey = KeyCode.A;
        }
    }

    void ChangeLane(bool skipAnimation = false, bool isRushed = false) {
        if (expectedLane == StateManager.Lane.Left) {
            Background.OnLeftLaneChange?.Invoke(skipAnimation, isRushed);
            StateManager.currentLane = StateManager.Lane.Left;
        } else {
            Background.OnRightLaneChange?.Invoke(skipAnimation, isRushed);
            StateManager.currentLane = StateManager.Lane.Right;
        }
        Car.OnBusLaneChange?.Invoke();
    }

    void DisplayPrompt() {
        isPrompted = true;
        if (expectedLane == StateManager.Lane.Left) {
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
