using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    [SerializeField] private Critter _critterTemplate;

    Vector3 _mousePosPrev;
    Vector3 _mouseVel = Vector3.zero;

    // Start is called before the first frame update
    void Start() {
        _mousePosPrev = GetMousePos();
    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePos = GetMousePos();
        Vector3 mouseVelCurrent = (mousePos - _mousePosPrev) / Time.deltaTime;
        float velDrag = 0.9f;
        _mouseVel = velDrag * _mouseVel + (1.0f - velDrag) * mouseVelCurrent;

        if (Input.GetMouseButtonDown(0)) {
            Quaternion forward = Quaternion.FromToRotation(Vector3.forward, _mouseVel);
            Critter newCritter = Instantiate(_critterTemplate, mousePos, forward);
            newCritter.gameObject.SetActive(true);
            newCritter.transform.parent = _critterTemplate.transform.parent;
        }

        _mousePosPrev = mousePos;
    }

    Vector3 GetMousePos() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }
}
