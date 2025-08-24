using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

public class UDP : MonoBehaviour
{
    Thread reciveT;
    UdpClient client;
    public int port = 5052;
    public bool startReciving = true;
    public bool printToConsole = false;
    public List<List<float>> LandMarks = new List<List<float>>();
    void Start()
    {
        reciveT = new Thread(new ThreadStart(ReciveData)); // executes ReciveData method
        reciveT.IsBackground = true;
        reciveT.Start();
    }
    public void ReciveData()
    {
        client = new UdpClient(port);
        while (startReciving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port);
                byte[] databyte = client.Receive(ref anyIP);

                string json = Encoding.UTF8.GetString(databyte);
                LandMarks = JsonConvert.DeserializeObject<List<List<float>>>(json);
                // x = LandMarks[0][0];
                // y = LandMarks[0][1];
                // z = LandMarks[0][2];
            }
            catch (Exception err)
            {
                if (printToConsole)
                    Debug.LogError(err.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
   /* private void OnApplicationQuit()
    {
        startReciving = false;
        client?.Close();
        reciveT?.Join();
    }*/
}
