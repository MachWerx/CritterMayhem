using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour {
    [SerializeField] Flock _flock;
    [SerializeField] MeshRenderer _geometry;

    private Material _material;
    private float _maxSpeed = 0.5f;
    private float _turnDrag = 0.2f;

    // Start is called before the first frame update
    void Start() {
        _material = _geometry.material;
        //Color bodyColor = _material.color;
    }

    // Update is called once per frame
    void Update() {
        Vector3 targetPos = _flock.center;

        Quaternion targetAngle = Quaternion.FromToRotation(Vector3.forward, targetPos - transform.position);
        float angle = Quaternion.Angle(transform.rotation, targetAngle);
        float speed = _maxSpeed / (1.0f + angle / 60.0f);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetAngle,
            1 - Mathf.Pow(_turnDrag, Time.deltaTime));

        transform.position += speed * Time.deltaTime * (transform.rotation * Vector3.forward);
    }

    public void IncreaseSpeed() {
        _maxSpeed += .2f;
        _turnDrag *= 0.9f;
    }
}
