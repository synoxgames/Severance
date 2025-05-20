using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeGenerator
{
    public float scale = 0.0f;
    public int size;
    public float cutoffPoint = 0.25f;
    public float[][] perlinNoise;
    public int[][] islandMap;

    public TypeGenerator(float scale, int size, float cutoffPoint) {
        this.scale = scale;
        this.size = size;
        this.cutoffPoint = cutoffPoint;
        perlinNoise = GeneratePerlinNoise(size);
        islandMap = ConvertToIslandMap(perlinNoise);
    }

    public void PrintNoise() {
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                Debug.Log($"X: {x}, Y: {y}, VALUE: {perlinNoise[x][y]}");
            }
        }
    }

    public void PrintIsland() {
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                Debug.Log($"X: {x}, Y: {y}, VALUE: {islandMap[x][y]}");
            }
        }
    }

    public void RenderImage(Renderer noiseSprite) {
        noiseSprite.material.mainTexture = GenerateImage();
    }

    public void RenderIslandImage(Renderer islandSprite) {
        islandSprite.material.mainTexture = GenerateIslandImage();
    }

    private Texture2D GenerateImage() {
        Texture2D noiseTexture = new Texture2D(size, size);
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                noiseTexture.SetPixel(x, y, new Color(perlinNoise[x][y], perlinNoise[x][y], perlinNoise[x][y]));
            }
        }

        noiseTexture.Apply();
        noiseTexture.filterMode = FilterMode.Point;
        return noiseTexture;
    }

    private Texture2D GenerateIslandImage() {
        Texture2D islandTexture = new Texture2D(size, size);
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (islandMap[x][y] == 1) islandTexture.SetPixel(x, y, Color.green);
                else islandTexture.SetPixel(x, y, Color.blue);
            }
        }

        islandTexture.Apply();
        islandTexture.filterMode = FilterMode.Point;
        return islandTexture;
    }

    public float [][] GeneratePerlinNoise(int size, int offset = 0) {
        float[][] map = new float[size][];

        for (int x = 0; x < size; x++) {

            map[x] = new float[size];

            for (int y = 0; y < size; y++) {
       
                float xCoor = (float)(x + offset) / size * scale;
                float yCoor = (float)(y + offset) / size * scale;

                float perlin = Mathf.PerlinNoise(xCoor, yCoor);
                map[x][y] = perlin <= cutoffPoint ? 1 : perlin;
            }
        }

        return map;
    }

    public int[][] ConvertToIslandMap(float[][] noiseMap) {
        int[][] newMap = new int[noiseMap.Length][];

        for (int x = 0; x < noiseMap.Length; x++) {

            newMap[x] = new int[noiseMap[x].Length];

            for (int y = 0; y < noiseMap.Length; y++) {
                if (noiseMap[x][y] == 1) {
                    newMap[x][y] = 0;
                } else {
                    newMap[x][y] = 1;
                }
            }
        }

        return newMap;
    }

    public static int CountDistinctIslands(int[][] grid) {
        int rows = grid.Length;
        if (rows == 0)
            return 0;

        int cols = grid[0].Length;
        if (cols == 0)
            return 0;

        HashSet<List<string>> coordinates = new HashSet<List<string>>();

        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < cols; ++j) {

                // If a cell is not 1 no need to dfs
                if (grid[i][j] != 1)
                    continue;

                // list to hold coordinates of this island
                List<string> v = new List<string>();
                Dfs(grid, i, j, i, j, v);

                // insert the coordinates for
                // this island to set
                coordinates.Add(v);
            }
        }

        return coordinates.Count - 1;
    }

    private static string ToString(float r, float c) {
        return r.ToString() + " " + c.ToString();
    }

    static int[][] dirs = { new int[] { 0, -1 },
                         new int[] { -1, 0 },
                         new int[] { 0, 1 },
                         new int[] { 1, 0 },
                         new int[] { 1, 1},
                         new int[] { 1, -1},
                         new int[] { -1, -1},
                         new int[] {  -1, 1}
    };

    private static void Dfs(int[][] grid, int x0, int y0, int i, int j, List<string> v) {

        int rows = grid.Length, cols = grid[0].Length;

        if (i < 0 || i >= rows || j < 0 || j >= cols || grid[i][j] <= 0)
            return;
        grid[i][j] *= -1;

        // repeat dfs for neighbors
        for (int k = 0; k < 8; k++) {
            Dfs(grid, x0, y0, i + dirs[k][0], j + dirs[k][1], v);
        }
    }

    public void SetNumberType(Number toSet) {
        int x = (int)toSet.GetPosition()[0];
        int y = (int)toSet.GetPosition()[1];
        if (perlinNoise[x][y] == 1) {
            Debug.Log("Normal Number");
            return;
        }
    }
}
