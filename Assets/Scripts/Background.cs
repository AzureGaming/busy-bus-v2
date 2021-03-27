using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    public delegate void LeftLaneChange(bool skipAnimation);
    public static LeftLaneChange OnLeftLaneChange;
    public delegate void RightLaneChange(bool skipAnimation);
    public static RightLaneChange OnRightLaneChange;

    public GameObject leftLanePosition;
    public GameObject rightLanePosition;
    public GameObject image;

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.A)) {
    //        GoToLeftLane();
    //    } else if (Input.GetKeyDown(KeyCode.D)) {
    //        GoToRightLane();
    //    }
    //}

    private void OnEnable() {
        OnLeftLaneChange += GoToLeftLane;
        OnRightLaneChange += GoToRightLane;
    }

    private void OnDisable() {
        OnLeftLaneChange -= GoToLeftLane;
        OnRightLaneChange -= GoToRightLane;
    }

    public void GoToLeftLane(bool skipAnimation) {
        StopAllCoroutines();
        StartCoroutine(GoToPosition(leftLanePosition.transform.position, skipAnimation));
    }

    public void GoToRightLane(bool skipAnimation) {
        StopAllCoroutines();
        Debug.Log("Go right");
        StartCoroutine(GoToPosition(rightLanePosition.transform.position, skipAnimation));
    }

    IEnumerator GoToPosition(Vector3 newPos, bool skipAnimation) {
        float timeElapsed = 0f;
        float totalTime = 1f;
        Vector3 pos = image.transform.position;

        if (!skipAnimation) {
            while (timeElapsed <= totalTime) {
                Vector3 nextPos = image.transform.position;
                float nextPosX = Mathf.Lerp(pos.x, newPos.x, ( timeElapsed / totalTime ));
                nextPos.x = nextPosX;
                image.transform.position = nextPos;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        image.transform.position = newPos;
    }
}
