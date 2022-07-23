using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FieldSecondMode: FieldBase
{
    [Header("Field size")]
    [SerializeField]
    private int fieldWidth;
    [SerializeField]
    private int fieldHeight;

    [Header("Tetromino positions")]
    [SerializeField]
    private Vector2 spawnPosition;
    [SerializeField]
    private Vector2 queuePosition;
    [SerializeField]
    private Vector2 stashPosition;
}
