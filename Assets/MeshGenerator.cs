using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public int xSize = 20;
    public int zSize = 20;

    Mesh mesh;
    float y = 0;

    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        var curve = Handles.MakeBezierPoints(
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(-5.0f, 0.0f, 0.0f),
            new Vector3(-5.0f, 0.75f, 0.75f),
            new Vector3(1.0f, -3f, -0.75f),
            400
        );

        
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for (int z = 0; z <= zSize; z++)
        {
            var curveAgentx = curve[z * 15].x * 5;
            var curveAgenty = curve[z * 15].y * 5;

            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x + curveAgentx, y + curveAgenty, z * 5);
                i++;
            }
            y -= .3f;
        }



        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null) return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
