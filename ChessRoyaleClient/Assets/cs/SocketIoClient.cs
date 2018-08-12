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
    public GameData GameData;

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
        if (socket != null)
        {
            return;
        }
        socket = IO.Socket(serverURL);
        socket.On(Socket.EVENT_CONNECT, () =>
        {
            // ? On disconnect?
        });

        socket.On("world-update", (data) =>
        {
            GameData = JsonConvert.DeserializeObject<GameData>(data.ToString());
        });
    }

    public void DoMove(string id, Vector2 move)
    {
        if (socket == null)
        {
            return;
        }
        socket.Emit("do-move", id, move.x, move.y);
    }

    public void AddPlayer(string id)
    {
        if (socket == null)
        {
            return;
        }
        socket.Emit("add-player", id);
    }

    void DoClose()
    {
        if (socket == null)
        {
            return;
        }
        socket.Disconnect();
        socket = null;
    }

}