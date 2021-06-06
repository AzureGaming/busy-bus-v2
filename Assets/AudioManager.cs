using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public delegate void DriveBus();
    public static DriveBus OnDriveBus;

    public AudioSource driveBus;

    private void OnEnable() {
        OnDriveBus += PlayDriveBus;
    }

    private void OnDisable() {
        OnDriveBus -= PlayDriveBus;
    }

    void PlayDriveBus() {
        driveBus.Play();
    }
}
