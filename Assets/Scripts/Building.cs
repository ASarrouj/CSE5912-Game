using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    void Start()
    {
        AddPhysicsToChildren(gameObject);

    }

    void AddPhysicsToChildren(GameObject g) {
        int numChildren = g.transform.childCount;
        for (int i = 0; i < numChildren; i++) {
            GameObject c = g.transform.GetChild(i).gameObject;
            if (c.transform.childCount > 0) {
                AddPhysicsToChildren(c);
            } else {
                c.AddComponent<Rigidbody>();
                c.AddComponent<BoxCollider>();
            }
        }
    }
}
