using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour {
    const float DURATION_SEC = 0.6f;
    const float SPEED_START = 250;
    const float SPEED_END = 0;

    float speed;
    Vector2 direction;
    Tween moveTween;

    public void Throw(Vector2 direction) {
        speed = SPEED_START;
        this.direction = direction.normalized;
        moveTween.Start(DURATION_SEC, (float progress) => {
            speed = Mathf.Lerp(SPEED_START, SPEED_END, progress);
        }, () => {
            Destroy(gameObject);
        });
    }

    void Awake() {
        moveTween = new Tween(this);
    }

    void FixedUpdate() {
        if (gameObject.activeSelf) {
            Vector2 delta = direction * speed * Time.deltaTime;
            transform.position = transform.position + (Vector3) delta;
        }
    }
}
