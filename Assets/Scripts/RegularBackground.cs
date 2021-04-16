using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularBackground : MonoBehaviour {
    public void CheckIfBackgroundShouldChange() {
            Background.OnCheckQueue?.Invoke();
    }
}
