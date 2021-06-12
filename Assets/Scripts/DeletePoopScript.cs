using UnityEngine;
public class DeletePoopScript : MonoBehaviour
{
	public float timeBeforeDestroy=7.5f;
	private void Start()
	{
		Invoke("DestroyPoop", timeBeforeDestroy);
	}
	public void DestroyPoop()
	{
		Destroy(transform.gameObject);
	}
}