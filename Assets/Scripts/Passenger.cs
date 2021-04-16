using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour {
    public Sprite boardingSprite;
    public Sprite sittingSprite;
    public Sprite rowdySprite;

    public float fare;

    bool isRowdy = false;
    bool isSitting = false;
    bool isBoarding = false;

    public void Board() {
        // Enter fare checking
        fare = 5f;
    }

    public void Leave() {
        Debug.Log("Passenger Leave");
        // Exit fare checking
    }

    public void Kick() {
        // Exit back of bus
    }

    public void Stay() {
        Debug.Log("Passenger Stay");
        // Exit fare checking 
        // Enter back of bus
    }
}
