using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour
{

    public int size_x = 50;
    public int size_z = 50;
    public float tileSize = 1.0f;
    TMData map;

    internal TMData returnTMData() {
        return map;
    }

    internal Vector3 getPosition(Transform tObject)
    {
        Vector3 pos = new Vector3();
        int x = Mathf.FloorToInt(tObject.transform.position.x / tileSize);
        int z = -Mathf.FloorToInt(tObject.transform.position.z / tileSize);
        pos.x = x;
        pos.z = z;
        return pos;
    }

    public Texture2D tileTextures;
    public int tileRes;
    public GameObject Block;
    public GameObject go;
    public GameObject WallParent;
    public GameObject GameMangers;

    // Use this for initialization
    void Start()
    {
        CreateMesh();
    }


    Color[][] chopUpTile() {
        //Section the texture up into actual tiles
        int numTilesPerRow = tileTextures.width / tileRes;
        int numOfRows = tileTextures.height / tileRes;
        Color[][] tiles = new Color[size_x * size_z][];
        for (int y = 0; y < numOfRows; y++) {
            for (int x = 0; x < numTilesPerRow; x++)
            {
                tiles[y * numOfRows + x] = tileTextures.GetPixels(x * tileRes, y * tileRes, tileRes, tileRes);
            }
        }
        return tiles;
    }


    void CreateTexture() {
        map = new TMData(size_z, size_x);
        int textureWidth = size_x * tileRes;
        int textureHight = size_z * tileRes;
        Texture2D texture = new Texture2D(textureWidth, textureHight);
        Color[][] tiles = chopUpTile();
        for (int y = 0; y < size_z; y++) {
            for (int x = 0; x < size_x; x++) {
                Color[] c =  tiles[(int) map.getTileAt(x, y)];
                texture.SetPixels(x * tileRes, y * tileRes, tileRes, tileRes, c);
            }
        }
        texture.Apply();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        Debug.Log("Done Texture");
        SpawnWalls(map);
        go.GetComponent<PlayerController>().setSpawn(map, size_x, size_z);
    }

    private void SpawnWalls(TMData td)
    {

            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_z; y++)
                {
                if ((int)td.getTileAt(x, y) == (int)TMData.TileType.WALL_TILE)
                {
                    float totY = -size_z + y + 0.5f;
                    Vector3 pos = new Vector3(x + 0.5f, 0, totY);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    Block.transform.localScale = new Vector3(1, UnityEngine.Random.Range(0.5f, 3.5f), 1);
                    Instantiate(Block, pos, rot);
                }
                else if ((int)td.getTileAt(x, y) == (int)TMData.TileType.STONE_TILE) {
                    float totY = -size_z + y + 0.5f;
                    Vector3 pos = new Vector3(x + 0.5f, 0, totY);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    Block.transform.localScale = new Vector3(1, UnityEngine.Random.Range(0.5f, 5), 1);
                    Instantiate(Block, pos, rot);
                }
                }
            }

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wallsTemp in walls) {
            wallsTemp.transform.parent = WallParent.transform;
        }
        ItemHandler IH = GameMangers.GetComponent<ItemHandler>();
        IH.spawnObjects(td, size_x, size_z);
    }

    public void CreateMesh()
    {

        int numTiles = size_x * size_z;
        int numTris = numTiles * 2;

        int vsize_x = size_x + 1;
        int vsize_z = size_z + 1;
        int numVerts = vsize_x * vsize_z;

        // Generate the mesh data
        Vector3[] vertices = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        int[] triangles = new int[numTris * 3];

        int x, z;
        for (z = 0; z < vsize_z; z++)
        {
            for (x = 0; x < vsize_x; x++)
            {
                vertices[z * vsize_x + x] = new Vector3(x * tileSize, 0, -z * tileSize);
                normals[z * vsize_x + x] = Vector3.up;
                uv[z * vsize_x + x] = new Vector2((float)x / size_x, 1f - (float)z / size_z);
            }
        }
        Debug.Log("Done Verts!");

        for (z = 0; z < size_z; z++)
        {
            for (x = 0; x < size_x; x++)
            {
                int squareIndex = z * size_x + x;
                int triOffset = squareIndex * 6;
                triangles[triOffset + 0] = z * vsize_x + x + 0;
                triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 0;
                triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 1;

                triangles[triOffset + 3] = z * vsize_x + x + 0;
                triangles[triOffset + 5] = z * vsize_x + x + vsize_x + 1;
                triangles[triOffset + 4] = z * vsize_x + x + 1;
            }
        }

        Debug.Log("Done Triangles!");

        // Create a new Mesh and populate with the data
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        // Assign our mesh to our filter/renderer/collider
        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        mesh_filter.mesh = mesh;
        mesh_collider.sharedMesh = mesh;
        Debug.Log("Done Mesh!");
        CreateTexture();
    }


}
