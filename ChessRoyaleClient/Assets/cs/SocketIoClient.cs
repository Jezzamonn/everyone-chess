﻿using System.Collections;
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
        // Lets just test json parsing for the mo
        string somesstring = @"{
  ""tiles"": [
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ],
    [
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1
    ]
  ],
  ""powerups"": [
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ],
    [
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    ]
  ],
  ""players"": [
    {
      ""id"": 0,
      ""x"": 0,
      ""y"": 4,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 1,
      ""x"": 8,
      ""y"": 1,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 2,
      ""x"": 7,
      ""y"": 5,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 3,
      ""x"": 9,
      ""y"": 7,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 4,
      ""x"": 4,
      ""y"": 7,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 5,
      ""x"": 9,
      ""y"": 4,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 6,
      ""x"": 5,
      ""y"": 5,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 7,
      ""x"": 7,
      ""y"": 1,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 8,
      ""x"": 6,
      ""y"": 7,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 9,
      ""x"": 10,
      ""y"": 4,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 10,
      ""x"": 5,
      ""y"": 4,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 11,
      ""x"": 6,
      ""y"": 1,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 12,
      ""x"": 4,
      ""y"": 6,
      ""type"": {
        ""id"": 3,
        ""letter"": ""B""
      },
      ""dead"": false
    },
    {
      ""id"": 13,
      ""x"": 4,
      ""y"": 4,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 14,
      ""x"": 5,
      ""y"": 0,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 15,
      ""x"": 8,
      ""y"": 0,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 16,
      ""x"": 3,
      ""y"": 0,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 17,
      ""x"": 5,
      ""y"": 8,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 18,
      ""x"": 0,
      ""y"": 3,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 19,
      ""x"": 9,
      ""y"": 7,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 20,
      ""x"": 8,
      ""y"": 3,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 21,
      ""x"": 4,
      ""y"": 2,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 22,
      ""x"": 0,
      ""y"": 5,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 23,
      ""x"": 7,
      ""y"": 0,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 24,
      ""x"": 10,
      ""y"": 2,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 25,
      ""x"": 2,
      ""y"": 10,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 26,
      ""x"": 4,
      ""y"": 4,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 27,
      ""x"": 7,
      ""y"": 1,
      ""type"": {
        ""id"": 3,
        ""letter"": ""B""
      },
      ""dead"": false
    },
    {
      ""id"": 28,
      ""x"": 9,
      ""y"": 3,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 29,
      ""x"": 5,
      ""y"": 4,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 30,
      ""x"": 5,
      ""y"": 4,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 31,
      ""x"": 6,
      ""y"": 4,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 32,
      ""x"": 4,
      ""y"": 2,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 33,
      ""x"": 9,
      ""y"": 7,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 34,
      ""x"": 1,
      ""y"": 4,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 35,
      ""x"": 1,
      ""y"": 4,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 36,
      ""x"": 9,
      ""y"": 2,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 37,
      ""x"": 7,
      ""y"": 6,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 38,
      ""x"": 4,
      ""y"": 7,
      ""type"": {
        ""id"": 3,
        ""letter"": ""B""
      },
      ""dead"": false
    },
    {
      ""id"": 39,
      ""x"": 10,
      ""y"": 3,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 40,
      ""x"": 6,
      ""y"": 2,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 41,
      ""x"": 9,
      ""y"": 9,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 42,
      ""x"": 2,
      ""y"": 6,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 43,
      ""x"": 9,
      ""y"": 7,
      ""type"": {
        ""id"": 3,
        ""letter"": ""B""
      },
      ""dead"": false
    },
    {
      ""id"": 44,
      ""x"": 5,
      ""y"": 10,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 45,
      ""x"": 1,
      ""y"": 5,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 46,
      ""x"": 9,
      ""y"": 2,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 47,
      ""x"": 3,
      ""y"": 4,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 48,
      ""x"": 8,
      ""y"": 1,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 49,
      ""x"": 5,
      ""y"": 5,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 50,
      ""x"": 0,
      ""y"": 3,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 51,
      ""x"": 3,
      ""y"": 2,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 52,
      ""x"": 4,
      ""y"": 7,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 53,
      ""x"": 2,
      ""y"": 0,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 54,
      ""x"": 9,
      ""y"": 9,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 55,
      ""x"": 9,
      ""y"": 8,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 56,
      ""x"": 0,
      ""y"": 9,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 57,
      ""x"": 7,
      ""y"": 5,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 58,
      ""x"": 8,
      ""y"": 1,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 59,
      ""x"": 9,
      ""y"": 9,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 60,
      ""x"": 6,
      ""y"": 3,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 61,
      ""x"": 8,
      ""y"": 3,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 62,
      ""x"": 4,
      ""y"": 3,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 63,
      ""x"": 7,
      ""y"": 5,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 64,
      ""x"": 2,
      ""y"": 0,
      ""type"": {
        ""id"": 3,
        ""letter"": ""B""
      },
      ""dead"": false
    },
    {
      ""id"": 65,
      ""x"": 3,
      ""y"": 5,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 66,
      ""x"": 4,
      ""y"": 8,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 67,
      ""x"": 9,
      ""y"": 6,
      ""type"": {
        ""id"": 3,
        ""letter"": ""B""
      },
      ""dead"": false
    },
    {
      ""id"": 68,
      ""x"": 5,
      ""y"": 0,
      ""type"": {
        ""id"": 3,
        ""letter"": ""B""
      },
      ""dead"": false
    },
    {
      ""id"": 69,
      ""x"": 2,
      ""y"": 8,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 70,
      ""x"": 8,
      ""y"": 5,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 71,
      ""x"": 9,
      ""y"": 5,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 72,
      ""x"": 3,
      ""y"": 1,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 73,
      ""x"": 0,
      ""y"": 0,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 74,
      ""x"": 10,
      ""y"": 5,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 75,
      ""x"": 0,
      ""y"": 4,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 76,
      ""x"": 0,
      ""y"": 7,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 77,
      ""x"": 10,
      ""y"": 10,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 78,
      ""x"": 7,
      ""y"": 1,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 79,
      ""x"": 5,
      ""y"": 6,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 80,
      ""x"": 5,
      ""y"": 0,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 81,
      ""x"": 3,
      ""y"": 0,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 82,
      ""x"": 4,
      ""y"": 10,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 83,
      ""x"": 4,
      ""y"": 9,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 84,
      ""x"": 0,
      ""y"": 1,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 85,
      ""x"": 3,
      ""y"": 1,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 86,
      ""x"": 4,
      ""y"": 5,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 87,
      ""x"": 4,
      ""y"": 7,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 88,
      ""x"": 5,
      ""y"": 4,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 89,
      ""x"": 6,
      ""y"": 4,
      ""type"": {
        ""id"": 1,
        ""letter"": ""P""
      },
      ""dead"": false
    },
    {
      ""id"": 90,
      ""x"": 9,
      ""y"": 8,
      ""type"": {
        ""id"": 3,
        ""letter"": ""B""
      },
      ""dead"": false
    },
    {
      ""id"": 91,
      ""x"": 9,
      ""y"": 2,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 92,
      ""x"": 1,
      ""y"": 7,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 93,
      ""x"": 8,
      ""y"": 9,
      ""type"": {
        ""id"": 4,
        ""letter"": ""R""
      },
      ""dead"": false
    },
    {
      ""id"": 94,
      ""x"": 10,
      ""y"": 6,
      ""type"": {
        ""id"": 2,
        ""letter"": ""N""
      },
      ""dead"": false
    },
    {
      ""id"": 95,
      ""x"": 6,
      ""y"": 9,
      ""type"": {
        ""id"": 6,
        ""letter"": ""K""
      },
      ""dead"": false
    },
    {
      ""id"": 96,
      ""x"": 7,
      ""y"": 0,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    },
    {
      ""id"": 97,
      ""x"": 7,
      ""y"": 8,
      ""type"": {
        ""id"": 5,
        ""letter"": ""Q""
      },
      ""dead"": false
    }
  ]
}";
        GameData game = JsonConvert.DeserializeObject<GameData>(somesstring);

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
                //// TODO: Parse data here, save it and let something else have at it?
                //object someGreatData = JsonConvert.DeserializeObject((string)data);
                //Debug.Log(someGreatData);
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