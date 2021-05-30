using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Passenger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public enum Type {
        Child,
        Adult,
        Senior
    }

    enum State {
        BOARDING,
        BOARDED,
        MISDEMEANOR,
        EXITING
    }

    public Sprite boarding;
    public Sprite boardingHighlighted;
    public Sprite sitting;
    public Sprite sittingHighlighted;
    public Sprite smoking;
    public Sprite smokingHighlighted;
    GameObject smokeParticles;

    public Type type;
    public float paidFare;

    Image image;
    Sprite startingSprite;
    State currentState;
    bool isPointerEnter;
    bool isHighlightValid;
    Coroutine misdemeanorRoutine;

    private void Awake() {
        image = GetComponent<Image>();
        foreach (Transform child in transform) {
            if (child.tag == "Smoke") {
                smokeParticles = child.gameObject;
            }
        }
        startingSprite = image.sprite;
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1) && isPointerEnter && GameManager.isPlayerHoldingCoins) {
            FareEvent.OnRejectPayment?.Invoke();
        }

        if (Input.GetMouseButtonDown(0) && isPointerEnter && Bus.isLookingBack) {
            //DisplayManager.OnPassengerClick?.Invoke();
            Kick();
        }

        if (misdemeanorRoutine == null) {
            misdemeanorRoutine = StartCoroutine(MisdemeanorRoutine());
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isPointerEnter = true;
        if (!isHighlightValid) {
            return;
        }
        Highlight();
    }

    public void OnPointerExit(PointerEventData eventData) {
        isPointerEnter = false;
        if (!isHighlightValid) {
            return;
        }
        if (Bus.isLookingBack) {
            if (currentState == State.MISDEMEANOR) {
                image.sprite = smoking;
            } else {
                image.sprite = sitting;
            }
        } else {
            image.sprite = boarding;
        }
    }

    public void Board() {
        UpdateState(State.BOARDING);
    }

    public void Leave() {
        Bus.OnDisembark?.Invoke();
    }

    public void Kick() {
        StartCoroutine(KickRoutine());
    }

    public void Stay() {
        UpdateState(State.BOARDED);
    }

    IEnumerator MisdemeanorRoutine() {
        while (currentState == State.BOARDED) {
            float delay = Random.Range(3f, 10f);
            float misdemeanorChance = Random.Range(0f, 1f);
            bool shouldActivate = misdemeanorChance >= 0.75f;
            if (true) {
                UpdateState(State.MISDEMEANOR);
                break;
            } else {
                yield return new WaitForSeconds(delay);
            }
        }
    }

    void SmokeCigarette() {
        image.sprite = smoking;
        smokeParticles.SetActive(true);
    }

    void PayFare() {
        float isCorrectFare = Random.Range(0f, 1f);

        switch (type) {
            case Type.Child: {
                paidFare = GameManager.CHILD_FARE;
                break;
            }
            case Type.Adult: {
                paidFare = GameManager.ADULT_FARE;
                break;
            }
            case Type.Senior: {
                paidFare = GameManager.SENIOR_FARE;
                break;
            }
        }

        // 25% chance to pay wrong fare
        if (isCorrectFare >= 0.75f) {
            SkewFarePayment();
        }

        FareBox.OnPresentFare?.Invoke(paidFare);
    }

    void Highlight() {
        if (GameManager.isPlayerHoldingCoins) {
            image.sprite = boardingHighlighted;
        } else if (Bus.isLookingBack) {
            if (currentState == State.MISDEMEANOR) {
                image.sprite = smokingHighlighted;
            } else {
                image.sprite = sittingHighlighted;
            }
        }
    }

    void SkewFarePayment() {
        // TODO: enhance
        paidFare += 1f;
    }

    void UpdateState(State state) {
        currentState = state;
        if (smokeParticles != null) {
            smokeParticles.SetActive(false);
        }
        switch (state) {
            case State.BOARDING: {
                isHighlightValid = true;
                image.sprite = boarding;
                PayFare();
                break;
            }
            case State.BOARDED: {
                image.sprite = sitting;
                Bus.OnSit?.Invoke();
                break;
            }
            case State.MISDEMEANOR: {
                SmokeCigarette();
                break;
            }
            case State.EXITING: {
                image.sprite = boarding;
                break;
            }
        }
    }

    IEnumerator KickRoutine() {
        isHighlightValid = false;
        foreach (Transform child in transform) {
            if (child.tag == "Smoke") {
                Destroy(child.gameObject);
            }
        }
        yield return StartCoroutine(FadeOut());
        UpdateState(State.EXITING);
        Bus.OnKick?.Invoke(gameObject);
        yield break;
    }

    IEnumerator FadeOut() {
        float timeElapsed = 0f;
        float totalTime = 1f;
        Color color = image.color;

        while (timeElapsed <= totalTime) {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(color.a, 0, ( timeElapsed / totalTime ));
            Color newColor = image.color;
            newColor.a = newAlpha;
            image.color = newColor;
            yield return null;
        }
    }

}