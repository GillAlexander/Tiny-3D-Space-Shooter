using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool playerHasControll = false;
    public bool gameIsPaused = false; // CHANGE
    public float PLAYERDELAYVALUE = 0;
    private Player player = null;
    private Vector3 idlePosition = Vector3.zero;
    private Vector3 playerPos = Vector3.zero;

    private FiringMechanics firingMechanics = null;

    //[SerializeField] private float playerRotationThreshHold = 1f;
    public AudioSource laserSound = null;

    private int rotationY = 0;
    public AnimationCurve spinCurve;

    public void EnablePlayerControll() => playerHasControll = true;
    public void DisablePlayerControll() => playerHasControll = false;

    public Rigidbody playerRigidbody = null;

    void Start()
    {
        player = GetComponent<Player>();
        firingMechanics = GetComponent<FiringMechanics>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        idlePosition = Camera.main.transform.position + Vector3.forward * 25;
        playerPos = player.transform.position;

        var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (playerHasControll)
        {
            player.transform.position = Vector3.Lerp(playerPos, new Vector3(cursorPosition.x,
                                                                            cursorPosition.y + 2.5f,
                                                                            cursorPosition.z) + Vector3.forward * 25, Time.deltaTime * PLAYERDELAYVALUE);
        }
        else
            player.transform.position = Vector3.Lerp(playerPos, idlePosition, Time.deltaTime * PLAYERDELAYVALUE);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Spin");
            StartCoroutine(SpinPlayer());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHasControll = !playerHasControll;
        }

        firingMechanics.Shoot(playerHasControll);
    }

    private IEnumerator SpinPlayer()
    {
        rotationY++;
        var newRotation = Quaternion.Euler(0, player.transform.rotation.y + rotationY, 0);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, newRotation, spinCurve.Evaluate(Time.deltaTime));
        yield return null;
    }
}

//var cursorPositionModified = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
//                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
//                                         0);

//var distanceToCursor = Vector3.Distance(player.transform.position, cursorPositionModified);
//if (distanceToCursor > playerRotationThreshHold)
//{
//    var direction = (player.transform.position - cursorPositionModified).normalized;
//    var dotDirection = Vector3.Dot(direction, Vector3.right);
//    Quaternion newRotation = new Quaternion();
//    if (dotDirection > 0) // Left
//    {
//        newRotation = Quaternion.Euler(0, -direction.y * 600 * distanceToCursor, 0);
//    }
//    else if (dotDirection < 0) // Right
//    {
//        newRotation = Quaternion.Euler(0, direction.y * 600 * distanceToCursor, 0);
//    }

//    player.transform.rotation = Quaternion.Lerp(player.transform.rotation, newRotation, Time.deltaTime * 5);
//}
//else
//{
//    player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 3);
//}