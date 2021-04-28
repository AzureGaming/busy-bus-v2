using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FareBox : MonoBehaviour {
    public delegate void PresentFare(float fare);
    public static PresentFare OnPresentFare;
    public delegate void RemoveFare();
    public static RemoveFare OnRemoveFare;

    public GameObject dimePrefab;
    public GameObject looniePrefab;
    public GameObject nickelPrefab;
    public GameObject quarterPrefab;
    public GameObject tooniePrefab;
    public Transform spawnArea;

    private void OnEnable() {
        OnPresentFare += SpawnFare;
        OnRemoveFare += DespawnFare;
    }

    private void OnDisable() {
        OnPresentFare -= SpawnFare;
        OnRemoveFare -= DespawnFare;
    }

    void SpawnFare(float fare) {
        float fareLeft = fare;
        while (fareLeft > 0) {
            int coin = Random.Range(0, 5);
            switch (coin) {
                case 0: {
                    if (fareLeft - 0.05f >= 0) {
                        SpawnAtRandomPosition(nickelPrefab);
                        fareLeft = (float)System.Math.Round(fareLeft - 0.05f, 2);
                    }
                    break;
                }
                case 1: {
                    if (fareLeft - 0.1f >= 0) {
                        SpawnAtRandomPosition(dimePrefab);
                        fareLeft = (float)System.Math.Round(fareLeft - 0.1f, 2);
                    }
                    break;
                }
                case 2: {
                    if (fareLeft - 0.25f >= 0) {
                        SpawnAtRandomPosition(quarterPrefab);
                        fareLeft = (float)System.Math.Round(fareLeft - 0.25f, 2);
                    }
                    break;
                }
                case 3: {
                    if (fareLeft - 1.00f >= 0) {
                        SpawnAtRandomPosition(looniePrefab);
                        fareLeft = (float)System.Math.Round(fareLeft - 1.00f, 2);
                    }
                    break;
                }
                case 4: {
                    if (fareLeft - 2.00f >= 0) {
                        SpawnAtRandomPosition(tooniePrefab);
                        fareLeft = (float)System.Math.Round(fareLeft - 2.00f, 2);
                    }
                    break;
                }
                default: {
                    Debug.LogWarning("Invalid coin was picked.");
                    break;
                }
            }
        }
    }

    void DespawnFare() {
        foreach (Transform child in spawnArea) {
            Destroy(child.gameObject);
        }
    }

    void SpawnAtRandomPosition(GameObject coin) {
        RectTransform rectTransform = spawnArea.GetComponent<RectTransform>();
        float minY = rectTransform.offsetMin.y;
        float minX = rectTransform.offsetMin.x;
        float maxY = rectTransform.offsetMax.y;
        float maxX = rectTransform.offsetMax.x;

        GameObject instantiation = Instantiate(coin, spawnArea);
        instantiation.transform.localPosition = new Vector2(Random.Range(minY, maxY), Random.Range(minX, maxX));
    }
}
