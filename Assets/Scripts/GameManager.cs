using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Critter _critterTemplate;
    [SerializeField] private Whale _whale;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void CreateCritter(Vector3 position, Quaternion orientation) {
        Critter newCritter = Instantiate(_critterTemplate, position, orientation);
        newCritter.gameObject.SetActive(true);
        newCritter.transform.parent = _critterTemplate.transform.parent;

        _whale.IncreaseSpeed();
    }
}
