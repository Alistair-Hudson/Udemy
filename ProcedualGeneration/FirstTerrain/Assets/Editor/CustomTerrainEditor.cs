using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EditorGUITable;
using System;

[CustomEditor(typeof(CustomTerrain))]
[CanEditMultipleObjects]

public class CustomTerrainEditor : Editor
{
    //properties
    SerializedProperty resetTerrain;

    SerializedProperty randomHeightRange;

    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;
    SerializedProperty perlinXScale;

    SerializedProperty perlinYScale;
    SerializedProperty perlinOffsetX;
    SerializedProperty perlinOffsetY;
    SerializedProperty perlinOctaves;
    SerializedProperty perlinPersistance;
    SerializedProperty perlinHeightScale;

    GUITableState perlinParameterTable;
    SerializedProperty perlinParmaters;

    SerializedProperty voroniNumPeaks;
    SerializedProperty voroniFallOff;
    SerializedProperty voroniDropOff;
    SerializedProperty voroniMinHeight;
    SerializedProperty voroniMaxHeight;
    SerializedProperty voronoiType;

    SerializedProperty midPointHeightScaling;
    SerializedProperty midPointRoughness;
    SerializedProperty midPointHeightDampeningRate;

    GUITableState splatmapHeightsTable;
    SerializedProperty splatHeights;
    SerializedProperty splatPerlinInc;
    SerializedProperty splatPerlinScale;

    GUITableState vegetationTable;
    SerializedProperty vegeMaxTrees;
    SerializedProperty vegeTreeSpacing;
    SerializedProperty vegetation;

    //Fold outs
    bool showRandom = false;
    bool showLoadHeights = false;
    bool showPerlinGeneration = false;
    bool showVoronoiGeneration = false;
    bool showMidPointDisplacement = false;
    bool showSmooth = false;
    bool showSplatmap = false;
    bool showVegetation = false;

    private void OnEnable()
    {
        resetTerrain = serializedObject.FindProperty("resetTerrain");

        randomHeightRange = serializedObject.FindProperty("randomHeightRange");

        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");

        perlinXScale = serializedObject.FindProperty("perlinXScale");
        perlinYScale = serializedObject.FindProperty("perlinYScale");
        perlinOffsetX = serializedObject.FindProperty("perlinOffsetX");
        perlinOffsetY = serializedObject.FindProperty("perlinOffsetY");
        perlinOctaves = serializedObject.FindProperty("perlinOctaves");
        perlinPersistance = serializedObject.FindProperty("perlinPersistance");
        perlinHeightScale = serializedObject.FindProperty("perlinHeightScale");

        perlinParameterTable = new GUITableState("perlinParameterTable");
        perlinParmaters = serializedObject.FindProperty("perlinParamters");

        voroniNumPeaks = serializedObject.FindProperty("voroniNumPeaks");
        voroniFallOff = serializedObject.FindProperty("voroniFallOff");
        voroniDropOff = serializedObject.FindProperty("voroniDropOff");
        voroniMinHeight = serializedObject.FindProperty("voroniMinHeight");
        voroniMaxHeight = serializedObject.FindProperty("voroniMaxHeight");
        voronoiType = serializedObject.FindProperty("voronoiType");

        midPointHeightScaling = serializedObject.FindProperty("midPointHeightScaling");
        midPointRoughness = serializedObject.FindProperty("midPointRoughness");
        midPointHeightDampeningRate = serializedObject.FindProperty("midPointHeightDampeningRate");

        splatmapHeightsTable = new GUITableState("splatmapHeightsTable");
        splatHeights = serializedObject.FindProperty("splatHeights");
        splatPerlinInc = serializedObject.FindProperty("splatPerlinInc");
        splatPerlinScale = serializedObject.FindProperty("splatPerlinScale");

        vegeMaxTrees = serializedObject.FindProperty("vegeMaxTrees");
        vegeTreeSpacing = serializedObject.FindProperty("vegeTreeSpacing");
        vegetation = serializedObject.FindProperty("vegetation");


    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CustomTerrain terrain = (CustomTerrain)target;
        EditorGUILayout.PropertyField(resetTerrain);
        DisplayRandomHeights(terrain);
        DisplayPerlinGeneration(terrain);
        DisplayVoronoiGenration(terrain);
        DisplayMidPointDisplacement(terrain);
        DisplayLoadFromImage(terrain);
        DisplaySplatmap(terrain);
        DisplayVegetation(terrain);
        DisplaySmoothing(terrain);
        DisplayResetTerrain(terrain);

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayVegetation(CustomTerrain terrain)
    {
        showVegetation = EditorGUILayout.Foldout(showVegetation, "Vegetation");
        if (showVegetation)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Vegetation", EditorStyles.boldLabel);
            EditorGUILayout.IntSlider(vegeMaxTrees, 0, 10000, "Max Trees");
            EditorGUILayout.IntSlider(vegeTreeSpacing, 0, 100, "Tree Spacing");
            vegetationTable = GUITableLayout.DrawTable(vegetationTable,
                                                           vegetation);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddVegetation();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemoveVegetation();
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Plant Vegetation"))
            {
                terrain.PlantVegetation();
            }
        }
    }

