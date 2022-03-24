using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NumberData", order = 1)]
public class Numbers : ScriptableObject
{
    public int min = 0;
    public int max = 30;
    public char sign = '+';
}