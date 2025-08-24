using System.Drawing;
using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public UDP udp;
    public GameObject[] HandPoints;
    void Start()
    {
        udp = GetComponent<UDP>();
    }

    // Update is called once per frame
    void Update()
    {
        List<List<float>> data = udp.LandMarks;

        for (int i = 0; i < 21; i++) // 21 points in hand
        {
            if (data.Count == 21)
            {
                float x = data[i][0] / 100;
                float y = data[i][1] / 100;
                float z = data[i][2] / 100;
                HandPoints[i].transform.localPosition = new Vector3(x, y, z);
            }
            Debug.Log(data.Count + "\r");
            
        }
    }
}
