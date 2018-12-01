using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Player {
	[SerializeField] float attackHoldTheshold = 0.25f;
    [SerializeField] float attackCooldown = 0.5f;

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

        if (hasAxe && attackCooldownTimer.isDone()) {
            if (Input.GetKeyDown(attackKey)) attackHoldTimer.start();
            if (Input.GetKeyUp(attackKey)) {
                if (attackHoldTimer.stop() < attackHoldTheshold) {
                    GameObject axeSwing = Instantiate(axeSwingPrefab, transform.position, Quaternion.identity);
                    axeSwing.GetComponent<AxeSwing>().Swing(transform.position, previousDirection);
                } else {
                    GameObject axeThrow = Instantiate(axeThrowPrefab, transform.position, Quaternion.identity);
                    axeThrow.GetComponent<AxeThrow>().Throw(previousDirection);
                    hasAxe = false;
                }

                attackCooldownTimer.start();
            }
        }

		if (move != Vector2.zero) previousDirection = move;
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (!hasAxe) {
            AxePickup axePickup = other.gameObject.GetComponent<AxePickup>();
            if (axePickup != null) {
                hasAxe = true;
                Destroy(other.gameObject);
            }
        }
    }
}
