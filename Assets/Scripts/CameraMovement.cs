using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    private float screenWidthBoundary;
    private float screenHeightBoundary;
    private int screenWidth;
    private int screenHeight;
    private Vector3 cameraPosition;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap pathMap;
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        cameraPosition = transform.position;
        screenHeightBoundary = screenHeight / 3.0f;
        screenWidthBoundary = screenWidth / 3.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.y > screenHeight - screenHeightBoundary)
        {
            float tempPositionY = cameraPosition.y;
            float speed = (Input.mousePosition.y - (screenHeight - screenHeightBoundary)) / (screenHeight - (screenHeight - screenHeightBoundary)) * 5;
            tempPositionY += speed * Time.deltaTime;
            if (MoveUp(tempPositionY) != null)
                cameraPosition.y = tempPositionY;
        }
        else if (Input.mousePosition.y < 0 + screenHeightBoundary)
        {
            float tempPositionY = cameraPosition.y;
            float speed = (Input.mousePosition.y - screenHeightBoundary) / (-screenHeightBoundary) * 5;
            tempPositionY -= speed * Time.deltaTime;
            if (MoveBottom(tempPositionY) != null)
                cameraPosition.y = tempPositionY;
        }
        if (Input.mousePosition.x > screenWidth - screenWidthBoundary)
        {
            float tempPositionX = cameraPosition.x;
            float speed = (Input.mousePosition.x - (screenWidth - screenWidthBoundary)) / (screenWidth - (screenWidth - screenWidthBoundary)) * 5;
            tempPositionX += speed * Time.deltaTime;
            if(MoveRight(tempPositionX) != null)
            {
                cameraPosition.x = tempPositionX;
            }
        }
        else if (Input.mousePosition.x < 0 + screenWidthBoundary)
        {
            float tempPositionX = cameraPosition.x;
            float speed = (Input.mousePosition.x - screenWidthBoundary) / (-screenWidthBoundary) * 5;
            tempPositionX -= speed * Time.deltaTime;
            if(MoveLeft(tempPositionX) != null)
            {
                cameraPosition.x = tempPositionX;
            }
        }
        transform.position = cameraPosition;
    }
    private Sprite MoveBottom(float down)
    {
        Vector3 cameraDown = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0 - down));
        Vector3Int bottomCoordinate = grid.WorldToCell(cameraDown);
        return pathMap.GetSprite(bottomCoordinate);
    }
    private Sprite MoveUp(float up)
    {
        Vector3 cameraUp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height + up));
        Vector3Int topCoordinate = grid.WorldToCell(cameraUp);
        return pathMap.GetSprite(topCoordinate);
    }
    private Sprite MoveRight(float right)
    {
        Vector3 cameraRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + right, Screen.height / 2));
        Vector3Int rightCoordinate = grid.WorldToCell(cameraRight);
        return pathMap.GetSprite(rightCoordinate);
    }
    private Sprite MoveLeft(float left)
    {
        Vector3 cameraLeft = Camera.main.ScreenToWorldPoint(new Vector3(0 - left, Screen.height / 2));
        Vector3Int leftCoordinate = grid.WorldToCell(cameraLeft);
        return pathMap.GetSprite(leftCoordinate);
    }
}
