using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeartchForMaidStateChicken : StateChicken
{
	public IdleStateChicken idle;
	public BreedingStateChicken breeding;
	public float rotateCoolDown=10;
	public float smooth = 5.0f;
	public float minBreedingRange=2;
	public float timeToFindMaid=40;
	public float increesTimeToFind=20;
	private bool canRotate, inBreedingRange, didntFound;
	private float rotateAmount;
	private float timer;
	private Quaternion target;
	public override StateChicken RunCurrentState()
	{
		TimeToSeatchMaid();
		Seartch();
		if (cm.maidIsClose&& inBreedingRange)
		{
			ResetValues();
			return breeding;
		}
		else if (didntFound)
		{
			ResetValues();
			return idle;
		}
		return this;
	}
	public void TimeToSeatchMaid()
	{
		if (!didntFound)
		{
			timer += Time.deltaTime;
			if (timer >= timeToFindMaid)
			{
				DidntFindMaid();
			}
		}
	}
	public void Seartch()
	{
		if (cm.otherChicken != null)
		{
			cm.transform.LookAt(cm.otherChicken.transform.position);

			cm.otherChicken.otherChicken = cm;
			float distance = Vector3.Distance(cm.transform.position, cm.otherChicken.transform.position);
			if (distance > minBreedingRange)
			{
				Wander();
			}
			else
			{
				inBreedingRange = true;
			}
		}
		else
		{
			cm.RaycastSweep();
			Wander();
			Rotate();
			if (canRotate)
			{
				StartCoroutine("NewRotate");
			}
		}
	}
	public void Wander()
	{
		if (cm.Fhit.transform != null)
		{
			if (cm.Fhit.transform.tag == "OuterBorder")
			{
				rotateAmount += 180;
				return;
			}
		}
		rb.velocity = -cm.transform.forward * speed / 2;
	}
	private IEnumerator NewRotate()
	{
		canRotate = false;
		rotateAmount = Random.Range(0.00f, 360.00f);
		target = Quaternion.Euler(0, rotateAmount, 0);
		yield return new WaitForSeconds(rotateCoolDown);
		canRotate = true;
	}
	public void Rotate()
	{
		cm.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
	}
	public void DidntFindMaid()
	{
		timeToFindMaid += increesTimeToFind;
		didntFound = true;
	}
	public void ResetValues()
	{
		canRotate = true;
		didntFound = false;
		inBreedingRange = false;
		timer = 0;
		rotateAmount = 0;
	}
}