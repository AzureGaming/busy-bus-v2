using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    // TODO: enforce singleton
    public enum Lane {
        Left,
        Right
    }
    public static Lane currentLane = Lane.Left;
}
