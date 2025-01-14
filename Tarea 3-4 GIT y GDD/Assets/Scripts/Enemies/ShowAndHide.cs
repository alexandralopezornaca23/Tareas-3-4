using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform showPoint;
    public Transform hidePoint;

    public float waitForShow;
    public float waitForHide;

    public float speedShow;
    public float speedHide;

    float timerShow;
    float timerHide;

    float speed;
    Vector2 targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = hidePoint.position;
        speed = speedHide;
        timerHide = 0f;
        timerShow = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position, targetPoint, speed * Time.deltaTime);

        if (Vector2.Distance(objectToMove.transform.position, hidePoint.position) < 0.1f)
        {
            //el objeto esta en el punto de escondido (hide).
            timerShow += Time.deltaTime;
            if (timerShow >= waitForShow && !Locked()) 
            {
                targetPoint = showPoint.position;
                speed = speedShow;
                timerShow = 0f;
            }            
        }
        else if (Vector2.Distance(objectToMove.transform.position, showPoint.position) < 0.1f)
        {
            //el objeto esta en el punto de mostrado (show).
            timerHide += Time.deltaTime;
            if (timerHide >= waitForHide)
            {
                targetPoint = hidePoint.position;
                speed = speedHide;
                timerHide = 0f;
            }
        }
    }

    bool Locked()
    {
        return Physics2D.OverlapBox(transform.position + Vector3.up, Vector2.one, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.up, Vector2.one);
    }
}
