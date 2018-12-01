using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour {
    const float DURATION_SEC = 0.8f;
    const float SPEED_START = 250;
    const float SPEED_END = 0;

    float speed;
    float angle;
    Tween moveTween;

    public void Throw(float angle) {
        speed = SPEED_START;
        this.angle = angle;
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
            Vector2 delta = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed * Time.deltaTime;
            transform.position = transform.position + (Vector3) delta;
        }
    }
}
