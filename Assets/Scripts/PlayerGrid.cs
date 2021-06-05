using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerGrid : MonoBehaviour
{
    [SerializeField] private Tilemap tile;
    // Start is called before the first frame update
    void Start()
    {
        SnapToGrid(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SnapToGrid(Vector3 playerLocation)
    {
        Vector3Int closestCell = tile.WorldToCell(playerLocation);
        Vector3 newPlayerLoc = tile.CellToWorld(closestCell);
        transform.position = new Vector3(newPlayerLoc.x + 0.5f, newPlayerLoc.y + 0.5f);
    }
}
