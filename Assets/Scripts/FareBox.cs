using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FareBox : MonoBehaviour {
    public GameObject dimePrefab;
    public GameObject looniePrefab;
    public GameObject nickelPrefab;
    public GameObject quarterPrefab;
    public GameObject tooniePrefab;
    public Transform spawnArea;

    private void Start() {
        Init();
    }

    private void Init() {
        Instantiate(looniePrefab, spawnArea, false);
    }
}
