using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{


    public GameObject Player;
    private Unit playerUnit;
    private Unit enemyUnit;
    public Text dialogueText;

    public GameObject[] monsters;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;
    public GameObject enemyGO;
    public AudioClip attack;
    public AudioClip monsterDead;
    public Button attackButton;
    public Button healButton;

    private bool isDoingSomething;
    private GameObject enemy;
    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {

        state = BattleState.START;
        playerUnit = Player.GetComponent<Unit>();
        StartCoroutine(SetupBattle());
        dialogueText.color = Color.black;
        audioPlayer = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (state == BattleState.ENEMYTURN) isDoingSomething = true;
        attackButton.interactable = !isDoingSomething;
        healButton.interactable = !isDoingSomething;
    }

    IEnumerator SetupBattle()
    {

        if (PlayerPrefs.GetString("monsterPrefab") == "GreenMonster") enemyGO = monsters[0];
        else enemyGO = monsters[1];
        enemy = Instantiate(enemyGO);

        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyUnit.unitName = PlayerPrefs.GetString("monsterName");
        enemyUnit.damage = PlayerPrefs.GetInt("monsterAttack");
        enemyUnit.unitLevel = PlayerPrefs.GetInt("monsterLevel");
        enemyUnit.currentHP = enemyUnit.maxHP;

        playerUnit.currentHP = PlayerPrefs.GetInt("playerHP");

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        audioPlayer.PlayOneShot(attack);
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            isDoingSomething = false;
            audioPlayer.PlayOneShot(monsterDead);
            Invoke("EndBattle", 0.8f);
        }
        else
        {
            state = BattleState.ENEMYTURN;

            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        while(state == BattleState.ENEMYTURN)
        {
            audioPlayer.PlayOneShot(attack);
            playerHUD.SetHP(playerUnit.currentHP);
            isDoingSomething = true;
            dialogueText.text = enemyUnit.unitName + " attacks!";

            yield return new WaitForSeconds(1.5f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);


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


    }

    void EndBattle()
    {
        Camera.main.GetComponent<AudioSource>().Play();
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            isDoingSomething = false;
            PlayerPrefs.DeleteKey("monsterName");
            PlayerPrefs.DeleteKey("monsterAttack");
            PlayerPrefs.DeleteKey("monsterLevel");
            PlayerPrefs.DeleteKey("monsterPrefab");
            PlayerPrefs.SetInt("playerHP", playerUnit.currentHP);
            GameObject.Find("Looter").GetComponent<Looter>().CurrentHealth = playerUnit.currentHP;
            GameObject.Find("Hero").GetComponent<Player>().killedMonsters++;
            PlayerPrefs.SetInt("killedMonsters", GameObject.Find("Hero").GetComponent<Player>().killedMonsters);
            Destroy(enemy);

            SceneManager.UnloadSceneAsync(2);

        }
        else if (state == BattleState.LOST)
        {
            dialogueText.color = Color.red;

            dialogueText.text = "You were defeated.";
            PlayerPrefs.DeleteAll();
            GameObject.Find("Looter").GetComponent<Looter>().CurrentHealth = playerUnit.currentHP;

            SceneManager.UnloadSceneAsync("BattleScene");
            SceneManager.LoadScene(0);
        }
    }

    void PlayerTurn()
    {
        isDoingSomething = false;
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(Random.Range(30,45));
        isDoingSomething = true;
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN && isDoingSomething)
            return;
        isDoingSomething = true;
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
}
