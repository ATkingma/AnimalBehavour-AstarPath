using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Color red;
    public Color green;
	public Color brown;
	public Color startColor;
    public float dfStart;
    public float dfEnd;
    public float totalDis;
	public float checkHeight;
	public float obsticalDistance = 990099;
	public float terrainValue=110011;
	public AStar aStar;
    public GridTile prevTile;
    public List<GridTile> neighboring = new List<GridTile>();
    public bool check = false;
	private bool isRough;
	private void Start()
    {
        startColor = transform.GetComponent<MeshRenderer>().material.color;
    }
    void CheckForObstacle(Transform target, Transform start)
    {
        //reset everything.
        aStar = transform.parent.GetComponent<AStar>();
        check = false;
        transform.GetComponent<MeshRenderer>().material.color = startColor;
        prevTile = null;

        Collider[] colliders = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y + checkHeight * .5f, transform.position.z), new Vector3(aStar.tileSize * .5f, checkHeight, aStar.tileSize * .5f));
        
        for (int i = 0; i < colliders.Length; i++)
        {
			for (int _i = 0; _i < aStar.layersThatCountAsObstacle.Length; _i++)
			{
				if (colliders[i].gameObject.layer == LayerMask.NameToLayer(aStar.layersThatCountAsObstacle[_i]))
				{
					transform.GetComponent<MeshRenderer>().material.color = red;
					totalDis = obsticalDistance;
					dfEnd = obsticalDistance;
					dfStart = obsticalDistance;
					check = true;
				}
				else if (colliders[i].gameObject.layer == LayerMask.NameToLayer(aStar.layersThatCountAsRoughTerrain[_i]))
				{
					transform.GetComponent<MeshRenderer>().material.color = brown;
					isRough = true;
				}
				else if (colliders[i].transform == target)
				{
					if (aStar.endTile == null)
					{
						aStar.endTile = transform.gameObject;
					}
				}
				else if (colliders[i].transform == start)
				{
					if (aStar.startTile == null)
					{
						aStar.startTile = transform.gameObject;
						check = true;
					}
				}
			}
        }
    }
    public void CalculateDistances()
    {
		if (isRough)
		{
			dfStart = Vector3.Distance(transform.position, aStar.startTile.transform.position);
			dfEnd = Vector3.Distance(transform.position, aStar.endTile.transform.position);

			dfStart += terrainValue;
			dfEnd += terrainValue;

			totalDis = dfStart + dfEnd;
		}
        else if(totalDis < obsticalDistance)
        {
			if (aStar.startTile==null)
			{
				Debug.LogWarning("AI isnt on a tile");
				return;
			}
			else if (aStar.endTile == null)
			{
				Debug.LogWarning("Target isnt on a tile");
				return;
			}

            dfStart = Vector3.Distance(transform.position, aStar.startTile.transform.position);
            dfEnd = Vector3.Distance(transform.position, aStar.endTile.transform.position);
            totalDis = dfStart + dfEnd;
        }
    }
    public void UpdateGrid(Transform target, Transform start)
    {
        CheckForObstacle(target, start);
    }
    void UpdateNeighboring()
    {
        neighboring = new List<GridTile>();

        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(aStar.tileSize * .7f, 1, aStar.tileSize * .7f));
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.layer == LayerMask.NameToLayer("Tile") && colliders[i].transform != transform)
            {
                neighboring.Add(colliders[i].GetComponent<GridTile>());
            }
        }
    }
    public void FindLowestdfEnd()
    {
        UpdateNeighboring();
        neighboring.Sort((x, y) => x.dfEnd.CompareTo(y.dfEnd));

		List<GridTile> tempTiles = new List<GridTile>();

        int amount = 0;
		for (int i = 0; i < neighboring.Count; i++)
		{
			if (!neighboring[i].check && amount < 3)
			{
				neighboring[i].prevTile = transform.GetComponent<GridTile>();
				neighboring[i].check = true;
				aStar.tilesToCheck.Add(neighboring[i]);
				amount += 1;
			}
		}
    }
    public void MakePath()
    {
        transform.GetComponent<MeshRenderer>().material.color = green;
        aStar.path.Add(transform.GetComponent<GridTile>());
        if(prevTile != null)
        {
            prevTile.MakePath();
        }
    }
}