using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BusEvent : MonoBehaviour {
    public delegate void Complete(EventType eventType, float timeElapsed, float totalTime);
    public static Complete OnComplete;
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

    private void OnEnable() {
        OnComplete += RatePerformance;
    }

    protected void SetupEvent() {
        timeElapsed = 0f;
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

    IEnumerator EventRoutine() {
        yield return new WaitUntil(() => hasResponded);
        StopCoroutine(timeoutRoutine);
        OnEvaluate();
        Evaluate();
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
        // TODO: handle valid payment accept
        // TODO: handle valid payment reject
        // TODO: handle invalid payment accept
        // TODO: handle invalid payment reject
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

    void RatePerformance(EventType eventType, float timeElapsed, float totalTime) {
    }
}