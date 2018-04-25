﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CubeMaker : MonoBehaviour
{
    public int row, col;
    public Vector3 size = Vector3.one;
    MeshCreator mc = new MeshCreator();

    public AudioSourceLoudnessTester m_audioTest;

	public float move;
    // Update is called once per frame
    void Update()
    {
		move = move + 0.01f;
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        // one submesh for each face
		Vector3 center = new Vector3(0, 0, 0);

        mc.Clear(); // Clear internal lists and mesh

        for (int row = 0; row < 20; row++)
        {
            for (int col = 0; col < 20; col++)
            {
               // center.Set(col * size.x * (float)1.2, Perlin.Noise(col * size.x * (float)0.5f+move,row * size.z * (float)1f+move), row * size.z * (float)1.2);
                center.Set(col * size.x * (float)1.2, 0.25f*m_audioTest.clipRange(row,col), row * size.z * (float)1.2);

                CreateCube(center);
            }
        }

        meshFilter.mesh = mc.CreateMesh();

    }

    void CreateCube(Vector3 center)
    {
        Vector3 cubeSize = size * 0.5f;

        // top of the cube
        // t0 is top left point
        Vector3 t0 = new Vector3(center.x + cubeSize.x, center.y + cubeSize.y, center.z - cubeSize.z);
        Vector3 t1 = new Vector3(center.x - cubeSize.x, center.y + cubeSize.y, center.z - cubeSize.z);
        Vector3 t2 = new Vector3(center.x - cubeSize.x, center.y + cubeSize.y, center.z + cubeSize.z);
        Vector3 t3 = new Vector3(center.x + cubeSize.x, center.y + cubeSize.y, center.z + cubeSize.z);

        // bottom of the cube
        Vector3 b0 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
        Vector3 b1 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
        Vector3 b2 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);
        Vector3 b3 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);

        // Top square
        mc.BuildTriangle(t0, t1, t2);
        mc.BuildTriangle(t0, t2, t3);

        // Bottom square
        mc.BuildTriangle(b2, b1, b0);
        mc.BuildTriangle(b3, b2, b0);

        // Back square
        mc.BuildTriangle(b0, t1, t0);
        mc.BuildTriangle(b0, b1, t1);

        mc.BuildTriangle(b1, t2, t1);
        mc.BuildTriangle(b1, b2, t2);

        mc.BuildTriangle(b2, t3, t2);
        mc.BuildTriangle(b2, b3, t3);

        mc.BuildTriangle(b3, t0, t3);
        mc.BuildTriangle(b3, b0, t0);
    }
}