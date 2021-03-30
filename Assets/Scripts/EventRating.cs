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
    public string grade {
        get {
            if (fails >= 5) {
                return grades[0];
            }

            if (greats > goods) {
                return grades[2];
            } else {
                return grades[1];
            }
        }
    }

    int fails = 0; // TODO: reset to 0?
    int goods = 0;
    int greats = 0;

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
        float ratio = timeElapsed / totalTime;
        if (ratio >= 0.75) {
            greats++;
        } else if (ratio >= 0.25) {
            goods++;
        } else {
            fails++;
        }
    }

    public void Fail() {
        fails++;
        GameManager.OnFailDay?.Invoke(fails);
    }
}
