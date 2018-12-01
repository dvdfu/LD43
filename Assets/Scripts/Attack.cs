using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	[SerializeField] float attackHoldTheshold = 0.25f;
	[SerializeField] float closeAttackDuration = 1f;
	[SerializeField] Collider2D weaponCollider;
	[SerializeField] KeyCode attackKey = KeyCode.RightShift;

	Timer timer;
	Timer closeAttackTimer;

	Coroutine attackCoroutine;

	void Start() {
		timer = new Timer(closeAttackDuration);
		closeAttackTimer = new Timer(closeAttackDuration);
	}

	void Update () {
		timer.update(Time.deltaTime);

		if (Input.GetKeyUp(attackKey) && timer.stop() < attackHoldTheshold && attackCoroutine == null) {
			attackCoroutine = StartCoroutine(closeAttack());
		}
	}

	IEnumerator closeAttack() {
		closeAttackTimer.reset();
		while(!closeAttackTimer.isDone()) {
			yield return null;
			closeAttackTimer.update(Time.deltaTime);
		}

		attackCoroutine = null;
	}
}
