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
        Show(city);
        Show(bus);
        Hide(backOfBus);
    }

    void DisplayBackOfBus() {
        Show(city);
        Hide(bus);
        Show(backOfBus);
    }

    void DisplayPassengerMenu() {
        Debug.LogWarning("TODO: IMPLEMENT PASSENGER MENU");
    }

    // Ref: https://answers.unity.com/questions/840927/how-do-i-disable-a-renderer-on-a-ui-object-in-46.html 
    void Show(GameObject obj) {
        UIBehaviour[] renderers = obj.GetComponentsInChildren<UIBehaviour>();
        foreach (UIBehaviour renderer in renderers) {
            renderer.enabled = true;
        }
    }

    // Ref: https://answers.unity.com/questions/840927/how-do-i-disable-a-renderer-on-a-ui-object-in-46.html 
    void Hide(GameObject obj) {
        UIBehaviour[] renderers = obj.GetComponentsInChildren<UIBehaviour>();
        foreach (UIBehaviour renderer in renderers) {
            renderer.enabled = false;
        }
    }
}
