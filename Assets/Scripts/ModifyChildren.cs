using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyChildren : MonoBehaviour
{
    void Awake()
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
                Rigidbody rb = c.AddComponent<Rigidbody>();
                c.AddComponent<BoxCollider>();
                rb.isKinematic = true;
            }
        }
    }
}
