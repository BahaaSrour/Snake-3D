using UnityEngine;
using System.Collections.Generic;
using System.Net;

public class Snake : MonoBehaviour
{

    public Transform bodyPrefab;
    public Vector3 startDirection;
    public float speed = 1f;
    public List<Transform> bodyParts = new List<Transform>();
    Transform snakeTransform;
    Vector3 lastTailPosition;
    [SerializeField] LayerMask foodLayerMask;

    [SerializeField] bool MoveLikeNormal = true;
    [SerializeField] FoodContainer foodContainer;
    void Start()
    {
        snakeTransform = transform;
        bodyParts.Add(transform);
    }
    Vector3 direction;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float diagonal = Input.GetAxis("Depth");

        direction = new Vector3(horizontal, -diagonal, vertical).normalized;
        Move(direction);
    }
    Vector3 lastDirection;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("Food");
            //other
            GameManager.instance.AddPoints(1);

            //TODO::Poling 
            foodContainer.DeactivateFood(other.gameObject.GetComponent<FoodBehaviour>());
            //GridManager.Instance.RemoveFoodFromGrid(other.gameObject.GetComponent<FoodBehaviour>());
            Grow();
        }
    }

    void Grow()
    {
        GameObject newPart = Instantiate(bodyPrefab.gameObject, lastTailPosition, Quaternion.identity);
        bodyParts.Add(newPart.transform);
    }
}