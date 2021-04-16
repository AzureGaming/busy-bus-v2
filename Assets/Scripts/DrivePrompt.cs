using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrivePrompt : MonoBehaviour {
    public delegate void LeftLaneChange();
    public static LeftLaneChange OnLeftLaneChange;
    public delegate void RightLaneChange();
    public static RightLaneChange OnRightLaneChange;
    public delegate void CompleteLaneChange();
    public static CompleteLaneChange OnCompleteLaneChange;

    Image image;
    Color startColor;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Start() {
        startColor = image.color;
        image.color = Color.clear;
    }

    private void OnEnable() {
        OnLeftLaneChange += ShowLeftPrompt;
        OnRightLaneChange += ShowRightPrompt;
        OnCompleteLaneChange += HidePrompt;
    }

    private void OnDisable() {
        OnLeftLaneChange -= ShowLeftPrompt;
        OnRightLaneChange -= ShowRightPrompt;
        OnCompleteLaneChange -= HidePrompt;
    }

    void ShowLeftPrompt() {
        image.color = startColor;
        Quaternion newRot = image.transform.rotation;
        newRot.y = 0;
        image.transform.rotation = newRot;
    }

    void ShowRightPrompt() {
        image.color = startColor;
        Quaternion newRot = image.transform.rotation;
        newRot.y = 180;
        image.transform.rotation = newRot;
    }

    void HidePrompt() {
        image.color = Color.clear;
    }
}
