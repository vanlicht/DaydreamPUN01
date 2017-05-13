using UnityEngine;
using System.Collections;

public class EngineBelt : MonoBehaviour
{

    public GameObject BeltDriver;
    public Material BeltMaterial;

    public GameObject VibrationJoint;
    public GameObject[] Cogs;

    public float Speed;
    float DriverValue;
    float offset;
    float randVal;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        DriverValue = BeltDriver.transform.localScale.x;
        offset = Time.time * Speed * DriverValue * -1.0f;
        
        BeltMaterial.SetTextureOffset("_MainTex", new Vector2(0, offset));
        
        ////Engine Vibration
        //randVal = DriverValue * Random.Range(-0.5f, 0.5f);
        //VibrationJoint.transform.localEulerAngles = new Vector3(randVal, 0f, randVal * 0.5f);

        //Cog rotation
        foreach(GameObject cog in Cogs)
        {
            cog.transform.localEulerAngles = new Vector3(offset * 90f, 0f, 0f);
        }
        
    }
}
