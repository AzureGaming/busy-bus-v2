using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusStopBackgroundManager : MonoBehaviour {
    public delegate void QueueBusStop(Bus.Lane lane);
    public static QueueBusStop OnQueueBusStop;
    public delegate void UpdateBackground(Bus.Lane lane);
    public static UpdateBackground OnUpdateBackground;

    public BusStopBackground left;
    public BusStopBackground right;

    private void OnEnable() {
        OnQueueBusStop += SetTriggerFlag;
        OnUpdateBackground += SetColor;
    }

    private void OnDisable() {
        OnQueueBusStop -= SetTriggerFlag;
        OnUpdateBackground -= SetColor;
    }

    void SetTriggerFlag(Bus.Lane lane) {
        switch (lane) {
            case Bus.Lane.Left:
                left.shouldTriggerEvent = true;
                break;
            case Bus.Lane.Right:
                right.shouldTriggerEvent = true;
                break;
            default:
                Debug.LogWarning("Cannot set trigger flag in BusStopBackgroundManager for unknown lane.");
                break;
        }
    }

    void SetColor(Bus.Lane lane) {
        switch (lane) {
            case Bus.Lane.Left:
                left.SetOpaque();
                right.SetTransparent();
                break;
            case Bus.Lane.Right:
                right.SetOpaque();
                left.SetTransparent();
                break;
        }
    }
}
