using UnityEngine;
using System.Collections.Generic;
using System.Net;

public class Snake : MonoBehaviour
{

    public Transform bodyPrefab;
    [SerializeField] float speed = 1f;
    [HideInInspector] public List<Transform> bodyParts = new List<Transform>();
    Transform snakeTransform;
    Vector3 lastTailPosition;

    [SerializeField] bool MoveLikeNormal = true;
    [SerializeField] FoodContainer foodContainer;

    public Transform HaloTransform;
    Vector3 direction;
    Vector3 lastDirection;
    bool fstTime=true;
    void Start()
    {
        snakeTransform = transform;
        bodyParts.Add(transform);
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float diagonal = Input.GetAxis("Depth");

        direction = new Vector3(horizontal, -diagonal, vertical).normalized;
        Move(direction);
        SerTheNerestFood(GridManager.Instance.GetTheNearestFood(snakeTransform.position));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            GameManager.instance.AddPoints(1);
            GameManager.instance.CheckWinGame();
            foodContainer.DeactivateFood(other.gameObject.GetComponent<FoodBehaviour>());
            Grow();
        }
        //else if(fstTime) fstTime=false;
        else  if (other.gameObject.CompareTag("Tail"))
        {
            GameManager.instance.PLayerLost();
        }
    }
    
    void Move(Vector3 direction)
    {
        if (Vector3.Dot(lastDirection, direction) < -.35)
            direction = lastDirection;

        if (direction == Vector3.zero)
        {
            if (MoveLikeNormal)
                direction = lastDirection;
            else
                return;
        }
        lastDirection = direction;
        bodyParts[0].position += direction * speed * Time.deltaTime;
        bodyParts[0].forward = direction;

        MoveTail();
    }
    void MoveTail()
    {
        int lastindextOfTail = bodyParts.Count - 1;
        lastTailPosition = bodyParts[lastindextOfTail].position;

        for (int i = lastindextOfTail; i > 0; i--)
        {
            var dis = bodyParts[i - 1].position - bodyParts[i].position;
            if (dis.sqrMagnitude > .3f)
            {
                bodyParts[i].position += dis.normalized * speed * Time.deltaTime;
                bodyParts[i].forward = dis;
            }
        }
    }
    void Grow()
    {
        GameObject newPart = Instantiate(bodyPrefab.gameObject, lastTailPosition, Quaternion.identity);
        bodyParts.Add(newPart.transform);
        if (bodyParts.Count<4)
            newPart.GetComponent<Collider>().enabled = false;
    }
    void SerTheNerestFood(Transform trans)
    {
        if (trans == null) return;
        HaloTransform.position = trans.position;
    }
    public void SetPLayerSpeed(float Value)
    {
        speed = Value;
    }
}