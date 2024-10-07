using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Critter _critterTemplate;
    [SerializeField] private Flock _flock;
    [SerializeField] private Whale _whale;
    [SerializeField] private Pearl _pearl;
    [SerializeField] private TMPro.TextMeshPro _scoreText;
    [SerializeField] private TMPro.TextMeshPro _gameOverText;

    enum State {
        Playing,
        GameOver
    }

    private State _state = State.Playing;
    private int _score = 0;

    // Start is called before the first frame update
    void Start() {
        ResetGame();
    }

    // Update is called once per frame
    void Update() {
        _scoreText.text = "Score: " + _score;
        switch (_state) {
            case State.Playing:
                int count = 0;
                foreach (Transform critter in _critterTemplate.transform.parent) {
                    if (!critter.gameObject.activeSelf || critter == this.transform) {
                        continue;
                    }
                    count++;
                }
                if (count == 0) {
                    _gameOverText.gameObject.SetActive(true);
                    _state = State.GameOver;
                }
                if (Input.GetMouseButtonDown(0)) {
                    _flock.Burst();
                }
                break;
            case State.GameOver:
                if (Input.GetMouseButtonDown(0)) {
                    ResetGame();
                }
                break;
        }
    }

    public void ResetGame() {
        _gameOverText.gameObject.SetActive(false);
        CreateCritter(Vector3.zero, Quaternion.identity);
        _whale.Reset();
        _score = 0;
        _state = State.Playing;
    }

    public void CreateCritter(Vector3 position, Quaternion orientation) {
        Critter newCritter = Instantiate(_critterTemplate, position, orientation);
        newCritter.gameObject.SetActive(true);
        newCritter.transform.parent = _critterTemplate.transform.parent;

        _score++;

        _whale.IncreaseSpeed();
    }
}
