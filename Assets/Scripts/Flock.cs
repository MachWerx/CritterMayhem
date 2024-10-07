using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    public Vector3 center {
        get {
            return _center;
        }
    }

    private Vector3 _center;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float weight = 0;
        Vector3 newCenter = Vector3.zero;

        foreach (Transform critter in transform) {
            if (!critter.gameObject.activeSelf) {
                continue;
            }

            newCenter += critter.position;
            weight += 1;
        }

        if (weight > 0) {
           _center = newCenter / weight;
        }
    }

    public void Burst() {
        float count = 0;
        foreach (Transform critter in transform) {
            if (!critter.gameObject.activeSelf || critter == this.transform) {
                continue;
            }

            count++;
        }

        foreach (Transform critter in transform) {
            if (!critter.gameObject.activeSelf || critter == this.transform) {
                continue;
            }
            Critter c = critter.GetComponent<Critter>();
            c.Burst(1.0f + 0.2f * (count - 1));
        }


    }
}
