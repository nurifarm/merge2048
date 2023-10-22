using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientOutput
{
    public Dictionary<string, object> api {
        get; set;
    }

    public string message {
        get; set;
    }

    public string stackTrace {
        get; set;
    }

    public object rs {
        get; set;
    }
}