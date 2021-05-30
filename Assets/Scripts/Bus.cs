using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Bus : MonoBehaviour {
    public enum Lane {
        Left,
        Right
    }

    public delegate void ChangeLane(Lane lane, bool isRushed);
    public static ChangeLane OnLaneChange;
    public delegate void Drive();
    public static Drive OnDrive;
    public delegate void Board();
    public static Board OnBoard;
    public delegate void Sit();
    public static Sit OnSit;
    public delegate void Kick(GameObject passenger);
    public static Kick OnKick;
    public delegate void Disembark();
    public static Disembark OnDisembark;
    public delegate void CloseFareBox();
    public static CloseFareBox OnCloseFareBox;

    public static Lane currentLane = Lane.Right;
    public static Passenger currentPassenger;

    public GameObject fareBox;
    public GameObject backOfBus;
    public GameObject kickedContainer;
    public static bool isLookingBack;

    List<Passenger> passengers;

    private void Awake() {
        passengers = new List<Passenger>();
    }

    private void Start() {
        DisplayManager.OnLookForward?.Invoke();
        if (currentLane == Lane.Left) {
            Background.OnInitLeftLane?.Invoke();
        } else {
            Background.OnInitRightLane?.Invoke();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { //testing
            TriggerPassengerPickup();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            DisplayManager.OnLookBack?.Invoke();
            isLookingBack = true;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            DisplayManager.OnLookForward?.Invoke();
            isLookingBack = false;
        }
    }

    private void OnEnable() {
        OnLaneChange += HandleLaneChange;
        OnDrive += Accelerate;
        OnBoard += BoardPassenger;
        OnSit += SeatPassenger;
        OnKick += KickPassenger;
        OnDisembark += DisembarkPassenger;
        OnCloseFareBox += DisableFareBox;
    }

    private void OnDisable() {
        OnLaneChange -= HandleLaneChange;
        OnDrive -= Accelerate;
        OnBoard -= BoardPassenger;
        OnSit -= SeatPassenger;
        OnKick -= KickPassenger;
        OnDisembark -= DisembarkPassenger;
        OnCloseFareBox -= DisableFareBox;
    }

    void ChangeLaneLeft(bool isRushed) {
        Background.OnLeftLaneChange?.Invoke(isRushed);
    }

    void ChangeLaneRight(bool isRushed) {
        Background.OnRightLaneChange?.Invoke(isRushed);
    }

    void TriggerPassengerPickup() {
        Background.OnShowBusStop?.Invoke();
    }

    void Accelerate() {
        Background.OnResumeDriving?.Invoke();
    }

    void BoardPassenger() {
        fareBox.SetActive(true);
        BoardingQueue.SpawnPassengers?.Invoke(); // sets current passenger
    }

    void DisembarkPassenger() {
        Debug.LogWarning("TODO: IMPLEMENT");
    }

    void KickPassenger(GameObject passenger) {
        Debug.LogWarning("TODO: IMPLEMENT");
        StartCoroutine(KickRoutine(passenger));
    }

    void DisableFareBox() {
        FareBox.OnRemoveFare?.Invoke();
        fareBox.SetActive(false);
    }

    void SeatPassenger() {
        passengers.Add(currentPassenger);

        Seat[] validSeats = backOfBus.GetComponentsInChildren<Seat>().Where((Seat seat) => !seat.isOccupied).ToArray();

        if (validSeats.Length > 0) {
            int randomSeat = Random.Range(0, validSeats.Length);
            validSeats[randomSeat].SetParent(currentPassenger.gameObject);
        } else {
            Debug.LogWarning("Cannot add new passenger, seats full.");
        }
    }

    void HandleLaneChange(Lane lane, bool isRushed) {
        if (lane == Lane.Left) {
            ChangeLaneLeft(isRushed);
        } else {
            ChangeLaneRight(isRushed);
        }
    }

    IEnumerator KickRoutine(GameObject passenger) {
        passenger.transform.SetParent(kickedContainer.transform, false);
        yield return StartCoroutine(FadeIn(passenger));
        yield return new WaitForSeconds(0.5f);
        BackDoor.OnOpen?.Invoke();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOut(passenger));
        yield return new WaitForSeconds(0.5f);
        BackDoor.OnClose?.Invoke();
    }

    IEnumerator FadeOut(GameObject passenger) {
        float timeElapsed = 0f;
        float totalTime = 1f;
        Color color = passenger.GetComponent<Image>().color;

        while (timeElapsed <= totalTime) {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(color.a, 0, ( timeElapsed / totalTime ));
            Color newColor = passenger.GetComponent<Image>().color;
            newColor.a = newAlpha;
            passenger.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }


    IEnumerator FadeIn(GameObject passenger) {
        float timeElapsed = 0f;
        float totalTime = 1f;
        Color color = passenger.GetComponent<Image>().color;

        while (timeElapsed <= totalTime) {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(color.a, 1, ( timeElapsed / totalTime ));
            Color newColor = passenger.GetComponent<Image>().color;
            newColor.a = newAlpha;
            passenger.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }
}
