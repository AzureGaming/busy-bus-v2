using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CityBackground : MonoBehaviour {
    public delegate void Animate();
    public static Animate OnAnimate;
    public delegate void StopAnimation();
    public static StopAnimation OnStopAnimation;
    public delegate void ChangeView(Bus.Lane lane);
    public static ChangeView OnChangeView;

    Animator animator;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        OnAnimate += SetFullAnimationSpeed;
        OnStopAnimation += SetZeroAnimationSpeed;
    }

    private void OnDisable() {
        OnAnimate -= SetFullAnimationSpeed;
        OnStopAnimation -= SetZeroAnimationSpeed;
    }

    public virtual void CheckIfBackgroundShouldChange() {
    }

    void SetZeroAnimationSpeed() {
        animator.speed = 0f;
    }

    void SetFullAnimationSpeed() {
        animator.speed = 1f;
    }
}
