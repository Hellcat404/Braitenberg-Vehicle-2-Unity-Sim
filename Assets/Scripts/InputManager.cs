using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {
    VehicleTwoController v2c = null;

    private void Start() {
        v2c = GetComponent<VehicleTwoController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        else if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else if (Input.GetKeyDown(KeyCode.T))
            v2c.TypeToggle = !v2c.TypeToggle;
        else if (Input.GetKeyDown(KeyCode.V))
            v2c.V3 = !v2c.V3;
    }
}
