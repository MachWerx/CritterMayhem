using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float speed = 1.0f;
        transform.position += speed * Time.deltaTime * (transform.rotation * Vector3.forward);
    }
}
