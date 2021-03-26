using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugMode : MonoBehaviour {
    [SerializeField]
    bool isEnabled = false;

    public GameObject debugWindow;
    public TMP_Text hour;
    public TMP_Text timeElapsed;
    public TMP_Text drivingResponse;
    public TMP_Text gameHoursPerSecond;

    private void Start() {
        SetVisibility();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F12)) {
            isEnabled = !isEnabled;
            SetVisibility();
        }
        TrackHour();
        TrackTimeElapsed();
        TrackDrivingResponse();
        TrackGameHoursPerSecond();
    }

    void SetVisibility() {
        debugWindow.SetActive(isEnabled);
    }

    void TrackHour() {
        hour.text = "<color=\"green\">hour: " + FindObjectOfType<TimeOfDay>().hour + "</color>";
    }

    void TrackTimeElapsed() {
        timeElapsed.text = "<color=\"green\">time elapsed: " + FindObjectOfType<TimeOfDay>().timeElapsed + "</color>";
    }

    void TrackDrivingResponse() {
        if (FindObjectOfType<DriveEvent>().playerResponse == null || FindObjectOfType<DriveEvent>().playerResponse == "") {
            drivingResponse.text = "<color=\"green\">drive response: waiting...</color>";
        } else {
            drivingResponse.text = "<color=\"green\">drive response: " + FindObjectOfType<DriveEvent>().playerResponse + "</color>";
        }
    }

    void TrackGameHoursPerSecond() {
        gameHoursPerSecond.text = "<color=\"green\">game hours per second: " + FindObjectOfType<TimeOfDay>().gameHoursPerSecond + "</color>";
    }
}
