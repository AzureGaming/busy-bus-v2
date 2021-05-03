using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackOfBus : MonoBehaviour {
    public delegate void MoveToBack(GameObject passenger);
    public static MoveToBack OnMoveToBack;

    List<GameObject> passengers;

    private void Awake() {
        passengers = new List<GameObject>();
    }

    private void OnEnable() {
        OnMoveToBack += AddPassenger;
    }

    private void OnDisable() {
        OnMoveToBack -= AddPassenger;
    }

    void AddPassenger(GameObject passenger) {
        passengers.Add(passenger);
        passenger.transform.SetParent(transform, false); // placeholder
    }
}
