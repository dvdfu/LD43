using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthSort : MonoBehaviour {
    SpriteRenderer sprite;

    void Awake() {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        sprite.sortingOrder = Mathf.FloorToInt(-transform.position.y);
    }
}
