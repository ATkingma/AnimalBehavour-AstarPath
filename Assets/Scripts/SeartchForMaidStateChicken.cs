using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeartchForMaidStateChicken : StateChicken
{
	public GameObject chicken;
	public AStar astar;
	public float wandertime=5;
	public SeartchForFoodStateChicken seartchFood;
	public IdleStateChicken idle;
	public BreedingStateChicken breeding;
	public float rotateCoolDown=10;
	public float smooth = 5.0f;
	public float minBreedingRange=2;
	public float timeToFindMaid=40;
	public float increesTimeToFind=20;
	private bool didntFound, randomTarget;
	private float timer;
	private int randomNumb;
	public override StateChicken RunCurrentState()
	{
		if (chicken == null || astar == null)
		{
			chicken = cm.transform.gameObject;
			astar = FindObjectOfType<AStar>();
		}
		TimeToSeatchMaid();
		Seartch();
		if (cm.maidIsClose)
		{
			ResetValues();
			cm.isBreeding = true;
			return breeding;
		}
		else if (didntFound)
		{
			ResetValues();
			return idle;
		}
		else if (cm.food < cm.minFood / 2)
		{
			ResetValues();
			return seartchFood;
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
			if (cm.hasmaid)
			{
				cm.otherChicken = null;
			}
			cm.transform.LookAt(cm.otherChicken.transform.position);

			chicken.GetComponent<AI>().target = cm.otherChicken.gameObject;
		}
		else//seartch
		{
			cm.RaycastSweep();
			StartCoroutine("NewRandomTarget");
		}
	}
	public void DidntFindMaid()
	{
		timeToFindMaid += increesTimeToFind;
		didntFound = true;
	}
	public void ResetValues()
	{
		didntFound = false;
		timer = 0;
	}
	private IEnumerator NewRandomTarget()
	{
		randomTarget = true;
		randomNumb = Random.Range(0, astar.tilesToCheck.Count);
		chicken.GetComponent<AI>().target = astar.tilesToCheck[randomNumb].gameObject;
		yield return new WaitForSeconds(wandertime);
		randomTarget = false;
	}
}