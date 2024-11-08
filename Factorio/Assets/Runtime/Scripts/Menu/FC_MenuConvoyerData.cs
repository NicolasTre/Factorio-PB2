using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FC_MenuConvoyerData
{
    public DIRECTION direction { get; set; }
    public Vector3 position { get; set; }

    public FC_MenuConvoyerData(DIRECTION direction, Vector3 position)
    {
        this.direction = direction;
        this.position = position;
    }
}