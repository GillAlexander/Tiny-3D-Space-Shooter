using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CONTROLLTHEPLAYER = false;
    public bool DELAYPLAYERMOVEMENT = false;
    public float PLAYERDELAYVALUE = 0;
    [SerializeField] private float fireCooldown = 0.25f;
    private float timer = 0;
    private Player player = null;

    [SerializeField] private float playerRotationThreshHold = 1f;
    public AudioSource laserSound = null;

    private int rotationY = 0;
    public AnimationCurve spinCurve;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        //player.transform.position = Vector3.Lerp(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 25, Time.deltaTime * 3);
        var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var cursorPositionModified = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                 Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                                 0);
        if (CONTROLLTHEPLAYER)
        {
            if (DELAYPLAYERMOVEMENT)
                player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(cursorPosition.x,
                                                                                                                                    cursorPosition.y + 2.5f,
                                                                                                                                    cursorPosition.z) + Vector3.forward * 25, Time.deltaTime * PLAYERDELAYVALUE);
                else
                player.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
                                                                                                                             Input.mousePosition.y + 2.5f, 
                                                                                                                             Input.mousePosition.z)) + Vector3.forward * 25;
        }
        else
            player.transform.position = Camera.main.transform.position + Vector3.forward * 25;

        var distanceToCursor = Vector3.Distance(player.transform.position, cursorPositionModified);

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

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Spin");
            StartCoroutine(SpinPlayer());

        }

        //if (Input.GetMouseButtonDown(1))
        //{
        if (timer >= fireCooldown) // Implement press to fire option
        {
            player.Fire();
            laserSound.Play();
            timer = 0;
        }
        //}
    }

    private IEnumerator SpinPlayer()
    {
        rotationY++;
        var newRotation = Quaternion.Euler(0, player.transform.rotation.y + rotationY, 0);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, newRotation, spinCurve.Evaluate(Time.deltaTime));
        yield return null;
    }
}
