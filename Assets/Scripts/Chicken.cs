using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Player {
	[SerializeField] KeyCode attackKey = KeyCode.RightShift;
    [SerializeField] GameObject drumstickPrefab;

    const float COOLDOWN_SEC = 0.05f;
    const float ROLL_DURATION_SEC = 0.3f;
    const float ROLL_MOVE_SPEED = 300;

    bool isRolling;
    float rollSpeed;
    Tween rollTween;
    Vector2 rollDirection;

    protected override void Awake() {
        base.Awake();
        rollTween = new Tween(this);
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        if (isRolling) {
            body.MovePosition(body.position + rollDirection * rollSpeed * Time.deltaTime);
        }
    }

    void Update() {
        if (isRolling) {
        } else {
            if (Input.GetKey(attackKey)) {
                isRolling = true;
                canMove = false;
                rollSpeed = ROLL_MOVE_SPEED;
                rollDirection = GetMoveVector().normalized;
                rollTween.Start(ROLL_DURATION_SEC, (float progress) => {
                    rollSpeed = Mathf.Lerp(ROLL_MOVE_SPEED, 0, progress);
                }, () => {
                    isRolling = false;
                    rollTween.Start(COOLDOWN_SEC, (float progress) => {}, () => {
                        canMove = true;
                    });
                });
            }
        }
    }

    void Die() {
        Instantiate(drumstickPrefab, transform.position, transform.rotation);
        Respawn();
    }

    void Respawn() {
        transform.position = Vector3.zero;
        Debug.Log("bruv");
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Weapon") Die();
    }
}
