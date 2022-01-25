using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<GameObject> ActiveObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //ActiveObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        //ActiveObjects.Add(GameObject.FindGameObjectWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList(GameObject newTarget)
    {
        ActiveObjects.Add(newTarget);
    }

    public void RemoveFromList(GameObject target)
    {
        ActiveObjects.Remove(target);
    }

    public float GetGreatestDistance()
    {
        if(ActiveObjects.Count > 0)
        {
            Bounds bounds = new Bounds(ActiveObjects[0].transform.position, Vector3.zero);
            for (int i = 0; i < ActiveObjects.Count; i++)
            {
                bounds.Encapsulate(ActiveObjects[i].transform.position);
            }

            return bounds.size.x;
        }
        return 0f;
    }

    public GameObject GetClosestTarget(GameObject current)
    {
        GameObject closestObject = null;
        float closestDist = Mathf.Infinity;
        foreach(GameObject target in ActiveObjects)
        {
            if (current != target)
            {
                float check = Vector3.Distance(current.transform.position, target.transform.position);
                if (check < closestDist)
                {
                    closestObject = target;
                    closestDist = check;
                }
            }
        }

        return closestObject;
    }

    public Vector3 GetCenter()
    {
        if (ActiveObjects.Count == 1)
        {
            return ActiveObjects[0].transform.position;
        }
        else if (ActiveObjects.Count == 0)
        {
            return new Vector3(0, 9, 0);
        }

        float centerX = 0;
        float centerY = 0;
        float centerZ = 0;

        foreach (GameObject target in ActiveObjects)
        {
            centerX += target.transform.position.x;
            centerY += target.transform.position.y;
            centerZ += target.transform.position.z;
        }

        return new Vector3(centerX / ActiveObjects.Count, centerY / ActiveObjects.Count, centerZ / ActiveObjects.Count);
    }
}
