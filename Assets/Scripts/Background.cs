using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    public delegate void LeftLaneChange(bool skipAnimation, bool isRushed);
    public static LeftLaneChange OnLeftLaneChange;
    public delegate void RightLaneChange(bool skipAnimation, bool isRushed);
    public static RightLaneChange OnRightLaneChange;
    public delegate void InitLeftLane();
    public static InitLeftLane OnInitLeftLane;
    public delegate void InitRightLane();
    public static InitRightLane OnInitRightLane;

    public GameObject leftLanePosition;
    public GameObject rightLanePosition;
    public GameObject image;

    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        OnLeftLaneChange += GoToLeftLane;
        OnRightLaneChange += GoToRightLane;
        OnInitLeftLane += InitLeft;
        OnInitRightLane += InitRight;
    }

    private void OnDisable() {
        OnLeftLaneChange -= GoToLeftLane;
        OnRightLaneChange -= GoToRightLane;
        OnInitLeftLane -= InitLeft;
        OnInitRightLane -= InitRight;
    }

    public void InitLeft() {
        image.transform.position = leftLanePosition.transform.position;
    }

    public void InitRight() {
        image.transform.position = rightLanePosition.transform.position;
    }

    public void ReduceAnimationSpeed() {
 
    }

    public void GoToLeftLane(bool skipAnimation, bool isRushed) {
        StopAllCoroutines();
        if (isRushed) {
            StartCoroutine(GoToPositionRush(leftLanePosition.transform.position));
        } else {
            StartCoroutine(GoToPositionSmooth(leftLanePosition.transform.position, skipAnimation));
        }
    }

    public void GoToRightLane(bool skipAnimation, bool isRushed) {
        StopAllCoroutines();
        if (isRushed) {
            StartCoroutine(GoToPositionRush(rightLanePosition.transform.position));
        } else {
            StartCoroutine(GoToPositionSmooth(rightLanePosition.transform.position, skipAnimation));
        }
    }

    //IEnumerator ChangeAnimationSpeed(int target) {
    //    float timeElapsed = 0f;
    //    float totalTime = 1f;
    //    while (timeElapsed <= totalTime) {
    //        //animator.sp
    //        //yield return null;
    //    }
    //}

    IEnumerator GoToPositionSmooth(Vector3 newPos, bool skipAnimation) {
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

    IEnumerator GoToPositionRush(Vector3 newPos) {
        Debug.Log("TODO: Implement GoToPositionRush");
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
