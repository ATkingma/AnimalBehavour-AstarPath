using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorninesScript : MonoBehaviour
{
	public ChickenManager cm;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Chicken")
		{
			if (other.gameObject != cm.gameObject)
			{
				Add();
			}
		}
	}
	public void Add()
	{
		cm.horneynis += 2.5f;
	}
}