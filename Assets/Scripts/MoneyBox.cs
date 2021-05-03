using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoneyBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    //TODO: Consolidate with passenger highlighting
    public Sprite highlighted;

    Image image;
    Sprite startingSprite;
    bool isPointerEnter;

    private void Awake() {
        image = GetComponent<Image>();
        startingSprite = image.sprite;
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1) && isPointerEnter) {
            FareEvent.OnAcceptPayment?.Invoke();
        }
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
