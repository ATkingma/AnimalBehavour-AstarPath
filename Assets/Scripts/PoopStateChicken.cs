using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoopStateChicken : StateChicken
{
	public GameObject chicken;
	public AStar astar;
	public IdleStateChicken idle;
	public GameObject poop;
	public float minInstestine=40;
	public float plusHunger=15;
	public float poopingTime=1.75f;
	public float newTargetCooldown = 3.5f;
	public float TimeBeforePoop=4f;
	private float timer;
	private bool randomTarget, isPooping;
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

		timer += Time.deltaTime;
		if (timer >= TimeBeforePoop)
		{
			Pooping();
		}
		if (!randomTarget)
		{
			StartCoroutine("NewRandomTarget");
		}
	}
	private IEnumerator NewRandomTarget()
	{
		int randomNumb = Random.Range(0, astar.tilesToCheck.Count);
		chicken.GetComponent<AI>().target = astar.tilesToCheck[randomNumb].gameObject;
		randomTarget = true;
		yield return new WaitForSeconds(newTargetCooldown);
		randomTarget = false;
	}
	private void Pooping()
	{
		isPooping = true;
		Instantiate(poop, cm.transform.position, Quaternion.identity);
		cm.intestineAmount -= minInstestine;
		cm.food -= plusHunger;
		Invoke("StoppedPooping", poopingTime);
	}
	public void StoppedPooping()
	{
		isPooping = false;
		timer = 0;
	}
}