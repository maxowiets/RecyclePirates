using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogPreset : ScriptableObject
{
    public List<string> dialogText;
    public List<bool> isPlayerTalking;
}
