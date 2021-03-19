using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FareEvent : BusEvent {
    float expectedFare;
    float paidFare;
    bool? playerDecision;

    Coroutine getPlayerResponse;

    public void Begin() {
        StartCoroutine(BeginRoutine());
    }

    protected override bool IsResponseCorrect() {
        if (playerDecision == null) {
            Debug.LogWarning("Player response checked before it was set. Returning false.");
            return false;
        }

        if (paidFare >= expectedFare) { // if payment valid
            return (bool)playerDecision;
        } else { // if payment invalid
            return (bool)!playerDecision;
        }
    }

    protected override void OnEvaluate() {
        // if valid payment accept
        // Passenger moves to back of bus
        // if valid payment reject
        // Passenger gets off bus
        // if invalid payment accept
        // Passenger moves to back of bus
        // if invalid payment reject
        // Passenger gets off bus  
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
        yield return new WaitUntil(() => playerDecision != null);
    }

    IEnumerator BeginRoutine() {
        yield return StartCoroutine(SetupPreEvent());
        SetupEvent();
        getPlayerResponse = StartCoroutine(GetPlayerResponse());

    }

    IEnumerator SetupPreEvent() {
        //TODO: set up background animation (bus stop) to match with the event
        Debug.LogWarning("TODO: Implement");
        yield break;
    }
}
