using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Email", menuName = "ScriptableObjects/Email", order = 1)]
public class EmailScriptableObject : ScriptableObject {
    public string header;
    public string body;
    public string sender;
    public string dateSent; // number?
    public bool read; // is this appropriate?
    public int id; // correlate an email to an event
    public bool sentToPlayer; // is this appropriate?
}
