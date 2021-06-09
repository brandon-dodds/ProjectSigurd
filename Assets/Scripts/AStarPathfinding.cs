using UnityEngine;
using UnityEngine.Tilemaps;
public class AStarPathfinding : MonoBehaviour
{
    [SerializeField] private Tilemap pathMap;
    [SerializeField] private Tilemap colourMap;
    private Vector3Int playerCellLocation;
    // Start is called before the first frame update
    void Start()
    {
        playerCellLocation = pathMap.WorldToCell(transform.position);
        Debug.Log(playerCellLocation.x);
        Debug.Log(playerCellLocation.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int mouseCellLocation = colourMap.WorldToCell(
            Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y)));
        colourMap.SetColor(mouseCellLocation, Color.black);

        AStar(playerCellLocation, mouseCellLocation);
    }

    void AStar(Vector3Int playerCellLocation, Vector3Int mouseCellLocation)
    {

    }
}
