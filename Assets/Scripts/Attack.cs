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
		timer = new Timer();
		closeAttackTimer = new Timer(closeAttackDuration);
		weaponCollider.gameObject.SetActive(false);
	}

	void Update () {
		timer.update(Time.deltaTime);

		if (Input.GetKeyDown(attackKey)) timer.start();
		if (Input.GetKeyUp(attackKey) && timer.stop() < attackHoldTheshold && attackCoroutine == null) {
			attackCoroutine = StartCoroutine(closeAttack());
		}
	}

	IEnumerator closeAttack() {
		closeAttackTimer.start();

		weaponCollider.gameObject.SetActive(true);
		while(!closeAttackTimer.isDone()) {
			yield return null;
			closeAttackTimer.update(Time.deltaTime);
		}

		weaponCollider.gameObject.SetActive(false);
		attackCoroutine = null;
	}
}
