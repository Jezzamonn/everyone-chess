using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

public class SocketIoClient : MonoBehaviour
{
    public string serverURL = "http://localhost:3000";

    protected Socket socket = null;

    void Destroy()
    {
        DoClose();
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DoOpen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoOpen()
    {
        if (socket == null)
        {
            socket = IO.Socket(serverURL);
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Debug.Log("Socket.IO connected.");
            });

            socket.On("world-update", (data) =>
            {
                Debug.Log(data);
            });
        }
    }

    void DoClose()
    {
        if (socket != null)
        {
            socket.Disconnect();
            socket = null;
        }
    }

}