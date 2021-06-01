using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingQueue : MonoBehaviour {
    // TODO: should be a singleton
    public delegate void SetupFareEvent();
    public static SetupFareEvent SpawnPassengers;

    public GameObject adultPrefab;
    public GameObject seniorPrefab;
    public GameObject childPrefab;

    public List<GameObject> commuters {
        get;
        private set;
    }

    private void Awake() {
        commuters = new List<GameObject>();
    }

    private void OnEnable() {
        SpawnPassengers += LoadBoardingPassengers;
    }

    public void Clear() {
        commuters.Clear();
    }

    void LoadBoardingPassengers() {
        Queue();
    }

    void Queue() {
        //int randomPassenger = Random.Range(0, 3);
        //GameObject randomPrefab;
        //switch (randomPassenger) {
        //    case 0: {
        //        randomPrefab = adultPrefab;
        //        break;
        //    }
        //    case 1: {
        //        randomPrefab = seniorPrefab;
        //        break;
        //    }
        //    case 2: {
        //        randomPrefab = childPrefab;
        //        break;
        //    }
        //    default: {
        //        randomPrefab = adultPrefab;
        //        break;
        //    }
        //}
        //GameObject passengerInstantiation = Instantiate(randomPrefab);
        GameObject passengerInstantiation = Instantiate(adultPrefab, transform);
        // hack to update the view without going through DisplayManager
        if (Bus.isLookingBack) {
            Utilities.HideUI(passengerInstantiation);
        }

        commuters.Add(passengerInstantiation);
        passengerInstantiation.GetComponent<Passenger>().Board();
        Bus.currentPassenger = passengerInstantiation.GetComponent<Passenger>();
    }

    void Queue(int amount) {
        for (int i = 0; i < amount; i++) {
            Queue();
        }
    }

    GameObject Dequeue() {
        if (commuters.Count < 1) {
            Debug.LogWarning("Queue is empty!");
            return null;
        }

        int lastIndex = commuters.Count - 1;
        GameObject passenger = commuters[lastIndex];

        commuters.RemoveAt(lastIndex);
        return passenger;
    }
}
