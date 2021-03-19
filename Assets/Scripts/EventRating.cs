using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRating : MonoBehaviour {
    // TODO: should be singleton
    public delegate void RateEvent(BusEvent.EventType type, float timeElapsed, float totalTime);
    public static RateEvent OnRateEvent;
    public delegate void FailEvent();
    public static FailEvent OnFailEvent;

    string[] grades = new string[] { "Needs Improvement", "Satisfactory", "Excellent" };

    private void OnEnable() {
        OnRateEvent += Rate;
        OnFailEvent += Fail;
    }

    private void OnDisable() {
        OnRateEvent -= Rate;
        OnFailEvent -= Fail;
    }

    public void Rate(BusEvent.EventType type, float timeElapsed, float totalTime) {
        switch (type) {
            case BusEvent.EventType.Drive:
                RateDriving(timeElapsed, totalTime);
                break;
            default:
                Debug.LogWarning("Event rating not implemented");
                break;
        }
    }

    void RateDriving(float timeElapsed, float totalTime) {
        Debug.LogWarning("Not implemented");
    }

    public void Fail() {
        Debug.LogWarning("Not implemented");
    }
}
