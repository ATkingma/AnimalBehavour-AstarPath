using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoughtTerrainScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Chicken")
		{
			other.GetComponent<AI>().moveSpeed /= 2;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Chicken")
		{
			other.GetComponent<AI>().moveSpeed *= 2;
		}
	}
}