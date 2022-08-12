using UnityEngine;
using UnityEngine.U2D;

public class SoftBodyDeformation : MonoBehaviour
{
    [SerializeField] public SpriteShapeController spriteShape;
    [SerializeField] public Transform[] points;
    private float splineOffset = 0.5f;

    private void Awake()
    {
        UpdateVerticies();
    }

    private void Update()
    {
        UpdateVerticies();
    }

    private void UpdateVerticies()
    {
        for(int i = 0; i < points.Length - 1; i++)
        {
            Vector2 vertex = points[i].localPosition;

            Vector2 toCenter = (Vector2.zero - vertex).normalized;

            float colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                spriteShape.spline.SetPosition(i, (vertex - toCenter * colliderRadius));
            }
            catch
            {
                Debug.Log("Bad Model");
                spriteShape.spline.SetPosition(i, (vertex - toCenter * (colliderRadius + splineOffset)));
            }

            Vector2 lt = spriteShape.spline.GetLeftTangent(i);

            Vector2 newRt = Vector2.Perpendicular(toCenter) * lt.magnitude;
            Vector2 newLt = Vector2.zero - newRt;

            spriteShape.spline.SetRightTangent(i, newRt);
            spriteShape.spline.SetLeftTangent(i, newLt);
        }
    }
}
