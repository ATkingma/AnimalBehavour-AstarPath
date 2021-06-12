using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Color red;
    public Color green;
    public Color startColor;
    public float dfStart;
    public float dfEnd;
    public float totalDis;
    public float checkHeight;
    public AStar aStar;
    public GridTile prevTile;
    public List<GridTile> neighboring = new List<GridTile>();
    public bool check = false;

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
            for (int i2 = 0; i2 < aStar.layersThatCountAsObstacle.Length; i2++)
            {
                if (colliders[i].gameObject.layer == LayerMask.NameToLayer(aStar.layersThatCountAsObstacle[i2]))
                {
                    transform.GetComponent<MeshRenderer>().material.color = red;
                    totalDis = 1000000;
                    dfEnd = 1000000;
                    dfStart = 1000000;
                    check = true;
                }
                else if(colliders[i].transform == target)
                {
                    if(aStar.endTile == null)
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
        if(totalDis < 1000000)
        {
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
