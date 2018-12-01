using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Player {
	[SerializeField] float attackHoldTheshold = 0.25f;
	[SerializeField] float closeAttackDuration = 1f;
	[SerializeField] float closeAttackOffset = 15f;

	[SerializeField] KeyCode attackKey = KeyCode.RightShift;
	[SerializeField] GameObject axeThrowPrefab;
    [SerializeField] GameObject axeSwingPrefab;

    bool hasAxe;
	Vector2 previousDirection;
	Timer timer;

	protected override void Awake() {
        base.Awake();
        hasAxe = true;
		timer = new Timer();
		previousDirection = Vector2.zero;
	}

	void Update() {
		Vector2 move = GetMoveVector();

		timer.update(Time.deltaTime);

        if (hasAxe) {
            if (Input.GetKeyDown(attackKey)) timer.start();
            if (Input.GetKeyUp(attackKey)) {
                if (timer.stop() < attackHoldTheshold) {
                    GameObject axeSwing = Instantiate(axeSwingPrefab, transform.position, Quaternion.identity);
                    axeSwing.GetComponent<AxeSwing>().Swing(transform.position, previousDirection);
                } else {
                    GameObject axeThrow = Instantiate(axeThrowPrefab, transform.position, Quaternion.identity);
                    axeThrow.GetComponent<AxeThrow>().Throw(previousDirection);
                    hasAxe = false;
                }
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
