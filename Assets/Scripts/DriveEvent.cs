using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveEvent : BusEvent {
    // TODO: variable to track the lane we're currently in
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

    private void Awake() {
        type = EventType.Drive;
        timeToWait = 3f;
    }

    public void Begin() {
        SetupEvent();
        SelectLane();
        DisplayPrompt();
        getPlayerResponse = StartCoroutine(GetPlayerResponse());
    }

    protected override bool IsResponseCorrect() {
        if (expectedKey == playerResponse) {
            return true;
        }
        return false;
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
        // select the unoccupied lane
        // set the expected key accordingly
        Debug.LogWarning("TODO: Implement");
    }

    void DisplayPrompt() {
        isPrompted = true;
        Debug.LogWarning("TODO: Implement");
    }

    void HidePrompt() {
        isPrompted = false;
        Debug.LogWarning("TODO: Implement");
    }

    IEnumerator GetPlayerResponse() {
        playerResponse = null;
        yield return new WaitUntil(() => {
            playerResponse = Input.inputString;
            return ( playerResponse == null || playerResponse == "" ) ? false : true;
        });
    }
}
