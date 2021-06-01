using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public ParticleSystem particleSystem;

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
        Utilities.ShowUI(city);
        Utilities.ShowUI(bus);
        Utilities.HideUI(backOfBus);
        Passenger.OnDisplayFrontOfBus?.Invoke();
    }

    void DisplayBackOfBus() {
        Utilities.ShowUI(city);
        Utilities.HideUI(bus);
        Utilities.ShowUI(backOfBus);
        Passenger.OnDisplayBackOfBus?.Invoke();
    }

    void DisplayPassengerMenu() {
        Debug.LogWarning("TODO: IMPLEMENT PASSENGER MENU");
    }
}
