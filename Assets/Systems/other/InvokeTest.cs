using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeTest : MonoBehaviour
{
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(refresh());
    }

  IEnumerator refresh()
    {
        while (true)
        {
            Invoke(nameof(Increm), 0.1f);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Increm()
    {
        num++;
    }
}
