using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour {
    [SerializeField] Flock _flock;
    [SerializeField] MeshRenderer _geometry;

    private Material _material;
    private float _speed = 0.0f;
    private float _maxSpeed = 0.5f;
    private float _maxTurnSpeed = 10.0f;

    // Start is called before the first frame update
    void Start() {
        _material = _geometry.material;
        //Color bodyColor = _material.color;
    }

    // Update is called once per frame
    void Update() {
        Vector3 targetPos = _flock.center;

        Quaternion targetAngle = Quaternion.FromToRotation(Vector3.forward, (targetPos - transform.position).normalized);
        float angle = Quaternion.Angle(transform.rotation, targetAngle);
        float targetSpeed = angle < 90 ? Mathf.Pow(1 - angle / 90.0f, .5f) : 0;
        targetSpeed = .1f + .9f * targetSpeed;
        targetSpeed *= _maxSpeed;
        float acc = 0.5f;
        if (_speed < targetSpeed) {
            _speed += acc * Time.deltaTime;
            if (_speed > targetSpeed) {
                _speed = targetSpeed;
            }
        } else {
            _speed -= acc * Time.deltaTime;
            if (_speed < targetSpeed) {
                _speed = targetSpeed;
            }
        }
        //float targetSpeed = _maxSpeed / (1.0f + 20.0f * angle / 180.0f);

        float turnSpeed = _maxTurnSpeed * Time.deltaTime * (_maxSpeed - .9f * _speed) / _maxSpeed;
        if (angle < turnSpeed) {
            transform.rotation = targetAngle;
            Debug.Log("set: " + turnSpeed + " / " + angle + " = ");
        } else {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetAngle,
                turnSpeed / angle);
        }

        transform.position += _speed * Time.deltaTime * (transform.rotation * Vector3.forward);
    }

    public void IncreaseSpeed() {
        _maxSpeed += 0.2f;
        _maxTurnSpeed += 10.0f;
    }
}
