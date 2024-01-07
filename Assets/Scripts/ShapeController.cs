using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    
    public void SetColor(Color color)
    {
        color.a = color.r;
    }
}
