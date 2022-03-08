using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_Phys : MonoBehaviour
{
    public Rigidbody rb;
    public CapsuleCollider colliderDistance;

    [Range(0,50)]
    public int segments = 50;
    private float xradius = 5;
    private float yradius = 5;
    LineRenderer line;

    public string tagsToWatch = "RB_To_Control";

     //The list of colliders currently inside the trigger
    public List<Collider> TriggerList = new List<Collider>();
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliderDistance = GetComponent<CapsuleCollider>();
        xradius = colliderDistance.radius;
        yradius = colliderDistance.radius;

        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount (segments + 1);
        line.useWorldSpace = false;
        CreatePoints ();
    }

    void CreatePoints ()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition (i,new Vector3(x,0,y) );

            angle += (360f / segments);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!TriggerList.Contains(other) && other.gameObject.CompareTag(tagsToWatch)) 
        {
            TriggerList.Add(other);

            //Enable RB
            other.GetComponent<Rigidbody>().isKinematic = false;
        }

    }

    private void OnTriggerExit(Collider other) 
    {
        TriggerList.Remove(other);
        //Disable RB
        other.GetComponent<Rigidbody>().isKinematic = true;
    }
}
