using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour {
    public Sprite boardingSprite;
    public Sprite sittingSprite;
    public Sprite rowdySprite;

    bool isRowdy = false;
    bool isSitting = false;
    bool isBoarding = false;

    public void Board() {
        // Enter fare checking
    }

    public void Leave() {
        // Exit fare checking
    }

    public void Kick() {
        // Exit back of bus
    }

    public void Stay() {
        // Exit fare checking 
        // Enter back of bus
    }
}
