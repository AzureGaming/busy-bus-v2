using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusStopBackground : CityBackground {
    public Animator regularAnimator;
    public Animator busStopAnimator;

    public bool shouldTriggerEvent;

    bool returnToNormal = false;
    Image image;

    protected override void Awake() {
        base.Awake();
        image = GetComponent<Image>();
    }

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
    public override void CheckIfBackgroundShouldChange() {
        if (returnToNormal) {
            City.OnShowRegular?.Invoke();
            returnToNormal = false;
        }
    }

    public void SetOpaque() {
        Color newColor = image.color;
        newColor.a = 1f;
        image.color = newColor; 
    }

    public void SetTransparent() {
        Color newColor = image.color;
        newColor.a = 0f;
        image.color = newColor;
    }
}
