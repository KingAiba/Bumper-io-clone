using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 offset;

    public float MinCamHeight = 15f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        offset = transform.position;
        Vector3 point = gameManager.GetCenter() + offset;

        transform.position = point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        offset.y = MinCamHeight + gameManager.GetGreatestDistance()/2;
        Vector3 point = gameManager.GetCenter() + offset;
        transform.position = point;
        //Debug.Log(GetGreatestDistance());
    }


}
