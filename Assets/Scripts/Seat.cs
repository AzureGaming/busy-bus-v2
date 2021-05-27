using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour {
    public bool isOccupied;

    public void SetParent(GameObject passenger) {
        passenger.transform.SetParent(transform, false);
        isOccupied = true;
    }
}
