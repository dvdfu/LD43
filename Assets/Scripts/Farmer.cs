using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Player {
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
    Timer attackCooldownTimer;

	protected override void Awake() {
        base.Awake();
        hasAxe = true;
        attackCooldownTimer = new Timer(attackCooldown, true);
		previousDirection = Vector2.right;
	}

	void Update() {
        if (hasAxe) {
            if (Input.GetKeyDown(attackKey)) {
                if (attackCooldownTimer.isDone()) {
                    speed = maxSpeed * axeThrowSlowdown;
                }
            } else if (Input.GetKeyUp(attackKey)) {
                speed = maxSpeed;
                hasAxe = false;
                attackCooldownTimer.start();
                GameObject axeThrow = Instantiate(axeThrowPrefab, transform.position, Quaternion.identity);
                axeThrow.GetComponent<AxeThrow>().Throw(id, previousDirection);
            }
        }
        
		Vector2 move = GetMoveVector();
		if (move != Vector2.zero) previousDirection = move;
	}

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "AxePickup" && !hasAxe) {
            hasAxe = true;
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Drumstick") {
            Score.instance.CollectPoint(id);
            Destroy(other.gameObject);
        }
    }
}
