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

    public Sprite boardingSprite;
    public Sprite sittingSprite;
    public Sprite rowdySprite;
    public Sprite highlighted;

    public Type type;
    public float paidFare;

    Image image;
    Sprite startingSprite;

    bool isRowdy = false;
    bool isSitting = false;
    bool isBoarding = false;

    bool isPointerEnter;

    private void Awake() {
        image = GetComponent<Image>();
        startingSprite = image.sprite;
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1) && isPointerEnter) {
            FareEvent.OnRejectPayment?.Invoke();
        }
    }

    public void Board() {
        PayFare();
    }

    public void Leave() {
        Bus.OnDisembark?.Invoke();
    }

    public void Kick() {
        Bus.OnKick?.Invoke();
    }

    public void Stay() {
        image.sprite = sittingSprite;
        Bus.OnSit?.Invoke();
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

    void SkewFarePayment() {
        // TODO: enhance
        paidFare += 1f;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (GameManager.isPlayerHoldingCoins) {
            isPointerEnter = true;
            image.sprite = highlighted;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        isPointerEnter = false;
        if (image.sprite == highlighted) {
            image.sprite = startingSprite;
        }
    }
}