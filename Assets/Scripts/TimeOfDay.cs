using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour {
    public delegate void ScheduleAction(int hour, Action action);
    /// <summary>
    /// Execute a function based on a provided "hour" 0 indexed (TODO: make this easier to use?) 
    /// </summary>
    public static ScheduleAction OnScheduleAction;

    const float REAL_MINUTES = 7f;
    const float TOTAL_GAME_HOURS = 12; // 06:00 - 18:00
    float timeElapsed;
    float targetTime;
    float gameHoursPerSecond;
    int hour;

    private void OnEnable() {
        OnScheduleAction += Schedule;
    }

    private void OnDisable() {
        OnScheduleAction -= Schedule;
    }

    public void Init() {
        hour = 0;
        timeElapsed = 0f;
        targetTime = 60 * REAL_MINUTES; // convert minutes to seconds
        gameHoursPerSecond = targetTime / TOTAL_GAME_HOURS; // express game hours in real seconds

        StartCoroutine(Timer());
        StartCoroutine(IncrementHour());
    }

    public void Kill() {
        StopAllCoroutines();
    }

    IEnumerator Timer() {
        while (timeElapsed <= targetTime) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator IncrementHour() {
        yield return new WaitForSeconds(gameHoursPerSecond);
    }

    void Schedule(int hour, Action action) {
        StartCoroutine(ExecAt(hour, action));
    }

    IEnumerator ExecAt(int hour, Action action) {
        yield return StartCoroutine(WaitUntilHour(hour));
        action();
    }

    IEnumerator WaitUntilHour(int targetHour) {
        yield return new WaitUntil(() => hour == targetHour);
    }
}
