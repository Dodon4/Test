using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [HideInInspector]
    public Node parent;
    private void OnMouseUpAsButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        if(hit.collider == null)
        {
            transform.position = parent.tile.transform.position;
            return;
        }
        foreach(Node el in BoardManager.instance.tiles)
        {
            SetNode(el, hit);
        }
        SetNode(BoardManager.instance.SmallPocket, hit);
        SetNode(BoardManager.instance.BigPocket, hit);
    }
    private void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = transform.position.z;
        transform.position = pos;
    }
    void SetNode(Node el, RaycastHit hit)
    {
        if (el.tile == hit.collider.gameObject)
        {
            if (el.isEmpty)
            {
                transform.position = hit.transform.position;
                parent.isEmpty = true;
                parent.square = null;

                parent = el;

                parent.isEmpty = false;
                parent.square = gameObject;
            }
            else
            {
                transform.position = parent.tile.transform.position;
            }
        }
    }
    public void DestroyAnim()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = !enabled;
        anim.SetTrigger("Death");
        Destroy(gameObject,anim.GetCurrentAnimatorClipInfo(0).Length);

    }
}
