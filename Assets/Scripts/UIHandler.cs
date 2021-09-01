using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _DebrisCollected;

    void Update()
    {
        _DebrisCollected.text = "Debris Collected: " + GameHandler.DebrisCollected.ToString();
    }
}
