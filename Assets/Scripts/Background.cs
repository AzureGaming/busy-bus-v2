using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    public GameObject leftLanePosition;
    public GameObject rightLanePosition;
    public GameObject image;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            GoToLeftLane();
        } else if (Input.GetKeyDown(KeyCode.D)) {
            GoToRightLane();
        }
    }

    public void GoToLeftLane() {
        StartCoroutine(GoToPosition(leftLanePosition.transform.position));
    }

    public void GoToRightLane() {
        StartCoroutine(GoToPosition(rightLanePosition.transform.position));
    }

    IEnumerator GoToPosition(Vector3 newPos) {
        float timeElapsed = 0f;
        float totalTime = 1f;
        Vector3 pos = image.transform.position;

        while (timeElapsed <= totalTime) {
            Vector3 nextPos = image.transform.position;
            float nextPosX = Mathf.Lerp(pos.x, newPos.x, ( timeElapsed / totalTime ));
            nextPos.x = nextPosX;
            image.transform.position = nextPos;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        image.transform.position = newPos;
    }
}
