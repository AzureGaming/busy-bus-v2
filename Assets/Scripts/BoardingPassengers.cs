using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingPassengers : MonoBehaviour {
    public delegate void Board();
    public static Board OnBoard;

    public GameObject regularPassenger;
    public static Passenger currentPassenger;

    private void OnEnable() {
        OnBoard += LoadPassenger;
    }

    private void OnDisable() {
        OnBoard -= LoadPassenger;
    }

    void LoadPassenger() {
        // TODO: should handle a queue of passengers
        currentPassenger = Instantiate(regularPassenger, transform).GetComponent<Passenger>();

        currentPassenger.Board();
    }
}
