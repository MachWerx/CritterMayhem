using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour {
    private float kDistanceGoal = 0.15f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float speed = 1.0f;
        Vector3 targetPos = 0 * transform.position;
        float weight = 0 * 1.0f;
        foreach (Transform critter in transform.parent) {
            if (!critter.gameObject.activeSelf || critter == this.transform) {
                continue;
            }

            float w = 1.0f / (0.05f + (critter.transform.position - transform.position).magnitude);
            Vector3 target = (transform.position - critter.position).normalized * kDistanceGoal + critter.position;

            targetPos += w * target;
            weight += w;
        }
        targetPos /= weight;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, targetPos - transform.position);

        transform.position += speed * Time.deltaTime * (transform.rotation * Vector3.forward);
        //transform.position = 0.9f * transform.position + 0.1f * targetPos;
    }
}
