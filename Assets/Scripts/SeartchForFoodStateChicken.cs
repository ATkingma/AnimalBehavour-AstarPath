using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeartchForFoodStateChicken : StateChicken
{
	public EatingStateChicken eating;
	public float interactableDistance=3;
	private bool hasFood, canRotate;
	private float rotateAmount;
	private Quaternion target;
	public float rotateCoolDown = 5.5f;
	public float smooth = 5.0f;
	public override StateChicken RunCurrentState()
	{
		Searth();
		if (hasFood)
		{
			cm.isEating = true;
			return eating;
		}
		else
		{
			return this;
		}
	}
	public void Searth()
	{
		cm.RaycastSweep();
		if (cm.lastBush != null)
		{
			if (cm.lastBush.transform.tag == "BerryBush")
			{
				FoundBush(cm.lastBush.transform);
			}
			else if (cm.hit.transform.tag == "OuterBox")
			{
				cm.transform.LookAt(cm.hit.transform);
			}
		}
		else
		{
			hasFood = false;
			cm.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
			rb.velocity = -cm.transform.forward * speed / 2;
		}
	}
	public void FoundBush(Transform bushPos)
	{
		float distance = Vector3.Distance(cm.transform.position, bushPos.position);
		if (distance > interactableDistance)
		{
			cm.transform.LookAt(bushPos);
			rb.velocity = -cm.transform.forward * speed / 2;
		}
		else if (distance <= interactableDistance)
		{
			hasFood = true;
		}
	}
	private IEnumerator NewRotate()
	{
		canRotate = false;
		rotateAmount = Random.Range(0.00f, 360.00f);
		target = Quaternion.Euler(0, rotateAmount, 0);
		yield return new WaitForSeconds(rotateCoolDown);
		canRotate = true;
	}
}