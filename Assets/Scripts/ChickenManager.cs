using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class ChickenManager : MonoBehaviour
{
	[Header("Player Values")]
	public GameObject chicken;
	public GameObject cam;
	public int health=10;
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
	public bool isBreeding;
	public bool maidIsClose;
	public bool hasmaid;
	[Header("reduce/add amount")]
	public float foodReduce = 0.45f;
	public float intestineAdd = 0.05f;
	public float horneynisAdd= 0.45f;
	[Header("Times")]
	public float damageCooldownTime=1.5f;
	public float timeToRecoverHealth=5f;
	public float timeBeforeDyingOfAge;
	[Header("raycast vars")]
	public LayerMask mapLayer;
	public float distanceForwardRay;
	public float distance = 20;
	public float theAngle = 25.0f;
	public float segments = 10.0f;
	public GameObject hit;
	public GameObject lastBush;
	public GameObject otherChicken;
	public Vector3 offSetStartPos = new Vector3(0, 0.5f,0);
	private RaycastHit Rhit;
	public RaycastHit Fhit;
	private Vector3 targetPos;
	private bool doingDamage, getHealth;
	private float timerForRecoverHealth;
	private float extraFoodReduce;	

	private void Start()
	{
		food = Random.Range(20, 100);
		foodReduce = Random.Range(0.1f, 5);
		extraFoodReduce = Random.Range(0.01f, 0.5f);
		intestineAmount = Random.Range(0, 70);
		intestineAdd = Random.Range(1, 5);
		horneynisAdd = Random.Range(1, 1.25f);
		timeBeforeDyingOfAge = Random.Range(300, 900);
		FindObjectOfType<CameraSwitcher>().cams.Add(cam);
		StartCoroutine("Age");
	}
	private void Update()
	{
		if (!isPooping || !isEating|| !isBreeding)
		{
			food -= foodReduce * ((Time.deltaTime / foodReduce)+ foodReduce * extraFoodReduce/minFood)/4;
			intestineAmount += intestineAdd * (Time.deltaTime / intestineAdd);
			horneynis += (Time.deltaTime / horneynisAdd)/ horneynisAdd;
		}
		if (food < 0)
		{
			if (!doingDamage)
			{
				StartCoroutine("DoDamage");
			}
		}
		else if (food > 100&&health<10)
		{
			timerForRecoverHealth += Time.deltaTime;
			if (timerForRecoverHealth >= timeToRecoverHealth)
			{
				StartCoroutine("GetHealth");
			}
		}
		if (health <= 0)
		{
			StartCoroutine("KillChicken");
		}
	}
	public IEnumerator GetHealth()
	{
		getHealth = true;
		health += 1;
		yield return new WaitForSeconds(0.5f);
		food -= 10;
		getHealth = false;
	}
	public IEnumerator DoDamage()
	{
		doingDamage = true;
		health-=1;
		yield return new WaitForSeconds(damageCooldownTime);
		doingDamage = false;
	}
	public IEnumerator Age()
	{
		yield return new WaitForSeconds(timeBeforeDyingOfAge/2);
		chicken.GetComponent<AI>().moveSpeed /= 1.5f;
		yield return new WaitForSeconds(timeBeforeDyingOfAge / 2);
		StartCoroutine("KillChicken");
	}
	public void CheckForward()
	{
		Physics.Raycast(transform.position,-transform.forward, out Fhit, distanceForwardRay, mapLayer);
	}
	public void RaycastSweep()
	{
		
		int startAngle = (int)(-theAngle * 0.5f); // half the angle to the Left of the forward
		int finishAngle = (int)(theAngle * 0.5f); // half the angle to the Right of the forward

		// the gap between each ray (increment)
		int inc = (int)(theAngle / segments);
		// step through and find each target point
		for (int i = startAngle; i < finishAngle; i += inc) // Angle from forward
		{
			targetPos = (Quaternion.Euler(0, i, 0) * -transform.forward).normalized * distance;
			
			// linecast between points
			if (Physics.Linecast(transform.position + offSetStartPos, targetPos, out Rhit))
			{
				hit = Rhit.transform.gameObject;
				if (Rhit.transform.tag == "BerryBush")
				{
					lastBush = Rhit.transform.gameObject;
				}
				else if (Rhit.transform.tag == "Chicken")
				{
					if (!Rhit.transform.GetComponent<ChickenManager>().hasmaid)
					{ 
						otherChicken = Rhit.transform.gameObject;
					}
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
	public IEnumerator KillChicken()
	{
		FindObjectOfType<CameraSwitcher>().cams.Remove(cam);
		yield return new WaitForSeconds(0.1f);
		Destroy(gameObject);
	}
}