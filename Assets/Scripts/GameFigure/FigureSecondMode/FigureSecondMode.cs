using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FigureSecondMode : FigureBase
{
    [Header("Move")]
    [SerializeField]
    private GameObject tetromino;
    [SerializeField]
    private ForceMode2D moveForceMode;
    [SerializeField]
    [Range(0,1000)]
    private float movePower;

    [Header("Rotate")]
    [SerializeField]
    private float torque;
    [SerializeField]
    private ForceMode2D rotateForceMode;

    [Header("Acceleration")]
    [SerializeField]
    [Range(0, 1000)]
    private float acceleratePower;
    [SerializeField]
    private ForceMode2D accelerateForceMode;

    public override void Move(float direct)
    {
        this.tetromino.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, movePower*direct), moveForceMode);
    }

    public override void Rotate(bool toggle)
    {
        this.tetromino.GetComponent<Rigidbody2D>().AddTorque(torque, rotateForceMode);
    }

    public override void Acceleration(sbyte toggle)
    {
        this.tetromino.GetComponent<Rigidbody2D>().AddForce(new Vector2(acceleratePower * -1, 0), accelerateForceMode);
    }

}
