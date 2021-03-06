using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BusEvent : MonoBehaviour {
    public enum EventType {
        Drive,
        Fare,
        Rowdy
    }

    Coroutine eventRoutine;
    Coroutine timeoutRoutine;
    protected bool hasResponded;
    protected float timeToWait {
        get; set;
    }
    protected EventType type {
        get; set;
    }
    float timeElapsed;

    protected void SetupBaseEvent() {
        timeElapsed = 0f;
        hasResponded = false;
        eventRoutine = StartCoroutine(EventRoutine());
        timeoutRoutine = StartCoroutine(Timeout());
    }

    protected virtual bool IsResponseCorrect() {
        Debug.LogWarning("Not implemented");
        return true;
    }

    protected virtual void OnEvaluate() {
    }

    protected virtual void OnTimeout() {
    }

    protected virtual void EventSpecific() {
    }

    IEnumerator EventRoutine() {
        yield return new WaitUntil(() => hasResponded);
        StopCoroutine(timeoutRoutine);
        OnEvaluate();
        Evaluate();
        EventSpecific();
    }

    IEnumerator Timeout() {
        while (timeElapsed <= timeToWait) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        StopCoroutine(eventRoutine);
        OnTimeout();
        Fail();
    }

    void Evaluate() {
        if (IsResponseCorrect()) {
            Pass();
        } else {
            Fail();
        }
    }


    void Pass() {
        EventRating.OnRateEvent?.Invoke(type, timeElapsed, timeToWait);
    }

    void Fail() {
        EventRating.OnFailEvent?.Invoke();
    }
}
