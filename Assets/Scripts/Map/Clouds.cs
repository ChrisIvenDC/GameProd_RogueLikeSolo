using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    [SerializeField] Transform leftMostPos;
    [SerializeField] Transform rightMostPos;
    [SerializeField] bool moveCloud;
    [SerializeField] float speed;
    [SerializeField] int cloudCount;

    private void Awake()
    {
        cloudCount = transform.childCount;
        clouds = new GameObject[cloudCount];


        for (int i = 0; i < cloudCount; ++i)
        {
            clouds[i] = transform.GetChild(i).gameObject;
        }
    }
    private void Update()
    {
        if (moveCloud)
        {
            Debug.Log("CloudShoud Move");
            foreach (var cloud in clouds)
            {
                Vector3 cloudposition = cloud.transform.position;
                cloudposition.x -= speed * Time.deltaTime; 
                if (cloudposition.x <= leftMostPos.position.x)
                {
                    cloudposition.x = rightMostPos.position.x;
                }
                cloud.transform.position = cloudposition;
            }
        }
    }
}
