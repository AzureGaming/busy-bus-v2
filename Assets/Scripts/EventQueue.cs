using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour {
    public DriveEvent driveEvent;
    public void QueueForDay() {
        for (int i = 0; i < 12; i++) {
            TimeOfDay.OnScheduleAction(i, () => { DriveEvent(); });
        }
    }

    void DriveEvent() {
        driveEvent.Begin();
    }
}
