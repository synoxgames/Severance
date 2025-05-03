using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeGenerator
{
    public float scale = 0.0f;
    public int size;
    public float[,] perlinNoise;

    public TypeGenerator(float scale, int size) {
        this.scale = scale;
        this.size = size;
        perlinNoise = GeneratePerlinNoise(size);
    }

    public void PrintNoise() {
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                Debug.Log($"X: {x}, Y: {y}, VALUE: {perlinNoise[x, y]}");
            }
        }
    }

    public void RenderImage(Renderer noiseSprite) {
        noiseSprite.material.mainTexture = GenerateImage();
    }

    private Texture2D GenerateImage() {
        Texture2D noiseTexture = new Texture2D(size, size);
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                noiseTexture.SetPixel(x, y, new Color(perlinNoise[x, y], perlinNoise[x, y], perlinNoise[x, y]));
            }
        }

        noiseTexture.Apply();
        return noiseTexture;
    }
   
    public float [,] GeneratePerlinNoise(int size) {
        float[,] map = new float[size,size];

        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
        
                float xCoor = (float)x / size * scale;
                float yCoor = (float)y / size * scale;

                map[x,y] = (1 - Mathf.PerlinNoise(xCoor, yCoor));
            }
        }

        return map;
    }

    public string GetEmotion(int x, int y) {

        Debug.Log($"{perlinNoise[x,y]}");

        return "";
    }
}
