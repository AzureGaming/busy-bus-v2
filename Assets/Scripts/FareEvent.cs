using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FareEvent : BusEvent {
    public delegate void Init();
    public static Init OnInit;
    public delegate void RejectPayment();
    public static RejectPayment OnRejectPayment;

    public GameObject regularPassenger;
    public GameObject fareBox;
    public Transform boardingPassengers;

    bool? playerDecision;
    bool isCorrectResponse;

    Coroutine getPlayerResponse;

    private void Awake() {
        type = EventType.Fare;
        timeToWait = 10f;
    }

    private void OnEnable() {
        OnInit += Begin;
        OnRejectPayment += Reject;
    }

    private void OnDisable() {
        OnInit -= Begin;
        OnRejectPayment -= Reject;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Begin();
        }
    }

    void Begin() {
        StartCoroutine(BeginRoutine());
    }

    public void Accept() {
        // Must be called to progress event
        playerDecision = true;
    }

    public void Reject() {
        // Must be called to progress event
        playerDecision = false;
    }

    protected override bool IsResponseCorrect() {
        if (playerDecision == null) {
            Debug.LogWarning("Player response checked before it was set. Returning false.");
            return false;
        }

        if (GameManager.currentPassenger.paidFare >= GameManager.ADULT_FARE) { // if payment valid
            isCorrectResponse = true;
            return (bool)playerDecision;
        } else { // if payment invalid
            isCorrectResponse = false;
            return (bool)!playerDecision;
        }
    }

    protected override void OnEvaluate() {
        if (isCorrectResponse && (bool)playerDecision) {
            // if valid payment accept
            // Passenger moves to back of bus
            GameManager.currentPassenger.Stay();
        } else if (isCorrectResponse && (bool)!playerDecision) {
            // if valid payment reject
            // Passenger gets off bus
            GameManager.currentPassenger.Leave();
        } else if (!isCorrectResponse && (bool)playerDecision) {
            // if invalid payment accept
            // Passenger moves to back of bus
            GameManager.currentPassenger.Stay();
        } else {
            // if invalid payment reject
            // Passenger gets off bus  
            GameManager.currentPassenger.Leave();
        }
    }

    protected override void OnTimeout() {
        if (getPlayerResponse != null) {
            StopCoroutine(getPlayerResponse);
        }
        // Passenger gets off bus?
        // Resume background animation
    }

    IEnumerator GetPlayerResponse() {
        playerDecision = default;
        yield return new WaitUntil(() => { return playerDecision != null; });
        hasResponded = true;
    }

    IEnumerator BeginRoutine() {
        yield return StartCoroutine(SetupPreEvent());
        SetupBaseEvent();
        SetupEvent();
        getPlayerResponse = StartCoroutine(GetPlayerResponse());
    }

    IEnumerator SetupPreEvent() {
        //TODO: set up background animation (bus stop) to match with the event
        Debug.LogWarning("TODO: Implement SetupPreEvent");
        yield break;
    }

    void SetupEvent() {
        GameManager.OnBoardPassenger?.Invoke();
        fareBox.SetActive(true);
    }
}
