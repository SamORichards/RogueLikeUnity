using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Regenerate")) {
            TileMap tileMap = (TileMap)target;
            tileMap.CreateMesh();
        }
        if (GUILayout.Button("Destory Walls")) {
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (GameObject wallBlocks in walls) {
                GameObject.DestroyImmediate(wallBlocks);
            }
        }
    }
}
