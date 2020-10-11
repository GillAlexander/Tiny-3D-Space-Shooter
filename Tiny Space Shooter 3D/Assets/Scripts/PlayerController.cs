using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Player player = null;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        //player.transform.position = Vector3.Lerp(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 25, Time.deltaTime * 3);
        player.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 25;


        if (Input.GetMouseButtonDown(1))
        {
            player.Fire();
        }
    }
}
