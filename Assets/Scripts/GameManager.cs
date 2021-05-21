using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // TODO: should be singleton
    public delegate void FailDay(int fails);
    public static FailDay OnFailDay;
    public delegate void CompleteDay();
    public static CompleteDay OnCompleteDay;
    public delegate void StartDay();
    public static StartDay OnStartDay;

    public static Passenger currentPassenger;

    public BoardingQueue boardingQueue;
    public TimeOfDay timeOfDay;
    public EventQueue eventQueue;
    public GameObject adultPassenger;
    public Transform boardingArea;

    public const float CHILD_FARE = 1f;
    public const float ADULT_FARE = 2f;
    public const float SENIOR_FARE = 3f;

    public enum Lane {
        Left,
        Right
    }

    public static bool isPlayerHoldingCoins;
    public static Lane currentLane = Lane.Right;

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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DisplayManager.OnLookBack?.Invoke();
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            DisplayManager.OnLookForward?.Invoke();
        }
    }

    private void Start() {
        StartTheDay();
    }

    public void StartTheDay() {
        boardingQueue.Clear();
        timeOfDay.Init();
        eventQueue.QueueForDay();
        DisplayManager.OnLookForward?.Invoke();
    }

    void GameOver(int fails) {
        // End the day early, cleanup
        if (fails >= 2) {
            timeOfDay.Kill();
            Debug.Log("TODO: Implement Game Over");
        }
    }

    void CompleteTheDay() {
        dayOver = true;
    }
}
