using UnityEngine;

public class ViewBobController : MonoBehaviour
{
    private bool viewBob = false;

    public float amount = 0.01f;
    public float frequency = 10.0f;
    public float smooth = 10.0f;

    private float defaultAmount;

    private void Start()
    {
        defaultAmount = amount;
    }
    // Update is called once per frame
    void Update()
    {
        if (viewBob)
        {
            Vector3 pos = Vector3.zero;
            pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
            pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2) * amount * 1.6f, smooth * Time.deltaTime);
            transform.localPosition += pos;
        }
        else
        {
            transform.localPosition = Vector3.zero;
            amount = defaultAmount;
        }
    }

    public void SetViewBob(bool b)
    {
        viewBob = b;
    }
}
