using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour {
    const float DURATION_SEC = 0.4f;
    const float SPEED_START = 400;
    const float SPEED_END = 0;

    [SerializeField] GameObject axePickupPrefab;

    Score.PlayerID id;
    float speed;
    Vector2 direction;
    Tween moveTween;

    public void Throw(Score.PlayerID id, Vector2 direction) {
        this.id = id;
        speed = SPEED_START;
        this.direction = direction.normalized;
        moveTween.Start(DURATION_SEC, (float progress) => {
            speed = Mathf.Lerp(SPEED_START, SPEED_END, progress);
        }, () => {
            Instantiate(axePickupPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        });
    }

    public Score.PlayerID GetID() {
        return id;
    }

    public Vector2 GetDirection() {
        return direction;
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

    void OnCollisionEnter2D(Collision2D collision) {
        ContactPoint2D point = collision.GetContact(0);
        if (point.normal.y != 0) {
            direction.y *= -1;
        }
        if (point.normal.x != 0) {
            direction.x *= -1;
        }
    }
}
