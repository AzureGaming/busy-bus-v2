using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularBackground : CityBackground {
    public override void CheckIfBackgroundShouldChange() {
        City.OnCheckQueue?.Invoke();
    }
}
