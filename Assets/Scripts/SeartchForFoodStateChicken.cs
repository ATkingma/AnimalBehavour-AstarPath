using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeartchForFoodStateChicken : StateChicken
{
	public AStar astar;
	public GameObject chicken;
	public EatingStateChicken eating;
	public float interactableDistance=3;
	public float newTargetCooldown = 2.5f;
	private bool hasFood;
	public float minRangeToEat=2;
	public float rotateCoolDown = 5.5f;
	public float smooth = 5.0f;
	private bool randomTarget, hasBush;
	public override StateChicken RunCurrentState()
	{
		Searth();
		if (hasBush)
		{
			CheckDistance();
		}
		if (hasFood)
		{
			cm.isEating = true;
			hasBush = false;
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
				FoundBush(cm.lastBush.GetComponent<BushScript>().tile);
			}
		}
		else
		{
			Wander();
		}
	}
	public void CheckDistance()
	{
		float dist = Vector3.Distance(chicken.transform.position, cm.lastBush.transform.position);
		if(dist<= minRangeToEat)
		{
			hasFood = true;
		}
	}
	public void FoundBush(GameObject bushPos)
	{
		chicken.GetComponent<AI>().target = bushPos;
		hasBush = true;
	}
	public void Wander()
	{
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
}