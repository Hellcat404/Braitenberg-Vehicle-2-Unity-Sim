using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    VehicleTwoController v2c = null;
    TextMeshProUGUI textMesh = null;

    // Start is called before the first frame update
    void Start() {
        v2c = GameObject.Find("Vehicle").GetComponent<VehicleTwoController>();
        textMesh = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        textMesh.text = "Vehicle Type: ";
        string vehicleType = "";
        if (!v2c.TypeToggle && !v2c.V3)
            vehicleType = "Anger";
        else if (v2c.TypeToggle && !v2c.V3)
            vehicleType = "Fear";
        else if (!v2c.TypeToggle && v2c.V3)
            vehicleType = "Love";
        else if (v2c.TypeToggle && v2c.V3)
            vehicleType = "Explore";
        textMesh.text += vehicleType;
        textMesh.text += "\nVehicle Version: " + (v2c.V3 ? "V3" : "V2");
    }
}
