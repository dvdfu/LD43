using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
	public static Vector2 GetMovementVector(MovementBindings movementBindings) {
		Vector2 move = Vector2.zero;
		if (Input.GetKey(movementBindings.left)) move.x--;
		if (Input.GetKey(movementBindings.right)) move.x++;
		if (Input.GetKey(movementBindings.up)) move.y++;
		if (Input.GetKey(movementBindings.down)) move.y--;
		return move;
	}
}
