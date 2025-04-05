using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public Transform floatingObject; // Assign the Sprite
    public Transform shadowObject;   // Assign the Shadow
    public float floatAmplitude = 0.2f;
    public float floatFrequency = 1f;
    public float shadowScaleFactor = 0.05f;

    private Vector3 initialPos;
    private Vector3 initialShadowScale;

    void Start()
    {
        if (floatingObject != null)
            initialPos = floatingObject.localPosition;

        if (shadowObject != null)
            initialShadowScale = shadowObject.localScale;
    }

    void Update()
    {
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        if (floatingObject != null)
            floatingObject.localPosition = initialPos + Vector3.up * floatOffset;

        if (shadowObject != null)
        {
            float scaleAdjust = 1 - (floatOffset + floatAmplitude) * shadowScaleFactor;
            shadowObject.localScale = new Vector3(initialShadowScale.x * scaleAdjust, initialShadowScale.y * scaleAdjust, 1f);
        }
    }
}
