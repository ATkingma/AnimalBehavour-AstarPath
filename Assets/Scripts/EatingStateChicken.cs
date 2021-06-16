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
		print("4");
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
				print("1");
				StartCoroutine("Eat");
			}
		}
	}
	private IEnumerator Eat()
	{
		isEating = true;
		cm.food += berryFills;
		print("2");
		yield return new WaitForSeconds(timeForEating);
		print("3");
		isEating = false;
	}
}