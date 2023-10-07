using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string TAG_TARGET = "Player";

    public List<Collider2D> detectedObjects = new List<Collider2D>();

    private Collider2D col;

    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(TAG_TARGET))
        {
            detectedObjects.Add(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(TAG_TARGET))
        {
            detectedObjects.Remove(collider);
        }
    }
}