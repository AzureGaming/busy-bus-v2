using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoor : MonoBehaviour {
    public delegate void Open();
    public static Open OnOpen;
    public delegate void Close();
    public static Close OnClose;

    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        OnOpen += PlayAnimation;
        OnClose += PlayAnimationReverse;
    }

    private void OnDisable() {
        OnOpen -= PlayAnimation;
        OnClose -= PlayAnimationReverse;
    }

    void PlayAnimation() {
        animator.SetTrigger("Open");
    }

    void PlayAnimationReverse() {
        animator.SetTrigger("Close");
    }
}
