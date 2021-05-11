using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorVibration : MonoBehaviour
{
    [SerializeField]
    private Transform[] sprites;
    private Vector3[] startPositions;

    [SerializeField]
    private float frequency, amplitute;

    // Start is called before the first frame update
    void Start()
    {
        SetStartPositions();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Random.Range(-amplitute, amplitute), Random.Range(-amplitute, amplitute), 0);

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].localPosition = startPositions[i] + movement;
        }
        
    }

    void SetStartPositions()
    {
        startPositions = new Vector3[sprites.Length];

        for (int i = 0; i < sprites.Length; i++)
        {
            startPositions[i] = sprites[i].localPosition;
        }
    }

}
