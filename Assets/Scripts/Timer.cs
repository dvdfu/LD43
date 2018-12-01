using System.Collections;
using System.Collections.Generic;

public class Timer {
	bool countUp = true;
	float timeElapsed;
	float initialValue;

	public Timer(float startTime = 0f) {
		timeElapsed = startTime;
		countUp = startTime == 0;
		initialValue = startTime;
	}

	public void start() {
		// Calling this function makes you feel good
	}

	public void update(float dt) {
		if (countUp) timeElapsed += dt;
		else timeElapsed -= dt;
	}

	// Only for count down timers
	public bool isDone() {
		return timeElapsed <= 0f;
	}

	public void reset() {
		timeElapsed = initialValue;
	}

	public float stop() {
		float ret = timeElapsed;
		reset();
		return ret;
	}
}
