using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {
    public Sprite tiny;
    public Sprite small;
    public Sprite medium;
    public Sprite large;

    public Transform endPos;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Start() {
        image.sprite = tiny;
        StartCoroutine(Move());
    }


    IEnumerator Move() {
        float totalTime = 10f;
        float timeElapsed = 0f;
        Vector2 startPos = transform.position;

        image.sprite = tiny;
        yield return new WaitForSeconds(2f);
        image.sprite = small;
        yield return new WaitForSeconds(1.5f);

        while (timeElapsed < totalTime) {
            if (transform.localPosition.x <= -17) {
                image.sprite = medium;
            }

            if (transform.localPosition.x <= -80) {
                image.transform.localScale = new Vector2(0.8f, 0.8f);
                image.sprite = large;
            }
            timeElapsed += Time.deltaTime;


            transform.position = Hermite(startPos, endPos.position, timeElapsed / totalTime);
            yield return null;
        }
    }

    float Hermite(float start, float end, float value) {
        return Mathf.Lerp(start, end, value * value * ( 3.0f - 2.0f * value ));
    }

    Vector2 Hermite(Vector2 start, Vector2 end, float value) {
        return new Vector2(Hermite(start.x, end.x, value), Hermite(start.y, end.y, value));
    }
}
