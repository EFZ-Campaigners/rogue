using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, BUSY, ENEMYTURN, WON, LOST }

public class BattleSystem : Singleton<BattleSystem>
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    public Unit playerUnit;
    public Unit enemyUnit;

    public Text dialogueText;

    //public BattleHUD playerHUD;
    //public BattleHUD enemyHUD;

    [SerializeReference] private GameObject panelPrefab;

    private GameObject panel;

    public BattleState state;

    private IEnumerator Start()
    {
        state = BattleState.START;
        yield return StartCoroutine(SetUpBattle());

        panel = Instantiate(panelPrefab);
        panel.SetActive(false);
    }

    private void Update()
    {

    }

    private IEnumerator WaitForGridSelection()
    {
        panel.SetActive(true);

        while (!Input.GetMouseButtonDown(0))
        {
            panel.transform.position = BoardManager.ScreenToWorldPointRegulized();
            yield return null;
        }

        panel.SetActive(false);
    }

    private IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawnPoint);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawnPoint);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "An enemy approaches!";

        //playerHUD.SetHUD(playerUnit);
        //enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private IEnumerator PlayerAttack()
    {
        Vector3 origin = playerUnit.transform.position;

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(playerUnit.MoveToAnimCoroutine(enemyUnit.transform.position));

        bool isDead = enemyUnit.TakeDamage(playerUnit.ATK);

        //enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        yield return StartCoroutine(playerUnit.MoveToAnimCoroutine(origin));

        yield return new WaitForSeconds(2);

        playerUnit.HideSelectionCircle();

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    private IEnumerator PlayerMove()
    {
        yield return StartCoroutine(WaitForGridSelection());

        yield return StartCoroutine(playerUnit.MoveToCoroutine(BoardManager.ScreenToWorldPointRegulized()));

        yield return new WaitForSeconds(1f);

        playerUnit.HideSelectionCircle();

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        enemyUnit.ShowSelectionCircle();

        dialogueText.text = enemyUnit.name + "attacks!";

        yield return new WaitForSeconds(1f);

        Vector3 origin = enemyUnit.transform.position;
        yield return StartCoroutine(enemyUnit.MoveToAnimCoroutine(playerUnit.transform.position));

        bool isDead = playerUnit.TakeDamage(enemyUnit.ATK);

        //playerHUD.SetHP(playerUnit.currentHP);

        yield return StartCoroutine(enemyUnit.MoveToAnimCoroutine(origin));

        yield return new WaitForSeconds(1f);

        enemyUnit.HideSelectionCircle();

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You are defeated.";
        }
    }
    private void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";

        playerUnit.ShowSelectionCircle();
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        StartCoroutine(PlayerAttack());

        state = BattleState.BUSY;
    }

    public void OnMoveButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        StartCoroutine(PlayerMove());

        state = BattleState.BUSY;
    }

}
