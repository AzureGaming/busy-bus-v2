using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusStopBackground : MonoBehaviour {
    public Animator regularAnimator;
    public Animator busStopAnimator;

    public bool shouldTriggerEvent;

    bool returnToNormal = false;

    public void OnFrameBesideBusStop() {
        if (shouldTriggerEvent) {
            GetComponent<Animator>().speed = 0f;
            busStopAnimator.speed = 0f;
            regularAnimator.speed = 0f;
            shouldTriggerEvent = false;
            returnToNormal = true;
            FareEvent.OnInit?.Invoke();
        }
    }
    public void CheckIfBackgroundShouldChange() {
        if (returnToNormal) {
            Background.OnShowRegular?.Invoke();
            returnToNormal = false;
        }
    }
}
