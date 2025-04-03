using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject[] animalPrefabs;

    [SerializeField]
    float cellSide;
    [SerializeField]
    float diametrRandomSphere;

    Dictionary<Vector2, GameObject> createdObjects = new Dictionary<Vector2, GameObject>();

    public void SpawnAnimals(Rect rect)
    {
        if (rect == default)
            return;

        int xMin = Mathf.CeilToInt(rect.xMin / cellSide);
        int yMin = Mathf.CeilToInt(rect.yMin / cellSide);
        int xMax = Mathf.FloorToInt(rect.xMax / cellSide);
        int yMax = Mathf.FloorToInt(rect.yMax / cellSide);
        for (int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                Vector2 pos = new Vector2(x * cellSide, y * cellSide);
                if (!createdObjects.ContainsKey(pos))
                {
                    var prefab = CommonTools.RandomObject<GameObject>(animalPrefabs);
                    GameObject animal = Instantiate(prefab,
                                                    pos + (Vector2)Random.insideUnitSphere * diametrRandomSphere, Quaternion.identity);
                    createdObjects.Add(pos, animal);
                }
            }
        }

    }

    private void getColumnsAndRows(Rect rect, float density, out int columns, out int rows)
    {
        if (density <= 0)
        {
            Debug.LogError("Density must be greater than zero.");
            columns = 0;
            rows = 0;
            return;
        }

        columns = Mathf.FloorToInt(rect.width / density);
        rows = Mathf.FloorToInt(rect.height / density);
    }
}