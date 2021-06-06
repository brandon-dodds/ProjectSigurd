using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private bool moving = false;
    [SerializeField] private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            Vector3 mouseCellPosition = tilemap.WorldToCell(mouseWorldPosition);
            if (mouseCellPosition == tilemap.WorldToCell(transform.position))
                moving = true;
        }
        else if (moving && Input.GetMouseButtonDown(0)) 
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            Vector3 mouseCellPosition = tilemap.WorldToCell(mouseWorldPosition);
            transform.position = new Vector3(mouseCellPosition.x + 0.5f, mouseCellPosition.y + 0.5f);
            moving = false;
        }
    }
}
