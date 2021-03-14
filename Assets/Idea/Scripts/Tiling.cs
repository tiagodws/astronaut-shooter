using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    [SerializeField] int offsetX = 2;
    [SerializeField] private bool hasTileableSprite = false;
    private bool hasRightTile = false;
    private bool hasLeftTile = false;
    private float spriteWidth = 0f;
    private Camera cameraComponent;

    void Awake() {
        cameraComponent = Camera.main;
    }

    void Start() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        spriteWidth = renderer.sprite.bounds.size.x;
    }

    void Update() {
        float camHorizontalExtend = cameraComponent.orthographicSize * Screen.width / Screen.height;

        if (!hasRightTile) {
            float edgeVisiblePositionRight = (transform.position.x + spriteWidth / 2) - camHorizontalExtend;
            bool shouldCreateRightTile = cameraComponent.transform.position.x >= edgeVisiblePositionRight - offsetX;

            if (shouldCreateRightTile) makeNewTile(Side.Right);
        }

        if (!hasLeftTile) {
            float edgeVisiblePositionLeft = (transform.position.x - spriteWidth / 2) + camHorizontalExtend;
            bool shouldCreateLeftTile = cameraComponent.transform.position.x <= edgeVisiblePositionLeft + offsetX;

            if (shouldCreateLeftTile) makeNewTile(Side.Left);
        }
    }

    private void makeNewTile(Side side) {
        Vector3 newPosition = new Vector3(transform.position.x + spriteWidth * ((int) side), transform.position.y, transform.position.z);
        Transform tile = Instantiate<Transform>(transform, newPosition, transform.rotation);

        if (!hasTileableSprite) {
            tile.localScale = new Vector3(-tile.localScale.x, tile.localScale.y, tile.localScale.z);
        }

        tile.parent = transform.parent;

        if (side == Side.Right) {
            hasRightTile = true;
            tile.GetComponent<Tiling>().hasLeftTile = true;
        } else if (side == Side.Left) {
            hasLeftTile = true;
            tile.GetComponent<Tiling>().hasRightTile = true;
        }
    }
}

enum Side {
    Left = -1,
    Right = 1
}