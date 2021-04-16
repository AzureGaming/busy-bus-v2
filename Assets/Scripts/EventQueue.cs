using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour {
    // Track in debug window?
    public DriveEvent driveEvent;
    public GameObject carPrefab;
    public Transform carSpawnPoint;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SpawnCar();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SpawnBusStop();
        }
    }

    public void QueueForDay() {
        for (int i = 0; i < 12; i++) {
            //TimeOfDay.OnScheduleAction(i, () => { DriveEvent(); });
        }
    }

    void SpawnCar() {
        Instantiate(carPrefab, carSpawnPoint);
    }

    void SpawnBusStop() {

    }
}
