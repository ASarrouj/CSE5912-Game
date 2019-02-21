using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObstacle : MonoBehaviour
{
    private Avoidance av;
    private BoxCollider col;
    private RectTransform detectRect;

    // Start is called before the first frame update
    void Start()
    {
        av = transform.parent.GetComponent<Avoidance>();
        col = GetComponent<BoxCollider>();
        detectRect = gameObject.AddComponent<RectTransform>();
        detectRect.sizeDelta = new Vector2(col.size.x, col.size.z);
    }

    private void OnTriggerStay(Collider collider) {
        Vector3 direction = collider.transform.position - transform.position;
        direction.Normalize();
        float towardObst = -1 * (Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);
        if (towardObst < 0) {
            towardObst += 360;
        }

        if (towardObst > transform.rotation.y) {
            av.AvoidLeft = true;
        } else {
            av.AvoidRight = true;
        }
    }
}
