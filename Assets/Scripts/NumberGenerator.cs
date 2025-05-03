using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    public int gridSize = 250;
    public int xSpacing = 10;
    public int ySpacing = 10;
    [Space]
    public GameObject[] numberGrid;
    public Sprite[] numberSprites;
    [Space]
    public string randomizer = "SEVERANCE";
    [Space]
    public Transform gridParent;
    public Vector3 gridOffset;

    [Header("Wiggle Settings")]
    public float minMoveSpeed = 5;
    public float maxMoveSpeed = 10;
    public float moveSpeed, radius = .5f;
    public TypeGenerator typeGenerator;
    [Space]
    public Renderer noiseSprite;

    public static NumberGenerator instance;

    public void Awake() {
        if (instance != this) {
            Destroy(instance);
        }

        instance = this;
    }

    public void Start() {
        numberGrid = GenerateGrid();
        gridOffset = new Vector3(-(gridSize / 2) * (xSpacing), -(gridSize / 2) * (ySpacing), 20);
        gridParent.position = gridOffset;
    }

    public GameObject[] GenerateGrid() {
        typeGenerator = new TypeGenerator(40f, gridSize);
        typeGenerator.RenderImage(noiseSprite);
        typeGenerator.PrintNoise();
        GameObject[] grid = new GameObject[gridSize * gridSize];
        Random.InitState(randomizer.GetHashCode());

        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                grid[x + y] = SpawnNumber(Random.Range(0, 9), x * xSpacing, y * ySpacing);
              //  typeGenerator.GetEmotion(x, y);
            }
        }

        return grid;
    }

    public GameObject SpawnNumber(int num, int x = 0, int y = 0) {
        GameObject number = new GameObject(num.ToString());
        number.transform.parent = gridParent;
        gridParent.parent.GetComponent<BoxCollider2D>().size = new Vector2(gridSize * xSpacing, gridSize * ySpacing);
        gridParent.parent.GetComponent<BoxCollider2D>().offset = new Vector2(-xSpacing / 2f, -ySpacing / 2f);

        number.transform.position = new Vector3(x, y, 0) + gridOffset;

        Number numberComp = number.AddComponent<Number>();
        numberComp.SetNumber(num);
        numberComp.SetPosition(x, y);

        SpriteRenderer renderer = number.AddComponent<SpriteRenderer>();
        renderer.sprite = numberSprites[num % numberSprites.Length];

        NumberWiggle wiggler = number.AddComponent<NumberWiggle>();
        wiggler.SetupWiggle(minMoveSpeed, maxMoveSpeed, radius);
        number.AddComponent<BoxCollider2D>();

        numberComp.Setup(wiggler, renderer, number.GetComponent<BoxCollider2D>());
       

        return number;
    }

}
