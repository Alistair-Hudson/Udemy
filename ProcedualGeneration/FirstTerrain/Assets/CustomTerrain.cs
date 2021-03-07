using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CustomTerrain : MonoBehaviour
{
    public enum TagType { Tag = 0, Layer = 1 }
    [SerializeField]
    int terrainLayer = -1;

    public Vector2 randomHeightRange = new Vector2(0, 0.1f);
    public Texture2D heightMapImage;
    public Vector3 heightMapScale = new Vector3(1, 1, 1);

    public bool resetTerrain = true;

    //Perlin Noise
    public float perlinXScale = 0.01f;
    public float perlinYScale = 0.01f;
    public int perlinOffsetX = 0;
    public int perlinOffsetY = 0;
    public int perlinOctaves = 3;
    public float perlinPersistance = 8;
    public float perlinHeightScale = 0.09f;

    //Multiple Perlin
    [Serializable]
    public class PerlinParamters
    {
        public float mPerlinXScale = 0.01f;
        public float mPerlinYScale = 0.01f;
        public int mPerlinOffsetX = 0;
        public int mPerlinOffsetY = 0;
        public int mPerlinOctaves = 3;
        public float mPerlinPersistance = 8;
        public float mPerlinHeightScale = 0.09f;
        public bool remove = false;
    }

    public List<PerlinParamters> perlinParamters = new List<PerlinParamters>()
    {
        new PerlinParamters()
    };

    //Voroni
    public int voroniNumPeaks = 3;
    public float voroniFallOff = 0.2f;
    public float voroniDropOff = 0.6f;
    public float voroniMinHeight = 0.1f;
    public float voroniMaxHeight = 0.5f;
    public enum VoronoiType
    {
        LINEAR = 0,
        PARABOLA,
        PARBLIN
    };
    public VoronoiType voronoiType = VoronoiType.LINEAR;

    //Mid Point
    public float midPointHeightScaling = 0.01f;
    public float midPointRoughness = 2f;
    public float midPointHeightDampeningRate = 2;

    //Splatmap
    [System.Serializable]
    public class SplatHeights
    {
        public Texture2D texture = null;
        public float minHeight = 0.1f;
        public float maxHeight = 0.2f;
        public float minSlope = 0f;
        public float maxSlope = 1.5f;
        public Vector2 tileOffset = new Vector2(0, 0);
        public Vector2 tileSize = new Vector2(50, 50);
        public bool remove = false;
    }
    public List<SplatHeights> splatHeights = new List<SplatHeights>()
    {
        new SplatHeights()
    };
    public float splatPerlinInc = 0.01f;
    public float splatPerlinScale = 0.1f;

    //Vegetation
    public int vegeMaxTrees = 5000;
    public int vegeTreeSpacing = 5;
    [System.Serializable]
    public class Vegetation
    {
        public GameObject prefab = null;
        public float minHeight = 0.1f;
        public float maxHeight = 0.2f;
        public float minSlope = 0;
        public float maxSlope = 90;
        public bool remove = false;
    }
    public List<Vegetation> vegetation = new List<Vegetation>()
    {
        new Vegetation()
    };

    public Terrain terrain;
    public TerrainData terrainData;

    float[,] GetHeights()
    {
        if (!resetTerrain)
        {
            return terrainData.GetHeights(0,
                                            0,
                                            terrainData.heightmapResolution,
                                            terrainData.heightmapResolution);
        }
        return new float[terrainData.heightmapResolution,
                        terrainData.heightmapResolution];
    }

    float GetSteepness(float[,] heightmap, int x, int y, int width, int height)
    {
        float h = heightmap[x, y];
        int nx = x + 1 > width ? x - 1 : x + 1;
        int ny = y + 1 > height ? y -1 : y + 1;

        float dx = heightmap[nx, y] - h;
        float dy = heightmap[x, ny] - h;
        Vector2 gradient = new Vector2(dx, dy);

        return gradient.magnitude;
    }
    
    public void AddVegetation()
    {
        vegetation.Add(new Vegetation());
    }

    public void RemoveVegetation()
    {
        for (int i = 0; i < vegetation.Count; ++i)
        {
            if (vegetation[i].remove)
            {
                vegetation.Remove(vegetation[i]);
                --i;
            }
        }
        if (vegetation.Count == 0)
        {
            vegetation.Add(new Vegetation());
        }
    }

    public void AddSplatHeight()
    {
        splatHeights.Add(new SplatHeights());
    }

    public void RemoveSplatHeights()
    {

        for (int i = 0; i < splatHeights.Count; ++i)
        {
            if (splatHeights[i].remove)
            {
                splatHeights.Remove(splatHeights[i]);
                --i;
            }
        }
        if (splatHeights.Count == 0)
        {
            splatHeights.Add(new SplatHeights());
        }

    }

    public void SplatMaps()
    {
        InitiateSplatMaps();
        SetSpatMaps();

    }

    private void SetSpatMaps()
    {
        float[,] heightMap = terrainData.GetHeights(0,
                                                    0,
                                                    terrainData.heightmapResolution,
                                                    terrainData.heightmapResolution);
        float[,,] splatmapData = new float[terrainData.alphamapWidth,
                                            terrainData.alphamapHeight,
                                            terrainData.alphamapLayers];
        for (int y = 0; y < terrainData.alphamapHeight; ++y)
        {
            for (int x = 0; x < terrainData.alphamapWidth; ++x)
            {
                float[] splat = new float[terrainData.alphamapLayers];
                for (int i = 0; i < splatHeights.Count; ++i)
                {
                    float noise = Mathf.PerlinNoise(x * splatPerlinInc, 
                                                    y * splatPerlinInc) * splatPerlinScale;
                    float thisHeightStart = splatHeights[i].minHeight - noise;
                    float thisHeightStop = splatHeights[i].maxHeight + noise;
                    float steepness = terrainData.GetSteepness((float)y / terrainData.alphamapHeight,
                                                                (float)x / terrainData.alphamapWidth);
                    if (heightMap[x, y] >= thisHeightStart && 
                        heightMap[x, y] <= thisHeightStop &&
                        steepness >= splatHeights[i].minSlope &&
                        steepness <= splatHeights[i].maxSlope)
                    {
                        splat[i] = 1;
                    }
                }
                NormalizeVector(splat);
                for (int j = 0; j < splatHeights.Count; ++j)
                {
                    splatmapData[x, y, j] = splat[j];
                }
            }
        }
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

    public void PlantVegetation()
    {
        TreePrototype[] treePrototypes;
        treePrototypes = new TreePrototype[vegetation.Count];
        int tindex = 0;
        foreach(Vegetation t in vegetation)
        {
            treePrototypes[tindex] = new TreePrototype();
            treePrototypes[tindex].prefab = t.prefab;
            ++tindex;
        }
        terrainData.treePrototypes = treePrototypes;

        ImpPlantVegetation();
    }

    private void ImpPlantVegetation()
    {
        List<TreeInstance> allVegetation = new List<TreeInstance>();
        for (int z = 0; z < terrainData.size.z; z += vegeTreeSpacing)
        {
            for (int x = 0; x < terrainData.size.x; x += vegeTreeSpacing)
            {
                for (int tp = 0; tp < terrainData.treePrototypes.Length; ++tp)
                {
                    Vector3 scaleFactor = terrainData.heightmapScale;
                    float thisHeight = terrainData.GetHeight((int)(x/scaleFactor.x), (int)(z/scaleFactor.z)) / terrainData.size.y;
                    float thisSteepness = terrainData.GetSteepness(x / (float)terrainData.size.x, z / (float)terrainData.size.z);
                    if (vegetation[tp].minHeight > thisHeight || vegetation[tp].maxHeight < thisHeight ||
                        vegetation[tp].minSlope > thisSteepness || vegetation[tp].maxSlope < thisSteepness)
                    {
                        continue;
                    }
                    TreeInstance instance = new TreeInstance();
                    instance.position = new Vector3((x + UnityEngine.Random.Range(-5, 5)) / terrainData.size.x,
                                                    thisHeight,
                                                    (z + UnityEngine.Random.Range(-5, 5)) / terrainData.size.z);
                    Vector3 treeWorldPos = new Vector3(instance.position.x * terrainData.size.x,
                                                        instance.position.y * terrainData.size.y,
                                                        instance.position.z * terrainData.size.z);
                    RaycastHit hit;
                    int layerMask = 1 << terrainLayer;
                    if (Physics.Raycast(treeWorldPos + new Vector3(0, 10, 0), -Vector3.up, out hit, 100, layerMask) ||
                        Physics.Raycast(treeWorldPos - new Vector3(0, 10, 0), Vector3.up, out hit, 100, layerMask))
                    {
                        instance.position.y = (hit.point.y - this.transform.position.y) / terrainData.size.y;
                    }
                    instance.rotation = UnityEngine.Random.Range(0, 360);
                    instance.prototypeIndex = tp;
                    instance.color = Color.white;
                    instance.lightmapColor = Color.white;
                    float scale = 0.95f;
                    instance.heightScale = scale;
                    instance.widthScale = scale;

                    allVegetation.Add(instance);
                    if (allVegetation.Count >= vegeMaxTrees)
                    {
                        TreesDone(allVegetation);
                        return;
                    }
                }
            }
        }
        TreesDone(allVegetation);

    }

    private void TreesDone(List<TreeInstance> allVegetation)
    {
        terrainData.treeInstances = allVegetation.ToArray();
    }

    private void NormalizeVector(float[] vector)
    {
        float total = 0;
        for (int i = 0; i < vector.Length; ++i)
        {
            total += vector[i];
        }
        for (int i = 0; i < vector.Length; ++i)
        {
            vector[i] /= total;
        }

    }

    private void InitiateSplatMaps()
    {
        TerrainLayer[] newSplatPrototypes = new TerrainLayer[splatHeights.Count];
        int spindex = 0;
        foreach (SplatHeights sh in splatHeights)
        {
            newSplatPrototypes[spindex] = new TerrainLayer();
            newSplatPrototypes[spindex].diffuseTexture = sh.texture;
            newSplatPrototypes[spindex].tileOffset = sh.tileOffset;
            newSplatPrototypes[spindex].tileSize = sh.tileSize;
            newSplatPrototypes[spindex].diffuseTexture.Apply(true);
            string path = "Assets\\New Terrain Layer " + spindex + ".terrainlayer";
            AssetDatabase.CreateAsset(newSplatPrototypes[spindex], path);
            ++spindex;
            Selection.activeObject = this.gameObject;
        }
        terrainData.terrainLayers = newSplatPrototypes;
    }

    public void MidPointDisplacement()
    {
        float[,] heightMap = GetHeights();
        DiamondStep(heightMap);

        terrainData.SetHeights(0, 0, heightMap);
    }

    private void DiamondStep(float[,] heightMap)
    {
        int width = terrainData.heightmapResolution - 1;
        int squareSize = width;
        float height = squareSize / 2 * midPointHeightScaling;
        float heightDamper = Mathf.Pow(midPointHeightDampeningRate, -1 * midPointRoughness);

        int cornerX, cornerY;
        int midX, midY;

        while (squareSize > 0)
        {
            for (int x = 0; x < width; x += squareSize)
            {
                for (int y = 0; y < width; y += squareSize)
                {
                    cornerX = (x + squareSize);
                    cornerY = (y + squareSize);

                    midX = (int)(x + squareSize / 2);
                    midY = (int)(y + squareSize / 2);

                    heightMap[midX, midY] = (float)(heightMap[x, y] +
                                                    heightMap[cornerX, y] +
                                                    heightMap[x, cornerY] +
                                                    heightMap[cornerX, cornerY]) / 4 +
                                                    UnityEngine.Random.Range(-height, height);
                }
            }

            SquareStep(heightMap, width, squareSize, height);

            squareSize /= 2;
            height *= heightDamper;
        }
    }

    private static void SquareStep(float[,] heightMap, int width, int squareSize, float height)
    {
        int cornerX, cornerY;
        int midX, midY;
        int pmidXL, pmidXR, pmidYU, pmidYD;

        for (int x = 0; x < width; x += squareSize)
        {
            for (int y = 0; y < width; y += squareSize)
            {
                cornerX = (x + squareSize);
                cornerY = (y + squareSize);

                midX = (int)(x + squareSize / 2);
                midY = (int)(y + squareSize / 2);

                pmidXR = midX + squareSize;
                pmidYU = midY + squareSize;
                pmidXL = midX - squareSize;
                pmidYD = midY - squareSize;

                if (pmidXL <= 0 ||
                    pmidYD <= 0 ||
                    pmidXR > width - 1 ||
                    pmidYU > width - 1)
                {
                    continue;
                }

                heightMap[midX, y] = (float)(heightMap[midX, midY] +
                                                heightMap[x, y] +
                                                heightMap[midX, pmidYD] +
                                                heightMap[cornerX, y]) / 4 +
                                                UnityEngine.Random.Range(-height, height);
                heightMap[cornerX, midY] = (float)(heightMap[pmidXR, midY] +
                                                heightMap[cornerX, cornerY] +
                                                heightMap[midX, midY] +
                                                heightMap[cornerX, y]) / 4 +
                                                UnityEngine.Random.Range(-height, height);
                heightMap[midX, cornerY] = (float)(heightMap[midX, midY] +
                                                heightMap[cornerX, cornerY] +
                                                heightMap[midX, pmidYU] +
                                                heightMap[x, cornerY]) / 4 +
                                                UnityEngine.Random.Range(-height, height);
                heightMap[x, midY] = (float)(heightMap[midX, midY] +
                                                heightMap[x, cornerY] +
                                                heightMap[pmidXL, midY] +
                                                heightMap[x, y]) / 4 +
                                                UnityEngine.Random.Range(-height, height);

            }
        }
    }

    public void SmoothTerrain()
    {
        float[,] heightMap = GetHeights();
        for (int y = 0; y < terrainData.heightmapResolution;  ++y)
        {
            for (int x = 0; x < terrainData.heightmapResolution; ++x)
            {
                heightMap[x, y] = AverageOfAllSurrounding(heightMap, x, y);
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private float AverageOfAllSurrounding(float[,] heightMap, int x, int y)
    {
        float total = 0;
        int numTilesUsed = 0;

        for (int i = x - 1; i <= x + 1; ++x)
        {
            if (i < 0 || i > heightMap.Length)
            {
                continue;
            }
            for (int j = y - 1; j <= y + 1; ++j)
            {
                if (j < 0 || j > heightMap.Length)
                {
                    continue;
                }
                total += heightMap[i, j];
                ++numTilesUsed;
            }
        }
        return total / numTilesUsed;
    }

    public void VoronoiGeneration()
    {
        float[,] heightMap = GetHeights();
        for (int p = 0; p < voroniNumPeaks; ++p)
        {
            VoronoiPeakGeneration(heightMap);
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private void VoronoiPeakGeneration(float[,] heightMap)
    {
        Vector3 peak = new Vector3(UnityEngine.Random.Range(0, terrainData.heightmapResolution),
                                    UnityEngine.Random.Range(voroniMinHeight, voroniMaxHeight),
                                    UnityEngine.Random.Range(0, terrainData.heightmapResolution));

        heightMap[(int)peak.x, (int)peak.z] = peak.y;

        Vector2 peakLocation = new Vector2(peak.x, peak.z);
        float maxDistance = Vector2.Distance(new Vector2(0, 0), new Vector2(terrainData.heightmapResolution,
                                                                            terrainData.heightmapResolution));

        for (int y = 0; y < terrainData.heightmapResolution; ++y)
        {
            for (int x = 0; x < terrainData.heightmapResolution; ++x)
            {
                float distanceToPeak = Vector2.Distance(peakLocation, new Vector2(x, y)) / maxDistance;
                float h = 0;
                switch (voronoiType)
                {
                    case VoronoiType.LINEAR:
                        h = peak.y - voroniFallOff * distanceToPeak;
                        break;
                    case VoronoiType.PARABOLA:
                        h = peak.y - Mathf.Pow(distanceToPeak, voroniDropOff);
                        break;
                    case VoronoiType.PARBLIN:
                        h = peak.y - voroniFallOff * distanceToPeak - Mathf.Pow(distanceToPeak, voroniDropOff);
                        break;
                }
                if(heightMap[x,y] < h)
                {
                    heightMap[x, y] = h;
                }
            }
        }
    }

    public void PerlinGeneration()
    {
        float[,] heightMap = GetHeights();

        for (int y = 0; y < terrainData.heightmapResolution; ++y)
        {
            for (int x = 0; x < terrainData.heightmapResolution; ++x)
            {
                heightMap[y, x] += Utils.fBM(x * perlinXScale,
                                                y * perlinYScale,
                                                perlinOctaves,
                                                perlinPersistance,
                                                perlinOffsetX,
                                                perlinOffsetY) * perlinHeightScale;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void MultiplePerlinGeneration()
    {
        float[,] heightMap = GetHeights();

        for (int y = 0; y < terrainData.heightmapResolution; ++y)
        {
            for (int x = 0; x < terrainData.heightmapResolution; ++x)
            {
                foreach (PerlinParamters p in perlinParamters)
                {
                    heightMap[y, x] += Utils.fBM(x * p.mPerlinXScale,
                                                    y * p.mPerlinYScale,
                                                    p.mPerlinOctaves,
                                                    p.mPerlinPersistance,
                                                    p.mPerlinOffsetX,
                                                    p.mPerlinOffsetY) * p.mPerlinHeightScale;
                }
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void AddNewPerlin()
    {
        perlinParamters.Add(new PerlinParamters());
    }

    public void RemovePerlin()
    {

        for (int i = 0; i < perlinParamters.Count; ++i)
        {
            if (perlinParamters[i].remove)
            {
                perlinParamters.Remove(perlinParamters[i]);
                --i;
            }
        }
        if (perlinParamters.Count == 0)
        {
            perlinParamters.Add( new PerlinParamters());
        }
        
    }

    public void RandomTerrain()
    {
        float[,] heightMap = GetHeights();
        
        for (int x = 0; x < terrainData.heightmapResolution; ++x)
        {
            for (int z = 0; z < terrainData.heightmapResolution; ++z)
            {
                heightMap[x, z] += UnityEngine.Random.Range(randomHeightRange.x, randomHeightRange.y);
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void LoadTexture()
    {
        float[,] heightMap = GetHeights();

        for (int x = 0; x < terrainData.heightmapResolution; ++x)
        {
            for (int z = 0; z < terrainData.heightmapResolution; ++z)
            {
                heightMap[x, z] += heightMapImage.GetPixel((int)(x * heightMapScale.x),
                                                           (int)(z * heightMapScale.z)).grayscale * heightMapScale.y;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void ResetTerrain()
    {
        float[,] heightMap = terrainData.GetHeights(0,
                                                    0,
                                                    terrainData.heightmapResolution,
                                                    terrainData.heightmapResolution);

        for (int x = 0; x < terrainData.heightmapResolution; ++x)
        {
            for (int z = 0; z < terrainData.heightmapResolution; ++z)
            {
                heightMap[x, z] = 0;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    private void OnEnable()
    {
        Debug.Log("Inialising Terrain Data");
        terrain = GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;
    }

    private void Awake()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings\\TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        AddTag(tagsProp, "Terrain", TagType.Tag);
        AddTag(tagsProp, "Cloud", TagType.Tag);
        AddTag(tagsProp, "Shore", TagType.Tag);

        SerializedProperty layerProp = tagManager.FindProperty("layers");
        terrainLayer = AddTag(layerProp, "Terrain", TagType.Layer);

        tagManager.ApplyModifiedProperties();

        this.gameObject.tag = "Terrain";
        this.gameObject.layer = terrainLayer;
    }

    private int AddTag(SerializedProperty tagsProp, string newTag, TagType tagType)
    {
        for (int i = 0; i < tagsProp.arraySize; ++i)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(newTag))
            {
                return i;
            }
        }
        switch (tagType)
        {
            case TagType.Tag:
                tagsProp.InsertArrayElementAtIndex(0);
                SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(0);
                newTagProp.stringValue = newTag;
                break;
            case TagType.Layer:
                for (int j = 8; j < tagsProp.arraySize; ++j)
                {
                    SerializedProperty l = tagsProp.GetArrayElementAtIndex(j);
                    if (l.stringValue == "")
                    {
                        l.stringValue = newTag;
                        return j;
                    }
                }
                break;
        }
        return -1;
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
