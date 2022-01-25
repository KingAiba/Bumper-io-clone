using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool rotationEnabled = true;
    public bool movementEnabled = true;

    private Vector3 lookDirection;

    private CubeController enemyControl;

    [SerializeField] 
    private GameObject target = null;

    private GameManager gameManager;

    
    // Start is called before the first frame update
    void Start()
    {
        enemyControl = GetComponent<CubeController>();
                
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
        rotateCube();
    }

    private void FixedUpdate()
    {
        moveCube();
    }

    private void LookAtTarget()
    {
        if(target == null)
        {
            target = enemyControl.gameManager.GetClosestTarget(gameObject);
        }
        else
        {
            lookDirection = (target.transform.position - transform.position).normalized;
        }
        
    }

    private void rotateCube()
    {
        if(rotationEnabled)
        {
            enemyControl.rotateCube(lookDirection);
        }
    }

    private void moveCube()
    {
        if(movementEnabled)
        {           
            enemyControl.moveCube(lookDirection);
        }       
    }

}
