using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [SerializeField] private Flock _flock;
    private float _initialHeight;

    // Start is called before the first frame update
    void Start() {
        _initialHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        float drift = Mathf.Pow(0.5f, Time.deltaTime);

        Vector3 target = drift * transform.position + (1 - drift) * _flock.center;
        target.y = _initialHeight;
        transform.position = target;
    }
}
