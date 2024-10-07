using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGarden : MonoBehaviour {
    [SerializeField] private RockDeformer _rockTemplate;
    [SerializeField] private float _radius = 10;
    [SerializeField] private int _rockN = 1000;

    // Start is called before the first frame update
    void Start() {
        //Random.InitState(0);
        for (int i = 0; i < _rockN; i++) {
            RockDeformer rock = Instantiate(_rockTemplate, transform);
            Vector3 pos = _radius * Random.insideUnitSphere;
            pos.y = 0;
            rock.transform.localPosition = pos;
            rock.transform.localScale = Random.Range(0.2f, 1.0f) * Vector3.one;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
