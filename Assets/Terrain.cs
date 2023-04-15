using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    private void Start()
    {
        Generate(5);
    }

    public virtual void Generate(int size)
    {
        if (size == 0)
            return;
        if ((float)size % 2 == 0)
            size -= 1;

        for (int i = -size/2; i <= size/2 ; i++)
        {
            var go = Instantiate(tilePrefab,transform);
            go.transform.localPosition = new Vector3(i, 0, 0);
        }
    }
}
