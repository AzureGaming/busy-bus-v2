using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // TODO: should be singleton
    public delegate void FailDay();
    public static FailDay OnFailDay;
    public delegate void CompleteDay();
    public static CompleteDay OnCompleteDay;
    public delegate void StartDay();
    public static StartDay OnStartDay;

    public BoardingQueue boardingQueue;
    public TimeOfDay timeOfDay;
    public EventQueue eventQueue;

    bool dayOver;

    private void OnEnable() {
        OnFailDay += GameOver;
        OnStartDay += StartTheDay;
        OnCompleteDay += CompleteTheDay;
    }

    private void OnDisable() {
        OnFailDay -= GameOver;
        OnStartDay -= StartTheDay;
        OnCompleteDay -= CompleteTheDay;
    }

    private void Start() {
        StartTheDay();
    }

    public void StartTheDay() {
        boardingQueue.Clear();
        timeOfDay.Init();
        eventQueue.QueueForDay();
    }

    void GameOver() {
        // End the day early, cleanup
        timeOfDay.Kill();
    }

    void CompleteTheDay() {
        dayOver = true;
    }

}
