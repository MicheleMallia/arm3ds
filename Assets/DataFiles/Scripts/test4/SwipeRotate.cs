using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRotate : MonoBehaviour {

	public Transform Surface;


	void Start(){
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update()
	{
		if (Input.touchCount == 1)
		{
			// GET TOUCH 0
			Touch touch0 = Input.GetTouch(0);

			// APPLY ROTATION
			if (touch0.phase == TouchPhase.Moved)
			{
				Surface.transform.Rotate(touch0.deltaPosition.y, -touch0.deltaPosition.x, 0f);

			}

		}
	}
}
