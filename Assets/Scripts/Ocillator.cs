using UnityEngine;

public class Ocillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        OcillatingObject();
    }

    void OcillatingObject()
    {
        if (period == Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //continuously growing over time

        const float tau = Mathf.PI * 2;//constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;    //recalcualted to going from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
