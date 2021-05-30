using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour {
    public delegate void LookForward();
    public static LookForward OnLookForward;
    public delegate void LookBack();
    public static LookBack OnLookBack;
    public delegate void PassengerClick();
    public static PassengerClick OnPassengerClick;

    public GameObject city;
    public GameObject bus;
    public GameObject backOfBus;
    public GameObject fareBox;

    private void OnEnable() {
        OnLookForward += DisplayFrontOfBus;
        OnLookBack += DisplayBackOfBus;
        OnPassengerClick += DisplayPassengerMenu;
    }

    private void OnDisable() {
        OnLookForward -= DisplayFrontOfBus;
        OnLookBack -= DisplayBackOfBus;
        OnPassengerClick -= DisplayPassengerMenu;
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

    void DisplayPassengerMenu() {
        Debug.LogWarning("TODO: IMPLEMENT PASSENGER MENU");
    }
}
