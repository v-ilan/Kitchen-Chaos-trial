using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    [SerializeField] private PlatesCounter platesCounter;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounterOnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounterOnPlateRemoved;
    }

    private void PlatesCounterOnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounterOnPlateSpawned(object sender, System.EventArgs e)
    {
        const float PLATE_OFFSET_Y = 0.1f;
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        plateVisualTransform.localPosition = new Vector3(0, PLATE_OFFSET_Y * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
