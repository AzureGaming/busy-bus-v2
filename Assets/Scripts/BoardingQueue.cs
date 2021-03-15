using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingQueue : MonoBehaviour {
    // TODO: should be a singleton
    public GameObject passengerPrefab;
    public List<GameObject> commuters {
        get;
        private set;
    }

    private void Awake() {
        commuters = new List<GameObject>();
    }

    public void Queue() {
        GameObject passenger = Instantiate(passengerPrefab);
        commuters.Add(passenger);
    }

    public void Queue(int amount) {
        for (int i = 0; i < amount; i++) {
            Queue();
        }
    }

    public GameObject Dequeue() {
        if (commuters.Count < 1) {
            Debug.LogWarning("Queue is empty!");
            return null;
        }

        int lastIndex = commuters.Count - 1;
        GameObject passenger = commuters[lastIndex];

        commuters.RemoveAt(lastIndex);
        return passenger;
    }

    public void Clear() {
        commuters.Clear();
    }
}
