using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 moveDirection;

    private CubeController playerCubeController;

    private Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerCubeController = GetComponent<CubeController>();
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        rotateCube();
    }

    private void FixedUpdate()
    {
        moveCube();
    }

    private void getInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    private void rotateCube()
    {
        playerCubeController.rotateCube(moveDirection);
    }

    private void moveCube()
    {
        playerCubeController.moveCube(moveDirection);
    }

}
