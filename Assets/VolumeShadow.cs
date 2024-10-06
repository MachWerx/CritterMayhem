using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeShadow : MonoBehaviour {
    [SerializeField] private Light _light;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.rotation = _light.transform.rotation;
    }
}
