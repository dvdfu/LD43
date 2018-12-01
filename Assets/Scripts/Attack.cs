using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	[SerializeField] float attackHoldTheshold = 0.25f;
	[SerializeField] float closeAttackDuration = 1f;
	[SerializeField] Collider2D weaponCollider;
	[SerializeField] MovementBindings movementBindings;
	[SerializeField] KeyCode attackKey = KeyCode.RightShift;
	[SerializeField] GameObject axeThrowPrefab;

	Vector2 previousDirection;

	Timer timer;
	Timer closeAttackTimer;

	Coroutine attackCoroutine;

	void Start() {
		timer = new Timer();
		closeAttackTimer = new Timer(closeAttackDuration);
		previousDirection = Vector2.zero;
		weaponCollider.gameObject.SetActive(false);
	}

	void Update () {
		Vector2 move = Util.GetMovementVector(movementBindings);
		Debug.Log(previousDirection);
		weaponCollider.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, previousDirection));

		timer.update(Time.deltaTime);

		if (Input.GetKeyDown(attackKey)) timer.start();
		if (Input.GetKeyUp(attackKey)) {
			if (timer.stop() < attackHoldTheshold) {
				if (attackCoroutine == null) attackCoroutine = StartCoroutine(CloseAttack());
			} else {
				GameObject axeThrow = Instantiate(axeThrowPrefab, transform.position, Quaternion.identity);
				axeThrow.GetComponent<AxeThrow>().Throw(Mathf.Atan2(previousDirection.y, previousDirection.x));
			}
		}

		if (move != Vector2.zero) previousDirection = move;
	}

	IEnumerator CloseAttack() {
		closeAttackTimer.start();

		weaponCollider.gameObject.SetActive(true);
		do {
			yield return null;
			closeAttackTimer.update(Time.deltaTime);
		} while(!closeAttackTimer.isDone());

		weaponCollider.gameObject.SetActive(false);
		attackCoroutine = null;
	}
}
