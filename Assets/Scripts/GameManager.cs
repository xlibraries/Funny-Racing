using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform startpoint;

    public float fuel;
    public float distanceCovered;
    private float distX;
    private float distY;
    private float distZ;
    private float startX;
    private float startY;
    private float startZ;

    readonly CarController player;

    // Start is called before the first frame update
    void Start()
    {
        fuel = 100;
        startX = startpoint.position.x;
        startY = startpoint.position.y;
        startZ = startpoint.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //DistanceCovered();
    }

    public void DistanceCovered()
    {
        distX = this.transform.position.x;
       // Debug.Log(distanceX.ToString());
        distY = this.transform.position.y;
        distZ = this.transform.position.z;

        distanceCovered = Mathf.Sqrt(
            Mathf.Pow(distX - startX, 2) +
            Mathf.Pow(distY - startY, 2) +
            Mathf.Pow(distZ - startZ, 2)
            );
        Debug.Log(distanceCovered);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collider.CompareTag("Fuel"))
        {
            fuel = 100;
        }
    }
}
