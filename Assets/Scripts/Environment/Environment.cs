using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    Transform[] children;
    bool[] isChildrenTransparent;
    Transform playerTransform;

    private void Start()
    {
        children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        isChildrenTransparent = new bool[children.Length];
        for (int i = 0; i < isChildrenTransparent.Length; i++)
        {
            isChildrenTransparent[i] = false;
        }
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            if (GameObject.Find("PlayerSpawnPoint").transform.GetChild(0) != null)
            {
                playerTransform = GameObject.Find("PlayerSpawnPoint").transform.GetChild(0);
            }
            else
            {
                return;
            }
        }

        ;
        var dir = playerTransform.position - Camera.main.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, dir, dir.magnitude);

        for (int i = 0; i < children.Length; i++)
        {
            bool containChild = false;
            for (int a = 0; a < hits.Length; a++)
            {
                if (hits[a].transform == children[i])
                {
                    containChild = true;
                    break;
                }
            }
            if (containChild)
            {
                if (!isChildrenTransparent[i])
                {
                    isChildrenTransparent[i] = true;
                    SpriteRenderer sr = children[i].GetComponent<SpriteRenderer>();
                    StartCoroutine(SpriteManager.instance.FadeToCoroutine(sr, 0.5f));
                }
            }
            else if (isChildrenTransparent[i])
            {
                isChildrenTransparent[i] = false;
                SpriteRenderer sr = children[i].GetComponent<SpriteRenderer>();
                StartCoroutine(SpriteManager.instance.FadeToCoroutine(sr, 1f));
            }
        }
    }

}
