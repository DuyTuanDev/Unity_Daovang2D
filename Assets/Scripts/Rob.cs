using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob : MonoBehaviour
{
    public string _tag;
    public float slowDown;
    public int dollar;
    void Awake() {
        this.tag = _tag;
    }
}
