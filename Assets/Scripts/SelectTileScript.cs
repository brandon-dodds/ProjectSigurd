using UnityEngine;
using UnityEngine.Tilemaps;
public class SelectTileScript : MonoBehaviour
{
    private Grid grid;
    private Vector3Int previousMousePos = new Vector3Int();
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tile hoverTile = null;
    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int mousePos = getMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {

            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
            interactiveMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }
    }

    Vector3Int getMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
