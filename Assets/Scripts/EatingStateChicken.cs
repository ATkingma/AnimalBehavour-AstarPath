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
	private bool  food, idle, isEating;
	public override StateChicken RunCurrentState()
	{
		Check();
		if (food)
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
	public void Check()
	{
		if (cm.food>=100)
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
		cm.food += berryFills;
		yield return new WaitForSeconds(timeForEating);
		isEating = false;
	}
}