using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 20.0f;
    public float pushStrength = 20.0f;

    public float massModifier = 1;

    public float initialScale = 1;
    public float initialMass = 1;

    public int kills = 0;
    public bool isDead = false;

    public CubeController pushedBy = null;
    private Rigidbody cubeRB;
    private PushTriggerScript pushCollider;

    public List<Material> cubeMaterials;
    public int currMat = 0;

    private MeshRenderer cubeRenderer;

    public GameManager gameManager;
    public int id;


    // Start is called before the first frame update
    void Start()
    {
        cubeRB = GetComponent<Rigidbody>();

        pushCollider = gameObject.transform.Find("PushTrigger").gameObject.GetComponent<PushTriggerScript>();

        cubeRenderer = GetComponent<MeshRenderer>();
        cubeRenderer.material = cubeMaterials[currMat];

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.AddToList(gameObject);

        SetInitialScaleMass(initialScale, initialMass);
    }

    // Update is called once per frame
    private void Update()
    {
       DeathCheck();
    }

    public void moveCube(Vector3 moveDirection)
    {
        if (cubeRB != null)
        {
            cubeRB.AddForce(moveDirection * speed * cubeRB.mass);
        }        
    }

    public void rotateCube(Vector3 moveDirection)
    {
        if (moveDirection.magnitude == 0)
        {
            transform.rotation = transform.rotation;
        }
        else
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotationSpeed);
            //cubeRB.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotationSpeed));

            Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotationSpeed);
            cubeRB.MoveRotation(new Quaternion( 0, rotation.y, 0, rotation.w));

            //cubeRB.AddRelativeTorque(rotation.eulerAngles * 100, ForceMode.Acceleration);
        }

    }

    public void Push(float pushForce, Vector3 pushDirection, CubeController cubeController)
    {
        cubeRB.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        pushedBy = cubeController;
        StartCoroutine(PushedByDuration());
    }

    public void DeathCheck()
    {
        if(transform.position.y < -5)
        {
            if (pushedBy != null)
            {
                pushedBy.AddKill(1);
            }
            isDead = true;     
            gameManager.RemoveFromList(gameObject);
            Destroy(gameObject);
        }
    }

    public void AddKill(int val)
    {
        kills += val;
        CubeUpdateOnKill();
    }

    public void CubeUpdateOnKill()
    {
        // change size, mass, material on kill
        UpdateSize();
        UpdateMass();
        UpdateMaterial();
        // update leaderboard entry on kill
        gameManager.UpdateLeaderboardItem(id, kills);
    }

    public void UpdateSize()
    {
        transform.localScale = new Vector3(initialScale + kills, initialScale + kills, initialScale + kills);
    }
    
    public void UpdateMass()
    {
        if(cubeRB)
        {
            cubeRB.mass = initialMass + (transform.localScale.x * massModifier);
        }       
    }

    public void UpdateMaterial()
    {
        cubeRenderer.material = GetNextMat();
    }

    public Material GetNextMat()
    {
        currMat = (currMat + 1) % cubeMaterials.Count;
        return cubeMaterials[currMat];
    }

    private void OnCollisionEnter(Collision collision)
    {
        //bool isFront = Physics.Raycast(transform.position, new Vector3(transform.forward.x, 0.25f, transform.forward.z), 1f);
        bool isFront = false;

        if (pushCollider != null)
        {
            isFront= pushCollider.inPushRange;
        }      

        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) && isFront)
        {
            //Debug.Log(isFront);
            Vector3 awayFromCube = (collision.gameObject.transform.position - transform.position);
            collision.gameObject.GetComponent<CubeController>().Push(pushStrength, awayFromCube, this);
        }
    }

    IEnumerator PushedByDuration()
    {
        yield return new WaitForSeconds(8);
        pushedBy = null;
    }

    public void SetInitialScale(float val)
    {
        initialScale = val;
    }
    public void SetInitialMass(float val)
    {
        initialMass = val;
    }

    public void SetInitialScaleMass(float scale, float mass)
    {
        SetInitialScale(scale);
        SetInitialMass(mass);

        UpdateSize();
        UpdateMass();
    }

}
