using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateChicken : StateChicken
{
	public GameObject chicken;
	public AStar astar;
	public PoopStateChicken poop;
	public SeartchForFoodStateChicken seartchFood;
	public SeartchForMaidStateChicken sMaid;
	public float newTargetCooldown   = 5.5f;
	public float smooth = 5.0f;
	private bool randomTarget, justStarted=true, startfunction;
	private int randomNumb=0;
	public override StateChicken RunCurrentState()
	{
		if (cm.food <= cm.minFood)
		{
			return seartchFood;
		}
		if (cm.intestineAmount >= cm.maxIntestineAmount)
		{
			cm.isPooping = true;
			return poop;
		}
		else if (cm.horneynis >= cm.horneynisMin)
		{
			cm.horneynis = 0;
			return sMaid;
		}
		Wander();
		return this;
	}
	public void Wander()
	{
		if (justStarted&& !startfunction)
		{
			StartCoroutine("Started");
		}
		if (!randomTarget&&!justStarted)
		{
			StartCoroutine("NewRandomTarget");
		}
	}
	private IEnumerator Started()
	{
		startfunction = true;
		yield return new WaitForSeconds(0.15f);
		justStarted = false;
	}
	private IEnumerator NewRandomTarget()
	{
		randomTarget = true;
		randomNumb = Random.Range(0, astar.tilesToCheck.Count);
		if (astar.tilesToCheck == new List<GridTile>())
		{
			yield break;
		}
		else if (astar.tilesToCheck[randomNumb] == null)
		{
			randomNumb = Random.Range(0, astar.tilesToCheck.Count);
			if (astar.tilesToCheck[randomNumb] == null)
			{
				yield break;
			}
		}
		chicken.GetComponent<AI>().target = astar.tilesToCheck[randomNumb].gameObject;
		yield return new WaitForSeconds(newTargetCooldown);
		randomTarget = false;
	}
}