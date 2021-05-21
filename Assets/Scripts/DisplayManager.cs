using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour {
    public delegate void LookForward();
    public static LookForward OnLookForward;
    public delegate void LookBack();
    public static LookBack OnLookBack;

    public GameObject city;
    public GameObject bus;
    public GameObject backOfBus;
    public GameObject fareBox;

    private void OnEnable() {
        OnLookForward += DisplayFrontOfBus;
        OnLookBack += DisplayBackOfBus;
    }

    private void OnDisable() {
        OnLookForward -= DisplayFrontOfBus;
        OnLookBack -= DisplayBackOfBus;
    }

    void DisplayFrontOfBus() {
        city.SetActive(true);
        bus.SetActive(true);
        backOfBus.SetActive(false);
    }

    void DisplayBackOfBus() {
        city.SetActive(true);
        bus.SetActive(false);
        backOfBus.SetActive(true);
    }
}
