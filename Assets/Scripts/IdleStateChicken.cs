using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateChicken : StateChicken
{
	public PoopStateChicken poop;
	public SeartchForFoodStateChicken seartchFood;
	public SeartchForMaidStateChicken sMaid;
	public float rotateCoolDown = 5.5f;
	public float smooth = 5.0f;
	private bool canRotate=true;
	private bool DontWander;
	private float rotateAmount;
	private Quaternion target;
	public override StateChicken RunCurrentState()
	{
		if (cm.food <= cm.minFood)
		{
			cm.lastBush = null;
			return seartchFood;
		}
		else if (cm.intestineAmount >= cm.maxIntestineAmount)
		{
			cm.isPooping = true;
			return poop;
		}
		else if(cm.horneynis>= cm.horneynisMin)
		{
			cm.horneynis = 0;
			return sMaid;
		}
		if (!DontWander)
		{
			Wander();
		}
		if (canRotate)
		{
			StartCoroutine("NewRotate");
		}
		Rotate();
		return this;
	}
	public void Wander()
	{
		cm.RaycastSweep();
		cm.CheckForward();
		if (cm.Fhit.transform != null)
		{
			if (cm.Fhit.transform.tag == "OuterBorder")
			{
				DontWander = true;
				rotateAmount += 180;
				return;
			}
		}
		rb.velocity = -cm.transform.forward * speed/2;
	}
	private IEnumerator NewRotate()
	{
		canRotate = false;
		DontWander = !DontWander;
		rotateAmount = Random.Range(0.00f, 360.00f);
		target = Quaternion.Euler(0, rotateAmount, 0);
		yield return new WaitForSeconds(rotateCoolDown);
		canRotate = true;
	}
	public void Rotate()
	{
		cm.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
	}
}