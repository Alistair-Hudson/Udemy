using UnityEditor;
using System;
using UnityEngine;
using System.IO;

public class TextureCreatorWindow : EditorWindow
{
    string filename = "NewProceduralTexture";
    float perlinXScale;
    float perlinYScale;
    int perlinOctaves;
    float perlinPersistance;
    float perlinHeightScale;
    int perlinOffSetX;
    int perlinOffsetY;
    bool alphaToggle = false;
    bool seamlessToggle = false;
    bool mapToggle = false;

    Texture2D pTexture;

    [MenuItem("Window/TextureCreatorWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TextureCreatorWindow));
    }

    private void OnEnable()
    {
        pTexture = new Texture2D(513, 513, TextureFormat.ARGB32, false);
    }

    private void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        filename = EditorGUILayout.TextField("Texture Name", filename);

        int wSize = (int)(EditorGUIUtility.currentViewWidth - 100);

        perlinXScale = EditorGUILayout.Slider("X Scale", perlinXScale, 0, 0.1f);
        perlinYScale = EditorGUILayout.Slider("Y Scale", perlinYScale, 0, 0.1f);
        perlinOctaves = EditorGUILayout.IntSlider("Ocatves", perlinOctaves, 1, 10);
        perlinPersistance = EditorGUILayout.Slider("Persistance", perlinPersistance, 1, 10);
        perlinHeightScale = EditorGUILayout.Slider("Height Scale", perlinHeightScale, 0, 1);
        perlinOffSetX = EditorGUILayout.IntSlider("Offset X", perlinOffSetX, 1, 10000);
        perlinOffsetY = EditorGUILayout.IntSlider("Offset Y", perlinOffsetY, 1, 10000);
        alphaToggle = EditorGUILayout.Toggle("Alpha?", alphaToggle);
        mapToggle = EditorGUILayout.Toggle("Map?", mapToggle);
        seamlessToggle = EditorGUILayout.Toggle("Seamless", seamlessToggle);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        float minColour = 1;
        float maxColour = 0;
        if (GUILayout.Button("Generate", GUILayout.Width(wSize)))
        {
            GenerateMap(minColour, maxColour);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(pTexture, GUILayout.Width(wSize), GUILayout.Height(wSize));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Save", GUILayout.Width(wSize)))
        {
            SaveImage();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

    }

    private void SaveImage()
    {
        byte[] bytes = pTexture.EncodeToPNG();
        Directory.CreateDirectory(Application.dataPath + "/SavedTextures");
        File.WriteAllBytes(Application.dataPath + "/SavedTextures/" + filename + ".png", bytes);
    }

    private void GenerateMap(float minColour, float maxColour)
    {
        int w = 513;
        int h = 513;
        float pValue;
        Color pixColour = Color.white;
        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (seamlessToggle)
                {
                    pValue = SeamlessGeneration(w, h, x, y);

                }
                else
                {
                    pValue = Utils.fBM(x * perlinXScale,
                                        y * perlinYScale,
                                        perlinOctaves,
                                        perlinPersistance,
                                        perlinOffSetX,
                                        perlinOffsetY) * perlinHeightScale;
                }
                float colValue = pValue;
                minColour = minColour > colValue ? colValue : minColour;
                maxColour = maxColour < colValue ? colValue : maxColour;
                pixColour = new Color(colValue, colValue, colValue, alphaToggle ? colValue : 1);
                pTexture.SetPixel(x, y, pixColour);
            }
        }
        if (mapToggle)
        {
            pixColour = SetMap(w, h, pixColour, minColour, maxColour);
        }
        pTexture.Apply(false, false);
    }

    private Color SetMap(int w, int h, Color pixColour, float minColour, float maxColour)
    {
        for (int y = 0; y < h; ++y)
        {
            for (int x = 0; x < w; ++x)
            {
                pixColour = pTexture.GetPixel(x, y);
                float colvalue = pixColour.r;
                colvalue = Utils.Map(colvalue, minColour, maxColour, 0, 1);
                pixColour.r = colvalue;
                pixColour.g = colvalue;
                pixColour.b = colvalue;
                pTexture.SetPixel(x, y, pixColour);
            }
        }

        return pixColour;
    }

    private float SeamlessGeneration(int w, int h, int x, int y)
    {
        float pValue;
        float u = (float)x / w;
        float v = (float)y / h;

        float noise00 = Utils.fBM(x * perlinXScale,
                            y * perlinYScale,
                            perlinOctaves,
                            perlinPersistance,
                            perlinOffSetX,
                            perlinOffsetY) * perlinHeightScale;
        float noise01 = Utils.fBM(x * perlinXScale,
                            y * perlinYScale,
                            perlinOctaves,
                            perlinPersistance,
                            perlinOffSetX,
                            perlinOffsetY + h) * perlinHeightScale;
        float noise10 = Utils.fBM(x * perlinXScale,
                            y * perlinYScale,
                            perlinOctaves,
                            perlinPersistance,
                            perlinOffSetX + w,
                            perlinOffsetY) * perlinHeightScale;
        float noise11 = Utils.fBM(x * perlinXScale,
                            y * perlinYScale,
                            perlinOctaves,
                            perlinPersistance,
                            perlinOffSetX + w,
                            perlinOffsetY + h) * perlinHeightScale;
        float noiseTotal = u * v * noise00 +
                            u * (1 - v) * noise01 +
                            (1 - u) * v * noise10 +
                            (1 - u) * (1 - v) * noise11;
        float value = (256 * noiseTotal) + 50;
        float r = Mathf.Clamp(noise00, 0, 255);
        float g = Mathf.Clamp(value, 0, 255);
        float b = Mathf.Clamp(value + 50, 0, 255);
        float a = Mathf.Clamp(value + 100, 0, 255);

        pValue = (r + b + g) / (3 * 255);
        return pValue;
    }
}
