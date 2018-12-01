using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Movement {
	[SerializeField] KeyCode attackKey = KeyCode.RightShift;

    const float COOLDOWN_SEC = 0.8f;
    const float ROLL_DURATION_SEC = 0.6f;
    const float ROLL_MOVE_SPEED = 400;

    bool isRolling;
    Tween rollTween;
    Vector2 rollDirection;

    protected override void Awake() {
        base.Awake();
        rollTween = new Tween(this);
    }

    void Update() {
        if (isRolling) {
        } else {
            if (Input.GetKey(attackKey)) {
                isRolling = true;
                rollDirection = GetMoveVector();
                rollTween.Start(ROLL_DURATION_SEC, (float progress) => {}, () => {
                    isRolling = false;
                });
            }
        }
    }
}
