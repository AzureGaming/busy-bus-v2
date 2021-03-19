using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveEvent : BusEvent {
    private void Awake() {
        type = EventType.Drive;
        timeToWait = 3f;
    }

}
