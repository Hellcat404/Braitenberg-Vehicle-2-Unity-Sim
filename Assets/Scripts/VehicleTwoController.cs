using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTwoController : MonoBehaviour {
    private Rigidbody rb;
    private GameObject sensorR;
    private GameObject sensorL;

    private List<float> distancesL;
    private List<float> distancesR;

    [SerializeField]
    public bool TypeToggle = false;
    [SerializeField]
    public bool V3 = false;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        sensorR = transform.Find("SensorR").gameObject;
        sensorL = transform.Find("SensorL").gameObject;

        distancesL = new List<float>();
        distancesR = new List<float>();
    }

    // Update is called once per frame
    void Update() {
        distancesL.Clear();
        distancesR.Clear();
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Light")) {
            Vector3 dif = obj.transform.position - gameObject.transform.position;
            if(Vector3.Distance(dif.normalized, gameObject.transform.forward) > 1.75) continue;
            float dist = Mathf.Sqrt((dif.x * dif.x) + (dif.z * dif.z));
            if(dist > 40f) continue;

            Vector3 difL = obj.transform.position - sensorL.transform.position;
            Vector3 difR = obj.transform.position - sensorR.transform.position;
            distancesL.Add(Mathf.Sqrt((difL.x * difL.x) + (difL.z * difL.z)));
            distancesR.Add(Mathf.Sqrt((difR.x * difR.x) + (difR.z * difR.z)));
            Vector3 dirL = difL.normalized;
            Vector3 dirR = difR.normalized;
            Debug.DrawRay(sensorL.transform.position, dirL, Color.blue);
            Debug.DrawRay(sensorR.transform.position, dirR, Color.red);
        }

        float speedL = 0f;
        float speedR = 0f;
        if(!V3) {
            //Speed up toward light
            //Left speed controller
            foreach(float dist in distancesL) {
                float distSqr = (dist * dist) > 1 ? (dist * dist) : 1;
                speedL += (1 + (1 / distSqr)) / distancesL.Count;
            }
            //Right speed controller
            foreach(float dist in distancesR) {
                float distSqr = (dist * dist) > 1 ? (dist * dist) : 1;
                speedR += (1 + (1 / distSqr)) / distancesR.Count;
            }

            if(TypeToggle) {
                rb.velocity = (gameObject.transform.forward * speedL) + (gameObject.transform.forward * speedR);
                rb.AddTorque(Vector3.up * (speedL - speedR), ForceMode.Force);
            } else {
                rb.velocity = (gameObject.transform.forward * speedL) + (gameObject.transform.forward * speedR);
                rb.AddTorque(-(Vector3.up * (speedL - speedR)), ForceMode.Force);
            }
        } else {
            //Slow down toward light
            //Left speed controller
            foreach(float dist in distancesL) {
                float distSqr = (dist * dist) > 1 ? (dist * dist) : 1;
                speedL += 1 - (1 / distSqr);
            }
            //Right speed controller
            foreach(float dist in distancesR) {
                float distSqr = (dist * dist) > 1 ? (dist * dist) : 1;
                speedR += 1 - (1 / distSqr);
            }

            if(TypeToggle) {
                rb.velocity = (gameObject.transform.forward * speedL) + (gameObject.transform.forward * speedR);
                rb.AddTorque(-(Vector3.up * (speedL - speedR)), ForceMode.Force);
            } else {
                rb.velocity = (gameObject.transform.forward * speedL) + (gameObject.transform.forward * speedR);
                rb.AddTorque(Vector3.up * (speedL - speedR), ForceMode.Force);
            }
        }
    }
}
