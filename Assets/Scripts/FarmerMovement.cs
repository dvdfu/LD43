using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour {
	const float speed = 80.0f;
	Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		Vector2 move = Vector2.zero;
		if (Input.GetKey(KeyCode.LeftArrow)) move.x--;
		if (Input.GetKey(KeyCode.RightArrow)) move.x++;
		if (Input.GetKey(KeyCode.UpArrow)) move.y++;
		if (Input.GetKey(KeyCode.DownArrow)) move.y--;

		move = move.normalized * Time.deltaTime * speed;
		body.MovePosition(body.position + move);
	}
}
