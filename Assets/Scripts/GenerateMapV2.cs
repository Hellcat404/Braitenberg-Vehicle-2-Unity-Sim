using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapV2 : MonoBehaviour {    
    public GameObject lightPrefab;
    
    [SerializeField]
    public bool Toggle;

    // Start is called before the first frame update
    void Start() {
        if(!Toggle) return;
        for(int x = -25; x < 25; x++) {
            for(int y = -25; y < 25; y++) {
                if(x == 0 && y == 0) continue;
                if(Random.Range(0, 100) % 10 == 0)
                    Instantiate(lightPrefab, new Vector3(x*10,0.5f,y*10), Quaternion.Euler(Vector3.zero));
            }
        }
    }
}
