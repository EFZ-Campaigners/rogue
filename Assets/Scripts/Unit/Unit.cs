using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData unitData;

    public Vector2Int boardPosition;
    public List<Vector2Int> movementRange => BoardManager.GetBoardPointRange(boardPosition, step);

    public int maxHP;
    public int currentHP;

    public int cost;
    public int ATK;
    public int HP;
    public int QK;
    public int step;
    public int range;

    [SerializeReference]
    private GameObject selectionCircle;

    private void Awake()
    {
        HideSelectionCircle();
    }

    private void Start()
    {
        cost = unitData.cost;
        ATK = unitData.ATK;
        HP = unitData.HP;
        QK = unitData.QK;
        step = unitData.step;
        range = unitData.range;
    }

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public IEnumerator MoveToAnimCoroutine(Vector3 position, float time = 0.2f)
    {
        Vector3 velocity = Vector3.zero;

        while (Vector3.Distance(transform.position, position) >= 0.01f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, time);
            yield return null;
        }

        transform.position = position;
    }

    public IEnumerator MoveToCoroutine(Vector3 position)
    {
        yield return StartCoroutine(MoveToAnimCoroutine(position));

        boardPosition = BoardManager.WorldToBoardPoint(position);
    }



    public void ShowSelectionCircle()
    {
        selectionCircle.SetActive(true);
    }

    public void HideSelectionCircle()
    {
        selectionCircle.SetActive(false);
    }

}
