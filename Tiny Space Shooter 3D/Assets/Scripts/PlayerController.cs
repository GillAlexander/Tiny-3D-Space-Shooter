using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fireCooldown = 0.25f;
    private float timer = 0;
    private Player player = null;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        //player.transform.position = Vector3.Lerp(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 25, Time.deltaTime * 3);
        player.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 25;


        //if (Input.GetMouseButtonDown(1))
        //{
        if (timer >= fireCooldown) // Implement press to fire option
        {
            player.Fire();
            timer = 0;
        }
        //}
    }
}