    private void DisplaySplatmap(CustomTerrain terrain)
    {
        showSplatmap = EditorGUILayout.Foldout(showSplatmap, "Splatmap");
        if (showSplatmap)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Splatmaps", EditorStyles.boldLabel);
            EditorGUILayout.Slider(splatPerlinInc, 0, 0.1f, "Perlin Incremints");
            EditorGUILayout.Slider(splatPerlinScale, 0, 0.1f, "Perlin Scale");
            splatmapHeightsTable = GUITableLayout.DrawTable(splatmapHeightsTable,
                                                           splatHeights);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddSplatHeight();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemoveSplatHeights();
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Apply Splat"))
            {
                terrain.SplatMaps();
            }
        }
    }

    private void DisplaySmoothing(CustomTerrain terrain)
    {
        showSmooth = EditorGUILayout.Foldout(showSmooth, "Smooth Terrain");
        if (showSmooth)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Smooth Terrain", EditorStyles.boldLabel);

            if (GUILayout.Button("Smooth Terrain"))
            {
                terrain.SmoothTerrain();
            }
        }
    }

    private void DisplayMidPointDisplacement(CustomTerrain terrain)
    {
        showMidPointDisplacement = EditorGUILayout.Foldout(showMidPointDisplacement, "Mid Point Displacement");
        if (showMidPointDisplacement)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Mid Point Displacement", EditorStyles.boldLabel);
            EditorGUILayout.Slider(midPointHeightScaling, 0, 1, new GUIContent("Height Scaling"));
            EditorGUILayout.Slider(midPointRoughness, 0, 10, new GUIContent("Roughness"));
            EditorGUILayout.Slider(midPointHeightDampeningRate, 0, 10, new GUIContent("Height Dampening Rate"));
            if (GUILayout.Button("Generate Terrain"))
            {
                terrain.MidPointDisplacement();
            }
        }
    }

    private void DisplayVoronoiGenration(CustomTerrain terrain)
    {
        showVoronoiGeneration = EditorGUILayout.Foldout(showVoronoiGeneration, "Voronoi Generation");
        if (showVoronoiGeneration)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Voronoi Tessellation", EditorStyles.boldLabel);
            EditorGUILayout.IntSlider(voroniNumPeaks, 1, 10, new GUIContent("Number of Peaks"));
            EditorGUILayout.Slider(voroniFallOff, 0, 10, new GUIContent("Falloff"));
            EditorGUILayout.Slider(voroniDropOff, 0, 10, new GUIContent("Dropoff"));
            EditorGUILayout.Slider(voroniMinHeight, 0, 1, new GUIContent("Min Height"));
            EditorGUILayout.Slider(voroniMaxHeight, 0, 1, new GUIContent("Max Height"));
            EditorGUILayout.PropertyField(voronoiType);
            if (GUILayout.Button("Generate Terrain"))
            {
                terrain.VoronoiGeneration();
            }

        }
    }

    private void DisplayPerlinGeneration(CustomTerrain terrain)
    {
        showPerlinGeneration = EditorGUILayout.Foldout(showPerlinGeneration, "Perlin Generation");
        if (showPerlinGeneration)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Perlin Noise", EditorStyles.boldLabel);
            perlinParameterTable = GUITableLayout.DrawTable(perlinParameterTable,
                                                           perlinParmaters);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddNewPerlin();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemovePerlin();
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Generate Terrain"))
            {
                terrain.MultiplePerlinGeneration();
            }

        }
    }

    private void DisplayLoadFromImage(CustomTerrain terrain)
    {
        showLoadHeights = EditorGUILayout.Foldout(showLoadHeights, "Load Heights");
        if (showLoadHeights)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Load Heights From Texture", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(heightMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            if (GUILayout.Button("Load Texture"))
            {
                terrain.LoadTexture();
            }
        }
    }

    private void DisplayResetTerrain(CustomTerrain terrain)
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Reset Terrain"))
        {
            terrain.ResetTerrain();
        }
    }

    private void DisplayRandomHeights(CustomTerrain terrain)
    {
        showRandom = EditorGUILayout.Foldout(showRandom, "Random");
        if (showRandom)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Set Heights Between Random Values", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(randomHeightRange);
            if (GUILayout.Button("Random Heights"))
            {
                terrain.RandomTerrain();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
