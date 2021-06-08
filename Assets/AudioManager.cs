using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public delegate void DriveBus();
    public static DriveBus OnDriveBus;
    public delegate void StopBus();
    public static StopBus OnStopBus;
    public delegate void OpenDoor();
    public static OpenDoor OnOpenDoor;
    public delegate void DropMoney();
    public static DropMoney OnDropMoney;
    public delegate void ShowBack();
    public static ShowBack OnShowBack;
    public delegate void BusSwerve();
    public static BusSwerve OnBusSwerve;

    public AudioSource driveBus;
    public AudioSource doorOpen;
    public AudioSource[] moneyDrops;
    public AudioSource rustle;
    public AudioSource tireScreech;

    private void OnEnable() {
        OnDriveBus += PlayDriveBus;
        OnStopBus += StopDriveBus;
        OnOpenDoor += PlayDoorOpen;
        OnDropMoney += PlayMoneyDrop;
        OnShowBack += PlayRustle;
        OnBusSwerve += PlayTireScreech;
    }

    private void OnDisable() {
        OnDriveBus -= PlayDriveBus;
        OnStopBus -= StopDriveBus;
        OnOpenDoor -= PlayDoorOpen;
        OnDropMoney -= PlayMoneyDrop;
        OnShowBack -= PlayRustle;
        OnBusSwerve -= PlayTireScreech;
    }

    void PlayDriveBus() {
        driveBus.Play();
    }

    void StopDriveBus() {
        driveBus.Stop();
    }

    void PlayDoorOpen() {
        doorOpen.Play();   
    }

    void PlayMoneyDrop() {
        int randomAudio = Random.Range(0, moneyDrops.Length);
        moneyDrops[randomAudio].Play();
    }

    void PlayRustle() {
        StartCoroutine(Rustle());
    }

    IEnumerator Rustle() {
        rustle.time = 1f;
        rustle.Play();
        yield return new WaitForSeconds(1f);
        rustle.Stop();
    }

    void PlayTireScreech() {
        tireScreech.Play();
    }
}
