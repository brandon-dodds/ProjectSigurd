using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    /*
     * Camera initialization. ScreenWidth and Height are calculated on Frame 0. and are the given screen height and width.
     * screenWidthBoundary and screenHeightBoundary are the thirds of the screen needed to calculate when to activate the camera.
     * The cameraposition variable is transform.position (the position of the attached object).
     * [SerializeField] allows attachment of fields to the object so I can access a grid and tilemap.
     * grid is the main grid and pathMap is the tilemap of the path objects.
     * speedConst is the constant of speed used to calculate the velocity of objects.
     */
    private float screenWidthBoundary;
    private float screenHeightBoundary;
    private int screenWidth;
    private int screenHeight;
    private Vector3 cameraPosition;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap pathMap;
    private const int speedConst = 5;
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        cameraPosition = transform.position;
        screenHeightBoundary = screenHeight / 3.0f;
        screenWidthBoundary = screenWidth / 3.0f;
    }
    void Update()
    {
        /*
         * The maths explained:
         * If the mouse x position or y position is greater than the height or width - boundary (1920 * 1920/3) for example.
         * Then I grab the camera position before it is changed.
         * Speed is calculated by the normalisation algorithm here: https://www.codecademy.com/articles/normalization times a constant.
         * the speed is then added to the position * time taken for the frame to pass. (essentially v = u + at)
         */
        if (Input.mousePosition.y > screenHeight - screenHeightBoundary)
        {
            float tempPositionY = cameraPosition.y;
            float speed = (Input.mousePosition.y - (screenHeight - screenHeightBoundary))
                / (screenHeight - (screenHeight - screenHeightBoundary)) * speedConst;
            tempPositionY += speed * Time.deltaTime;
            if (MoveUp(tempPositionY) != null)
                cameraPosition.y = tempPositionY;
        }
        else if (Input.mousePosition.y < 0 + screenHeightBoundary)
        {
            float tempPositionY = cameraPosition.y;
            float speed = (Input.mousePosition.y - screenHeightBoundary) / (-screenHeightBoundary) * speedConst;
            tempPositionY -= speed * Time.deltaTime;
            if (MoveBottom(tempPositionY) != null)
                cameraPosition.y = tempPositionY;
        }
        if (Input.mousePosition.x > screenWidth - screenWidthBoundary)
        {
            float tempPositionX = cameraPosition.x;
            float speed = (Input.mousePosition.x - (screenWidth - screenWidthBoundary)) 
                / (screenWidth - (screenWidth - screenWidthBoundary)) * speedConst;
            tempPositionX += speed * Time.deltaTime;
            if(MoveRight(tempPositionX) != null)
            {
                cameraPosition.x = tempPositionX;
            }
        }
        else if (Input.mousePosition.x < 0 + screenWidthBoundary)
        {
            float tempPositionX = cameraPosition.x;
            float speed = (Input.mousePosition.x - screenWidthBoundary) / (-screenWidthBoundary) * speedConst;
            tempPositionX -= speed * Time.deltaTime;
            if(MoveLeft(tempPositionX) != null)
            {
                cameraPosition.x = tempPositionX;
            }
        }
        transform.position = cameraPosition;
    }
    /// <summary>
    /// The function takes the screen to world point of the camera position. 
    /// Finds the grid at that position and returns the sprite at that location.
    /// </summary>
    /// <param name="down">The float value of how far the camera moves downward.</param>
    /// <returns>Returns the sprite of the block.</returns>
    private Sprite MoveBottom(float down)
    {
        Vector3 cameraDown = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0 - down));
        Vector3Int bottomCoordinate = grid.WorldToCell(cameraDown);
        return pathMap.GetSprite(bottomCoordinate);
    }
    /// <summary>
    /// The function takes the screen to world point of the camera position. 
    /// Finds the grid at that position and returns the sprite at that location.
    /// </summary>
    /// <param name="up">The float value of how far the camera moves upward.</param>
    /// <returns>Returns the sprite of the block.</returns>
    private Sprite MoveUp(float up)
    {
        Vector3 cameraUp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height + up));
        Vector3Int topCoordinate = grid.WorldToCell(cameraUp);
        return pathMap.GetSprite(topCoordinate);
    }
    /// <summary>
    /// The function takes the screen to world point of the camera position. 
    /// Finds the grid at that position and returns the sprite at that location.
    /// </summary>
    /// <param name="right">The float value of how far the camera moves rightward.</param>
    /// <returns>Returns the sprite of the block.</returns>
    private Sprite MoveRight(float right)
    {
        Vector3 cameraRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + right, Screen.height / 2));
        Vector3Int rightCoordinate = grid.WorldToCell(cameraRight);
        return pathMap.GetSprite(rightCoordinate);
    }
    /// <summary>
    /// The function takes the screen to world point of the camera position. 
    /// Finds the grid at that position and returns the sprite at that location.
    /// </summary>
    /// <param name="left">The float value of how far the camera moves leftward.</param>
    /// <returns>Returns the sprite of the block.</returns>
    private Sprite MoveLeft(float left)
    {
        Vector3 cameraLeft = Camera.main.ScreenToWorldPoint(new Vector3(0 - left, Screen.height / 2));
        Vector3Int leftCoordinate = grid.WorldToCell(cameraLeft);
        return pathMap.GetSprite(leftCoordinate);
    }
}
