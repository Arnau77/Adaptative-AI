using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool playerTurn = false;
    [HideInInspector] public bool endTurn = false;
    [HideInInspector] public bool dead = false;
    public string playerName = "";
    public Player enemy = null;
    public int initialLife = 100;
    public int initialSpeed = 10;
    public int initialAttack = 20;
    public int initialDefense = 10;
    public int initialMana = 50;
    public GameManager gameManager = null;
    int life;
    int speed;
    int attack;
    int defense;
    int mana;
    int levelOfChangeStats = 0;
    bool firstFrame = true;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTurn)
            return;

        if (firstFrame)
        {
            Debug.Log("My turn: " + playerName);
        }
        firstFrame = false;

        endTurn = true;
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack(gameManager.basicAttackDamage);
        }
        else if (Input.GetKeyDown(KeyCode.S) && mana >= gameManager.manaSpentWithSpecialAttack)
        {
            mana -= gameManager.manaSpentWithSpecialAttack;
            Debug.Log(playerName + " has " + mana + " of mana");
            Attack(gameManager.manaAttackDamage);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            RecoverMana();
        }
        else if (Input.GetKeyDown(KeyCode.M) && levelOfChangeStats < gameManager.maxOfTimesChangingLevelOfStats && mana >= gameManager.manaSpentWithIncreasingStats)
        {
            mana -= gameManager.manaSpentWithIncreasingStats;
            ChangeStats(true, this);
            levelOfChangeStats++;
        }
        else if (Input.GetKeyDown(KeyCode.L) && enemy.levelOfChangeStats > -gameManager.maxOfTimesChangingLevelOfStats && mana >= gameManager.manaSpentWithDecreasingStats)
        {
            mana -= gameManager.manaSpentWithDecreasingStats;
            ChangeStats(false, enemy);
            enemy.levelOfChangeStats--;
        }
        else if (Input.GetKeyDown(KeyCode.H) && mana >= gameManager.manaSpentWithHealing) 
        {
            mana -= gameManager.manaSpentWithHealing;
            Heal();
        }
        else
        {
            endTurn = false;
        }
        if (endTurn)
        {
            firstFrame = true;
            Debug.Log("A turn has passed: " + playerName);
        }
    }

    void Heal()
    {
        float percentageInDecimals = (float)gameManager.percentageHealing / 100f;
        float lifeToRecover = initialLife * percentageInDecimals;
        life += (int)lifeToRecover;
        if (life > initialLife)
        {
            life = initialLife;
        }
        Debug.Log(playerName + " has " + life + " of life");
    }

    void ChangeStats(bool positiveChange, Player playerToAffect)
    {
        int multiplierOfLevel = positiveChange ? 1 : -1;
        float percentageInDecimals = (float)gameManager.percentageChangingStats / 100f;
        float attackChanged= playerToAffect.initialAttack * percentageInDecimals * multiplierOfLevel;
        playerToAffect.attack += (int)attackChanged;
        float defenseChanged = playerToAffect.initialDefense * percentageInDecimals * multiplierOfLevel;
        playerToAffect.defense += (int)defenseChanged;
        float speedChanged = playerToAffect.initialSpeed * percentageInDecimals * multiplierOfLevel;
        playerToAffect.speed += (int)speedChanged;
        Debug.Log(playerToAffect.playerName + "now has " + playerToAffect.attack + " of attack, " + playerToAffect.defense + " of defense and " + playerToAffect.speed + " of speed");
    }

    void RecoverMana()
    {
        float percentageInDecimals = (float)gameManager.percentageRecoveryMana / 100f;
        float manaToRecover = initialMana * percentageInDecimals;
        mana += (int)manaToRecover;
        if (mana > initialMana)
        {
            mana = initialMana;
        }
        Debug.Log(playerName + " has " + mana + " of mana");
    }
    void Attack(int attackDamage)
    {
        int damage = (attack - enemy.defense) * attackDamage;
        enemy.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        Debug.Log(playerName + "has a life of " + life);
        if (life <= 0)
        {
            dead = true;
        }
    }

    public int getSpeed() { return speed; }

    public void Reset()
    {
        life = initialLife;
        attack = initialAttack;
        defense = initialDefense;
        speed = initialSpeed;
        mana = initialMana;
        endTurn = true;
        playerTurn = false;
        dead = false;
    }
}
