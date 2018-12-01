﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour {
	const float speed = 100.0f;
	Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		Vector2 move = Vector2.zero;
		if (Input.GetKey(KeyCode.A)) move.x--;
		if (Input.GetKey(KeyCode.D)) move.x++;
		if (Input.GetKey(KeyCode.W)) move.y++;
		if (Input.GetKey(KeyCode.S)) move.y--;

		move = move.normalized * Time.deltaTime * speed;
		body.MovePosition(body.position + move);
	}
}
