using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AICarSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public WaypointManager waypointManager;
    public float oddsToSpawn = 0.001f;
    public MeshRenderer spawnObjectRenderer;
    public List<Material> carPaintList = new List<Material>();

    void Start()
    {
        spawnObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float chance = Random.Range(0.0f, 1.0f);
        if (chance < oddsToSpawn)
        {
            spawnObjectRenderer.material = carPaintList[Random.Range(0, carPaintList.Count - 1)];
            GameObject obj = Instantiate(spawnObject, transform);
            CarInputAI c = obj.GetComponent<CarInputAI>();
            c.waypointManager = waypointManager;
            obj.SetActive(true);
            obj.transform.position = transform.position;
        }
    }
}
