using UnityEngine;
using TMPro;

public class TileScript : MonoBehaviour
{
    public Vector3 targetPosition;

    public void Initalize(int number, Vector3 targetPosition)
    {
        SetText(number);
        SetTargetPosition(targetPosition);
    }

    private void SetText(int number)
    {
        GetComponentInChildren<TMP_Text>().SetText(number.ToString());
    }

    private void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public bool IsAtCorrectPosition()
    {
        return Mathf.Abs(transform.position.x - targetPosition.x) < 0.1
            && Mathf.Abs(transform.position.y - targetPosition.y) < 0.1;
    }
}
