using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GridGenerator gridGenerator = (GridGenerator)target;
        if (GUILayout.Button("Generate Grid"))
        {
            gridGenerator.GenerateTiles();
        }
        
        if (GUILayout.Button("Clear Grid"))
        {
            gridGenerator.ClearTiles();
        }
        
        if (GUILayout.Button("Assign Grid Material"))
        {
            gridGenerator.AssignTileMaterials();
        }
    }

    [MenuItem("Grid Generator/Generate Grid")]
    private static void GenerateGridMenu()
    {
        GridGenerator gridGenerator = FindObjectOfType<GridGenerator>();
        gridGenerator.GenerateTiles();
    }
    
    [MenuItem("Grid Generator/Clear Grid")]
    private static void ClearGridMenu()
    {
        GridGenerator gridGenerator = FindObjectOfType<GridGenerator>();
        gridGenerator.ClearTiles();
    }
    
    [MenuItem("Grid Generator/Assign Material")]
    private static void AssignMaterialMenu()
    {
        GridGenerator gridGenerator = FindObjectOfType<GridGenerator>();
        gridGenerator.AssignTileMaterials();
    }
}
