using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Player {
    [SerializeField] Score score;
    [SerializeField] Score.PlayerID id;
    [SerializeField] float attackHoldTheshold = 0.25f;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float axeSwingAttackDuration = 0.1f;
    [SerializeField] float axeThrowSlowdown = 0.5f;

	[SerializeField] KeyCode attackKey = KeyCode.RightShift;
	[SerializeField] GameObject axeThrowPrefab;
    [SerializeField] GameObject axeSwingPrefab;

    bool hasAxe;
	Vector2 previousDirection;
	Timer attackHoldTimer;
    Timer attackCooldownTimer;

	protected override void Awake() {
        base.Awake();
        hasAxe = true;
		attackHoldTimer = new Timer();
        attackCooldownTimer = new Timer(attackCooldown, true);
		previousDirection = Vector2.right;
	}

	void Update() {
		Vector2 move = GetMoveVector();
		attackHoldTimer.update(Time.deltaTime);
        attackCooldownTimer.update(Time.deltaTime);

        // Slow down movement while winding up axe throw
        if (attackHoldTimer.isRunning && attackHoldTimer.timeElapsed >= attackHoldTheshold) {
            speed = maxSpeed * axeThrowSlowdown;
        }

        if (hasAxe && attackCooldownTimer.isDone()) {
            if (Input.GetKeyDown(attackKey)) attackHoldTimer.start();
            if (Input.GetKeyUp(attackKey)) {
                if (attackHoldTimer.stop() < attackHoldTheshold) {
                    score.CollectPoint(id);
                    GameObject axeSwing = Instantiate(axeSwingPrefab, transform.position, Quaternion.identity);
                    axeSwing.GetComponent<AxeSwing>().Swing(transform.position, previousDirection);

                    speed = 0f;
                    Tween t = new Tween(this);
                    t.Start(axeSwingAttackDuration, null, () => speed = maxSpeed);
                } else {
                    GameObject axeThrow = Instantiate(axeThrowPrefab, transform.position, Quaternion.identity);
                    axeThrow.GetComponent<AxeThrow>().Throw(previousDirection);
                    hasAxe = false;

                    speed = maxSpeed * axeThrowSlowdown;
                    Tween t = new Tween(this);
                    t.Start(axeSwingAttackDuration, null, () => speed = maxSpeed);
                }

                attackCooldownTimer.start();
            }
        }

		if (move != Vector2.zero) previousDirection = move;
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "AxePickup" && !hasAxe) {
            hasAxe = true;
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Drumstick") {
            Destroy(other.gameObject);
        }
    }
}
