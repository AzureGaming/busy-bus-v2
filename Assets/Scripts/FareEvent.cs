using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FareEvent : BusEvent {
    public delegate void Init();
    public static Init OnInit;
    public delegate void RejectPayment();
    public static RejectPayment OnRejectPayment;
    public delegate void AcceptPayment();
    public static AcceptPayment OnAcceptPayment;

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
        OnAcceptPayment += Accept;
    }

    private void OnDisable() {
        OnInit -= Begin;
        OnRejectPayment -= Reject;
        OnAcceptPayment -= Accept;
    }

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) {
        //    Begin();
        //}
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
            isCorrectResponse = (bool)playerDecision;
            return (bool)playerDecision;
        } else { // if payment invalid
            isCorrectResponse = (bool)!playerDecision;
            return (bool)!playerDecision;
        }
    }

    protected override void EventSpecific() {
        if (isCorrectResponse && (bool)playerDecision) {
            // if valid payment accept
            // Passenger moves to back of bus
            FareBox.OnRemoveFare?.Invoke();
            fareBox.SetActive(false);
            GameManager.currentPassenger.Stay();
            Background.OnResumeDriving?.Invoke();
        } else if (isCorrectResponse && (bool)!playerDecision) {
            // if valid payment reject
            // Passenger gets off bus
            FareBox.OnRemoveFare?.Invoke();
            fareBox.SetActive(false);
            GameManager.currentPassenger.Leave();
            Background.OnResumeDriving?.Invoke();
        } else if (!isCorrectResponse && (bool)playerDecision) {
            // if invalid payment accept
            // Passenger moves to back of bus
            FareBox.OnRemoveFare?.Invoke();
            fareBox.SetActive(false);
            GameManager.currentPassenger.Stay();
            Background.OnResumeDriving?.Invoke();
        } else {
            // if invalid payment reject
            // Passenger gets off bus  
            FareBox.OnRemoveFare?.Invoke();
            fareBox.SetActive(false);
            GameManager.currentPassenger.Leave();
            Background.OnResumeDriving?.Invoke();
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
        fareBox.SetActive(true);
        BoardingQueue.SpawnPassengers?.Invoke();
    }
}
