using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BackOfBus : MonoBehaviour {
    public delegate void Board(GameObject passenger);
    public static Board OnBoard;

    public GameObject testPassenger;
    public Transform seat;

    List<GameObject> passengers;

    private void Awake() {
        passengers = new List<GameObject>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            AddPassenger();
        }
    }

    //private void OnEnable() {
    //    OnBoard += AddPassenger;
    //}

    //private void OnDisable() {
    //    OnBoard -= AddPassenger;
    //}

    void AddPassenger() {
        GameObject test = Instantiate(testPassenger, seat);
        test.GetComponent<Image>().sprite = test.GetComponent<Passenger>().sittingSprite;
        passengers.Add(test);

        Seat[] validSeats = GetComponentsInChildren<Seat>().Where((Seat seat) => !seat.isOccupied).ToArray();

        if (validSeats.Length > 0) {
            int randomSeat = Random.Range(0, validSeats.Length);
            validSeats[randomSeat].SpawnPassenger(test);
        } else {
            Debug.LogWarning("Cannot add new passenger, seats full.");
        }
    }
}
