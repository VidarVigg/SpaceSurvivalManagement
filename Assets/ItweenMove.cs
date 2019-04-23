using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItweenMove : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public iTween.EaseType ease;

    public Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentPos = this.transform.position;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            iTween.MoveTo(gameObject, iTween.Hash(
                "position", startPos,
                "time", 0.3f,
                "easetype", ease
                ));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            iTween.MoveTo(gameObject, iTween.Hash(
                "position", endPos,
                "time", 0.5f,
                "easetype", ease
                ));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            iTween.PunchPosition(gameObject, Vector3.one * 25, 0.3f);
        }
    }
}
