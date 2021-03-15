using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // TODO: should be singleton
    public delegate void FailDay();
    public static FailDay OnFailDay;
    public delegate void StartDay();
    public static StartDay OnStartDay;

    public BoardingQueue boardingQueue;
    public TimeOfDay timeOfDay;

    private void OnEnable() {
        OnFailDay += GameOver;
        OnStartDay += StartTheDay;
    }

    private void OnDisable() {
        OnFailDay -= GameOver;
        OnStartDay -= StartTheDay;
    }

    public void StartTheDay() {
        boardingQueue.Clear();
        timeOfDay.Init();
    }

    void GameOver() {
        // End the day early, cleanup
        timeOfDay.Kill();
    }

}
