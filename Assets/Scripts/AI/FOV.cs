﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class FOV : MonoBehaviour
    {
        public bool showFOV = false;

        public GameObject shortFOV, longFOV;
        private GameObject leftFOV, rightFOV; 
        private Mesh leftMesh, rightMesh;
        private readonly float visionAngle = 90;
        private readonly float visionDepth = 10;
        private readonly float stuckThreshold = 5;

        void Awake()
        {
            CreateFOV();
        }

        void CreateFOV() {
            shortFOV = new GameObject("shortFOV");
            shortFOV.transform.parent = transform;
            shortFOV.transform.localPosition = new Vector3(0, 1, 4);

            longFOV = new GameObject("longFOV");
            longFOV.transform.parent = transform;
            longFOV.transform.localPosition = new Vector3(0, 1, 4);

            AddBoxColliders();

            if (showFOV) {
                leftFOV = new GameObject("leftFOV");
                leftFOV.transform.parent = shortFOV.transform;
                leftFOV.transform.localPosition = new Vector3(0, 0, 0);
                leftFOV.layer = LayerMask.NameToLayer("Trigger");


                rightFOV = new GameObject("rightFOV");
                rightFOV.transform.parent = shortFOV.transform;
                rightFOV.transform.localPosition = new Vector3(0, 0, 0);
                rightFOV.layer = LayerMask.NameToLayer("Trigger");

                GenerateMeshes();
                DebugRender();
            }
        }

        private void DebugRender() {
            leftFOV.AddComponent<MeshRenderer>();
            leftFOV.AddComponent<MeshFilter>();
            leftFOV.GetComponent<MeshFilter>().mesh = leftMesh;
            leftFOV.GetComponent<Renderer>().material.color = Color.cyan;
            rightFOV.AddComponent<MeshRenderer>();
            rightFOV.AddComponent<MeshFilter>();
            rightFOV.GetComponent<MeshFilter>().mesh = rightMesh;
            rightFOV.GetComponent<Renderer>().material.color = Color.red;
        }  

        private void AddBoxColliders() {
            BoxCollider col = shortFOV.AddComponent<BoxCollider>();
            col.isTrigger = true;
            col.center = new Vector3(0, 0, stuckThreshold / 2);
            col.size = new Vector3(8, 1, stuckThreshold);

            col = longFOV.AddComponent<BoxCollider>();
            col.isTrigger = true;
            col.center = new Vector3(0, 0, visionDepth / 2);
            col.size = new Vector3(8, 1, visionDepth);

            
        }

        public void GenerateMeshes() {
            float VisionAngleRadians = visionAngle * Mathf.PI / 180;

            Vector3[] leftVertices = {
                new Vector3(0, 0, 0),

                new Vector3(Mathf.Cos(VisionAngleRadians / 2 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(VisionAngleRadians / 2 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(VisionAngleRadians * 7 / 16 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(VisionAngleRadians * 7 / 16 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(VisionAngleRadians * 3 / 8 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(VisionAngleRadians * 3 / 8 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(VisionAngleRadians * 5 / 16 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(VisionAngleRadians * 5 / 16 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(VisionAngleRadians / 4 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(VisionAngleRadians / 4 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(VisionAngleRadians * 3 / 16 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(VisionAngleRadians * 3 / 16 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(VisionAngleRadians / 8 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(VisionAngleRadians / 8 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(VisionAngleRadians / 16 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(VisionAngleRadians / 16 + Mathf.PI / 2) * stuckThreshold),

                new Vector3(0, 0, stuckThreshold),

                };

            Vector3[] rightVertices = {
                new Vector3(0, 0, 0),

                new Vector3(0, 0, stuckThreshold),

                new Vector3(Mathf.Cos(-VisionAngleRadians / 16 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(-VisionAngleRadians / 16 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(-VisionAngleRadians / 8 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(-VisionAngleRadians / 8 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(-VisionAngleRadians * 3 / 16 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(-VisionAngleRadians * 3 / 16 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(-VisionAngleRadians / 4 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(-VisionAngleRadians / 4 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(-VisionAngleRadians * 5 / 16 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(-VisionAngleRadians * 5 / 16 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(-VisionAngleRadians * 3 / 8 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(-VisionAngleRadians * 3 / 8 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(-VisionAngleRadians * 7 / 16 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(-VisionAngleRadians * 7 / 16 + Mathf.PI / 2) * stuckThreshold),
                new Vector3(Mathf.Cos(-VisionAngleRadians / 2 + Mathf.PI / 2) * stuckThreshold, 0, Mathf.Sin(-VisionAngleRadians / 2 + Mathf.PI / 2) * stuckThreshold),
            };

            int[] triangles = {
                0, 1, 2,    0, 2, 3,    0, 3, 4,    0, 4, 5,    0, 5, 6,    0, 6, 7,    0, 7, 8,    0, 8, 9,
            };

            leftMesh = new Mesh();
            leftMesh.Clear();
            leftMesh.vertices = leftVertices;
            leftMesh.triangles = triangles;

            rightMesh = new Mesh();
            rightMesh.Clear();
            rightMesh.vertices = rightVertices;
            rightMesh.triangles = triangles;


            Vector3[] normals = {   Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                                    Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up };
            leftMesh.normals = normals;
            leftMesh.RecalculateNormals();
            rightMesh.normals = normals;
            rightMesh.RecalculateNormals();
        }
    }
}
