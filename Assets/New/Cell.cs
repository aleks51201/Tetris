﻿using System.Collections;
using UnityEngine;


public class Cell : MonoBehaviour
{
<<<<<<< Updated upstream

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
=======
    public GameObject childCell;

    public void IsRigidBody2DKinematic(bool isKinematic)
    {
        this.childCell.GetComponent<Rigidbody2D>().isKinematic= isKinematic;
    }
    public void IsColider2DEnaled(bool isEnabled)
    {
        this.childCell.GetComponent<Collider2D>().enabled = isEnabled;
    }
    public void SetLayer(int nummerateLayer)
    {
        this.childCell.layer = nummerateLayer;
    }

>>>>>>> Stashed changes
}
