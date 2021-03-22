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


    private void Start() {
        SetVisibility();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F12)) {
            isEnabled = !isEnabled;
            SetVisibility();
        }
        hour.text = "<color=\"green\">hour: " + FindObjectOfType<TimeOfDay>().hour + "</color>";
        timeElapsed.text = "<color=\"green\">time elapsed: " + FindObjectOfType<TimeOfDay>().timeElapsed + "</color>";
    }

    void SetVisibility() {
        debugWindow.SetActive(isEnabled);
    }
}
