using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenManager : MonoBehaviour
{
	[Header("Player Values")]
	public GameObject chicken;
	[Header("UI")]
	public Image fillArea;
	public Text feelings;
	public Text progress;
	public Text state;
	public float feelingsValue=100;
	public float food = 100;
	public float horneynis = 0;
	public float intestineAmount = 10;
	[Header("minmalAmount")]
	public float minFood = 50f;
	public float maxIntestineAmount = 75f;
	public float horneynisMin=90;
	[Header("public bools")]
	public bool isEating;
	public bool isPooping;
	public bool maidIsClose;
	[Header("reduce/add amount")]
	public float foodReduce = 0.45f;
	public float intestineAdd = 0.05f;
	[Header("raycast vars")]
	public LayerMask mapLayer;
	public float distanceForwardRay;
	public float distance = 20;
	public float theAngle = 25.0f;
	public float segments = 10.0f;
	public GameObject hit;
	public GameObject lastBush;
	public ChickenManager otherChicken;
	public Vector3 offSetStartPos = new Vector3(0, 0.5f,0);
	private RaycastHit Rhit;
	public RaycastHit Fhit;
	private Vector3 targetPos;
	private void Update()
	{
		if (!isPooping || !isEating)
		{
			food -= foodReduce * (Time.deltaTime / foodReduce);
			intestineAmount += intestineAdd * (Time.deltaTime / intestineAdd);
		}
	}
	public void CheckForward()
	{
		Physics.Raycast(transform.position,-transform.forward, out Fhit, distanceForwardRay, mapLayer);
	}
	public void RaycastSweep()
	{
		
		int startAngle = Convert.ToInt32(-theAngle * 0.5f); // half the angle to the Left of the forward
		int finishAngle = Convert.ToInt32(theAngle * 0.5f); // half the angle to the Right of the forward

		// the gap between each ray (increment)
		int inc = Convert.ToInt32(theAngle / segments);
		// step through and find each target point
		for (int i = startAngle; i < finishAngle; i += inc) // Angle from forward
		{
			targetPos = (Quaternion.Euler(0, i, 0) * -transform.forward).normalized * distance;
			
			// linecast between points
			if (Physics.Linecast(-transform.position + offSetStartPos, targetPos, out Rhit))
			{
				hit = Rhit.transform.gameObject;
				if (Rhit.transform.tag == "BerryBush")
				{
					lastBush = Rhit.transform.gameObject;
				}
				else if (Rhit.transform.tag == "Chicken")
				{
					otherChicken = Rhit.transform.GetComponent<ChickenManager>();
				}
			}
			// to show ray just for testing
			Debug.DrawLine(transform.position + offSetStartPos, targetPos, Color.green);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == otherChicken)
		{
			maidIsClose = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == otherChicken)
		{
			maidIsClose = false;
		}
	}
}