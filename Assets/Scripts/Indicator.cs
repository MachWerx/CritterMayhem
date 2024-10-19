using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _visualization;

    private Camera _mainCam; 

    // Start is called before the first frame update
    void Start() {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = _target.position;
        Vector3 pos_VS = 2.0f * (_mainCam.WorldToViewportPoint(pos) - 0.5f * Vector3.one);

        // constrain indicator in x
        float xFactor = Mathf.Abs(pos_VS.x);
        float xThreshold = 0.9f;
        if (xFactor > xThreshold) {
            pos_VS.x *= xThreshold / xFactor;
            pos_VS.y *= xThreshold / xFactor;
        }

        // constrain indicator in y
        float yFactor = Mathf.Abs(pos_VS.y);
        float yThreshold = 0.8f;
        if (yFactor > yThreshold) {
            pos_VS.x *= yThreshold / yFactor;
            pos_VS.y *= yThreshold / yFactor;
        }

        // set the indicator's position
        pos = _mainCam.ViewportToWorldPoint(pos_VS / 2.0f + 0.5f * Vector3.one);
        pos.y = _target.position.y;
        transform.position = pos;

        // set the indicator's rotation
        float angle = Mathf.Atan2(-pos_VS.y, pos_VS.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, angle, 0);

        _visualization.SetActive(xFactor > 1.0f || yFactor > 1.0f);
    }
}
