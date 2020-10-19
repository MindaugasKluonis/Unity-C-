using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop : MonoBehaviour
{

    public float hopHeight = 1.25f;
    private bool hopping = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0.0f;
            StartCoroutine(HopToDestination(pos, 0.3f));
        }
    }

    IEnumerator HopToDestination(Vector3 dest, float time)
    {
        if (hopping) yield break;

        hopping = true;
        var startPos = transform.position;
        var timer = 0.0f;

        while (timer <= 1.0f)
        {
            var height = Mathf.Sin(Mathf.PI * timer) * hopHeight;
            transform.position = Vector3.Lerp(startPos, dest, timer) + Vector3.up * height;

            timer += Time.deltaTime / time;
            yield return null;
        }
        hopping = false;
    }


}