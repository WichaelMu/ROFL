using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RangeFinder : MonoBehaviour
{
    public Mouse Mouse;

    Character player;

    public Color LineColor;
    [Range(3, 256)]
    public int numSegments = 128;
    [Range(.1f, 1f)]
    public float LineWidth = .1f;

    void Start()
    {
        player = GetComponent<Character>();
    }

    public void DoRenderer(float radius)
    {
        LineRenderer LR = gameObject.GetComponent<LineRenderer>();
        LR.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        LR.startColor = LineColor;
        LR.endColor = LineColor;
        LR.startWidth = LineWidth;
        LR.endWidth = LineWidth;
        LR.positionCount = numSegments + 1;
        LR.useWorldSpace = false;

        float deltaTheta = (float)(2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0, z);
            LR.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

    public Character GetPlayer()
    {
        return player;
    }
}