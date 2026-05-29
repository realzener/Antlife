using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField] private MapGenerator generator;

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mouseScreen = Mouse.current.position.ReadValue();
            mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z);

            Vector3 world = Camera.main.ScreenToWorldPoint(mouseScreen);

            Vector2Int tile = new Vector2Int(
                Mathf.FloorToInt(world.x),
                Mathf.FloorToInt(world.y)
            );

            Debug.Log(tile);

            if (generator == null)
            {
                Debug.LogError("MapGenerator missing!");
                return;
            }

            generator.BuildTile(tile.x, tile.y, TileType.Grass);
        }
    }
}