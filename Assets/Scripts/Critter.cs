using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour {
    [SerializeField] private InputManager _inputManager;

    private float kDistanceGoal = 0.15f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float speed = 1.0f;
        float weight = 10.0f;
        Vector3 targetPos = weight * transform.position;

        float mouseWeight = 10.0f;
        weight += mouseWeight;
        targetPos += mouseWeight * _inputManager.mousePos;

        foreach (Transform critter in transform.parent) {
            if (!critter.gameObject.activeSelf || critter == this.transform) {
                continue;
            }

            float w = 1.0f / (0.00005f + Mathf.Pow((critter.transform.position - transform.position).magnitude, 2));
            Vector3 target = (transform.position - critter.position).normalized * kDistanceGoal + critter.position;

            targetPos += w * target;
            weight += w;
        }
        targetPos /= weight;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.FromToRotation(Vector3.forward, targetPos - transform.position),
            1 - Mathf.Pow(0.01f, Time.deltaTime));

        transform.position += speed * Time.deltaTime * (transform.rotation * Vector3.forward);
    }
}
