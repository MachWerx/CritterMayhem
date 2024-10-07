using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour {
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private Whale _whale;
    [SerializeField] private Pearl _pearl;

    private float kDistanceGoal = 0.15f;
    private Material _material;
    private float _speed = 1.0f;

    // Start is called before the first frame update
    void Start() {
        _material = GetComponent<MeshRenderer>().material;
        _trail = GetComponentInChildren<TrailRenderer>();
        Color bodyColor = _material.color;
        _trail.startColor = bodyColor;
        bodyColor.a = 0;
        _trail.endColor = bodyColor;
    }

    // Update is called once per frame
    void Update() {
        _pearl.CheckCollision(transform);

        if ((transform.position - _whale.transform.position).magnitude < 0.5f * (transform.lossyScale.x + _whale.transform.lossyScale.x)) {
            Destroy(gameObject);
        }
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

        transform.position += _speed * Time.deltaTime * (transform.rotation * Vector3.forward);

        _speed = Mathf.Pow(0.3f, Time.deltaTime) * (_speed - 1.0f) + 1.0f;
    }

    public void Burst(float speed) {
        Vector3 newDir = Random.onUnitSphere;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
        _speed = speed;
    }
}
