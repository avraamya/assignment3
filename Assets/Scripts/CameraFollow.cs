using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float speed = 2.0f;    
    // Start is called before the first frame update
    void Start()
    {
        
        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position = player.transform.position + offset;
       float interpolation = speed * Time.deltaTime;
       Vector3 position = this.transform.position;
       position.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, interpolation);
       position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, interpolation);
       this.transform.position = position;
    }
}
