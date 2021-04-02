using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {
    public Sprite tiny;
    public Sprite small;
    public Sprite medium;
    public Sprite large;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Update() {
        if (transform.localPosition.y <= 0 && transform.localPosition.y >= -10) {
            image.sprite = tiny;
        }

        if (transform.localPosition.y < -10 && transform.localPosition.y >= -20) {
            image.sprite = small;
        }

        if (transform.localPosition.y < -20 && transform.localPosition.y >= -30) {
            image.sprite = medium;
        }

        if (transform.localPosition.y < -30) {
            image.sprite = large;
        }
        Vector3 newPos = transform.position;
        newPos.y -= 0.01f;
        transform.position = newPos;
    }
}
