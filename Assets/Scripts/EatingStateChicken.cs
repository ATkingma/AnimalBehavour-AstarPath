using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingStateChicken : StateChicken
{
	public SeartchForFoodStateChicken seartchState;
	public IdleStateChicken idleState;
	public float berryFills=20;
	public float timeBetweenChecks = 1.5f;
	public float timeForEating = 1.5f;
	private bool checkedBush, food, idle, checking, isEating;
	public override StateChicken RunCurrentState()
	{
		if (!checkedBush)
		{
			CheckBush();
		}
		else if (food)
		{
			food = false;
			return seartchState;
		}
		else if (idle)
		{
			cm.isEating = false;
			cm.isPooping = false;
			idle = false;
			return idleState;
		}
		return this;
	}
	public void CheckBush()
	{
		checkedBush = true;
		if (!checking)
		{
			StartCoroutine("CheckedBush");
		}
		if (cm.lastBush.transform.GetComponent<BushScript>().berryAmount <= 0||cm.food>=100)
		{
			if (cm.food <= cm.minFood)
			{
				food = true;
			}
			else if (cm.food > cm.minFood)
			{
				idle = true;
			}
		}
		else
		{
			if (!isEating)
			{
				StartCoroutine("Eat");
			}
		}

	}
	private IEnumerator Eat()
	{
		isEating = true;
		StartCoroutine("CheckedBush");
		if (cm.lastBush.transform.GetComponent<BushScript>().berryAmount > 0)
		{
			cm.lastBush.transform.GetComponent<BushScript>().berryAmount--;
			cm.food += berryFills;
		}
		yield return new WaitForSeconds(timeForEating);
		isEating = false;
		checkedBush = false;
	}
	private IEnumerator CheckedBush()
	{
		checking = true;
		cm.RaycastSweep();
		yield return new WaitForSeconds(timeBetweenChecks);
		checking = false;
	}
}