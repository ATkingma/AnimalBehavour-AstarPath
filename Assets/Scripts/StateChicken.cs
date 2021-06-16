using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateChicken : MonoBehaviour
{
	public string stateName;
	[Space(3)]
	public ChickenManager cm;
	[Header("Movement")]
	public float moveSpeed=5;
	public float rotateSpeed=10;
	public bool hasActionToMove;
	public abstract StateChicken RunCurrentState();
}