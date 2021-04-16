using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusStopBackground : MonoBehaviour {
    public bool isValid;
    public Animator regularAnimator;
    bool returnToNormal = false;
    public void StopAnimating() {
        if (isValid) {
            GetComponent<Animator>().speed = 0;
            regularAnimator.speed = 0;
            isValid = false;
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
