using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateChicken : MonoBehaviour
{
	public string stateName;
	[Space(3)]
	public ChickenManager cm;
	public abstract StateChicken RunCurrentState();
}