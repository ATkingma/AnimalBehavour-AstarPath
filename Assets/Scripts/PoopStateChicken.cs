using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopStateChicken : StateChicken
{
	public IdleStateChicken idle;
	public GameObject poop;
	public float poopingTime=1.75f;
	public float TimeBeforePoop=4f;
	private bool isPooping;
	private float timer;
	public override StateChicken RunCurrentState()
	{
		if (cm.intestineAmount <= cm.maxIntestineAmount)
		{
			cm.isEating = false;
			cm.isPooping = false;
			return idle;
		}
		else
		{
			if (!isPooping)
			{
				MoveChicken();
			}
			return this;
		}
	}
	public void MoveChicken()
	{
		cm.RaycastSweep();
		if (cm.Fhit.transform != null)
		{
			if (cm.Fhit.transform.tag == "OuterBox")
			{
				cm.transform.LookAt(cm.Fhit.transform);
			}
		}
		rb.velocity = -cm.transform.forward * speed / 2;
		timer += Time.deltaTime;
		if (timer >= TimeBeforePoop)
		{
			Pooping();
		}
	}
	private void Pooping()
	{
		isPooping = true;
		Instantiate(poop, cm.transform.position, Quaternion.identity);
		cm.intestineAmount = 10;
		Invoke("StoppedPooping", poopingTime);
	}
	public void StoppedPooping()
	{
		isPooping = false;
		timer = 0;
	}
}