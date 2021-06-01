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
    private enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }
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
         * the speed is then added to the position * time taken for the frame to pass.
         */
        float initialPositionY = cameraPosition.y;
        float initialPositionX = cameraPosition.x;
        if (Input.mousePosition.y > screenHeight - screenHeightBoundary)
        {
            float speed = (Input.mousePosition.y - (screenHeight - screenHeightBoundary))
                / (screenHeight - (screenHeight - screenHeightBoundary)) * speedConst;
            float finalPosition = initialPositionY + (speed * Time.deltaTime);
            if(GetSpriteFromDirectionAndPixels(Directions.Up, CalculateDifferenceInPixelSpace(initialPositionY, finalPosition)) != null)
            {
                cameraPosition.y = finalPosition;
            }
        }
        else if (Input.mousePosition.y < 0 + screenHeightBoundary)
        {
            float speed = (Input.mousePosition.y - screenHeightBoundary) / (-screenHeightBoundary) * speedConst;
            float finalPosition = initialPositionY - (speed * Time.deltaTime);
            if (GetSpriteFromDirectionAndPixels(Directions.Down, CalculateDifferenceInPixelSpace(initialPositionY, finalPosition)))
            {
                cameraPosition.y = finalPosition;
            }
        }
        if (Input.mousePosition.x > screenWidth - screenWidthBoundary)
        {
            float speed = (Input.mousePosition.x - (screenWidth - screenWidthBoundary)) 
                / (screenWidth - (screenWidth - screenWidthBoundary)) * speedConst;
            float finalPosition = initialPositionX + (speed * Time.deltaTime);
            if (GetSpriteFromDirectionAndPixels(Directions.Right, CalculateDifferenceInPixelSpace(initialPositionX, finalPosition)) != null)
            {
                cameraPosition.x = finalPosition;
            }

        }
        else if (Input.mousePosition.x < 0 + screenWidthBoundary && GetSpriteFromDirectionAndPixels(Directions.Left) != null)
        {
            float speed = (Input.mousePosition.x - screenWidthBoundary) / (-screenWidthBoundary) * speedConst;
            float finalPosition = initialPositionX - (speed * Time.deltaTime);
            if (GetSpriteFromDirectionAndPixels(Directions.Left, CalculateDifferenceInPixelSpace(initialPositionX, finalPosition)) != null)
            {
                cameraPosition.x = finalPosition;
            }
        }
        transform.position = cameraPosition;
    }
    /// <summary>
    /// The function takes the screen to world point of the camera position. 
    /// Finds the grid at that position and returns the sprite at that location.
    /// </summary>
    /// <returns>Returns the sprite of the block.</returns>
    private Sprite GetSpriteFromDirectionAndPixels(Directions direction, float x = 1)
    {
        Vector3 usedScreenPoint = default;
        switch (direction)
        {
            case Directions.Up:
                usedScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height + x));
                break;
            case Directions.Down:
                usedScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, -x));
                break;
            case Directions.Left:
                usedScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(-x, Screen.height / 2));
                break;
            case Directions.Right:
                usedScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + x, Screen.height / 2));
                break;
        }
        Vector3Int cellCoordinate = grid.WorldToCell(usedScreenPoint);
        return pathMap.GetSprite(cellCoordinate);
    }
    private float CalculateDifferenceInPixelSpace(float initialPosition, float finalPosition)
    {
        Vector3 beforeMoveScreenPos = Camera.main.WorldToScreenPoint(new Vector3(cameraPosition.x, initialPosition));
        Vector3 afterMoveScreenPos = Camera.main.WorldToScreenPoint(new Vector3(cameraPosition.x, finalPosition));
        return Mathf.Abs(afterMoveScreenPos.y - beforeMoveScreenPos.y);
    }
}
