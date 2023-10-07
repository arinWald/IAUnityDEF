using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockScript : MonoBehaviour
{

	public FlockingManager myManager;
	float speed;

	// Use this for initialization
	void Start()
	{
		speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);

	}

	// Update is called once per frame
	void Update()
	{
		// Reset speed in random moments
		if(Random.Range(0, 100) < 10)
        {
			speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
		}

		// Fish swim for a while in the same direction, then changes the direction
		if (Random.Range(0, 100) < 10)
        {
			ApplyRules();
        }
		transform.Translate(0, 0, Time.deltaTime * speed);

	}
	void ApplyRules()
	{
		GameObject[] gos;
		gos = myManager.allFish;

		Vector3 vcentre = Vector3.zero;
		Vector3 vavoid = Vector3.zero;
		float gSpeed = 0.01f;
		float nDistance;
		int groupSize = 0;

		foreach (GameObject go in gos)
		{
			if (go != this.gameObject)
			{
				nDistance = Vector3.Distance(go.transform.position, this.transform.position);
				if (nDistance <= myManager.neighbourDistance)
				{
					vcentre += go.transform.position;
					groupSize++;

					if (nDistance < 1.0f)
					{
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}

					FlockScript anotherFlock = go.GetComponent<FlockScript>();
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}
		}

		if (groupSize > 0)
		{
			vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
			speed = gSpeed / groupSize;

			// Avoid uncontrolled speed
			if(speed > myManager.maxSpeed)
            {
				speed = myManager.maxSpeed;
            }

			Vector3 direction = (vcentre + vavoid) - transform.position;
			if (direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp(transform.rotation,
													  Quaternion.LookRotation(direction),
													  myManager.rotationSpeed * Time.deltaTime);

		}
	}
}