using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : MonoBehaviour {
    [SerializeField] private GameManager _gameManager;

    // Start is called before the first frame update
    void Start() {
        ResetPosition();
    }

    // Update is called once per frame
    void Update() {

    }

    public void CheckCollision(Transform critterTransform) {
        if ((transform.position - critterTransform.position).magnitude < 0.5f * (transform.lossyScale.x + critterTransform.lossyScale.x)) {
            _gameManager.CreateCritter(transform.position, Quaternion.FromToRotation(Vector3.forward, Random.onUnitSphere));
            ResetPosition();
        }
    }

    void ResetPosition() {
        Vector2 newPos = Random.insideUnitCircle;
        transform.position = new Vector3(newPos.x, 0, newPos.y);
    }
}
