using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    enum Options
    {
        NONE = 0,
        ATTACK,
        DEFENSE,
        HEAL,
        SPECIAL_ATTACK,
        RECOVER_MANA,
        INCREASE_STATS,
        DECREASE_STATS
    }

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
    public Slider lifeSlider = null;
    public Slider manaSlider = null;
    int life;
    int speed;
    int attack;
    int defense;
    int mana;
    int levelOfChangeStats = 0;
    bool firstFrame = true;
    float defendingBonus = 1;
    Options optionChoosen;


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

        if (optionChoosen == Options.NONE)
        {
            if (firstFrame)
            {
                Debug.Log(playerName + " please choose an option for this turn.");
            }
            firstFrame = false;
            endTurn = DecideOption();
            return;
        }


        Debug.Log("My turn: " + playerName);
        ExecuteOption();
        optionChoosen = Options.NONE;
        endTurn = true;
        firstFrame = true;
        Debug.Log("A turn has passed: " + playerName);
    }

    void ExecuteOption()
    {
        defendingBonus = 1;
        switch (optionChoosen)
        {
            case Options.ATTACK:
                Attack(gameManager.basicAttackDamage);
                break;
            case Options.DEFENSE:
                mana -= gameManager.manaSpentWithDefense;
                manaSlider.value = mana;
                defendingBonus = defendingBonus + ((float)gameManager.percentageDefense / 100f);
                break;
            case Options.SPECIAL_ATTACK:
                mana -= gameManager.manaSpentWithSpecialAttack;
                manaSlider.value = mana;
                Debug.Log(playerName + " has " + mana + " of mana");
                Attack(gameManager.manaAttackDamage);
                break;
            case Options.RECOVER_MANA:
                RecoverMana();
                break;
            case Options.HEAL:
                mana -= gameManager.manaSpentWithHealing;
                manaSlider.value = mana;
                Heal();
                break;
            case Options.INCREASE_STATS:
                mana -= gameManager.manaSpentWithIncreasingStats;
                manaSlider.value = mana;
                ChangeStats(true, this);
                levelOfChangeStats++;
                break;
            case Options.DECREASE_STATS:
                mana -= gameManager.manaSpentWithDecreasingStats;
                manaSlider.value = mana;
                ChangeStats(false, enemy);
                enemy.levelOfChangeStats--;
                break;
        }
    }

    bool DecideOption()
    {
        bool optionDecided = true;
        if (Input.GetKeyDown(KeyCode.A))
        {
            optionChoosen = Options.ATTACK;
        }
        else if (Input.GetKeyDown(KeyCode.S) && mana >= gameManager.manaSpentWithSpecialAttack)
        {
            optionChoosen = Options.SPECIAL_ATTACK;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            optionChoosen = Options.RECOVER_MANA;
        }
        else if (Input.GetKeyDown(KeyCode.M) && levelOfChangeStats < gameManager.maxOfTimesChangingLevelOfStats && mana >= gameManager.manaSpentWithIncreasingStats)
        {
            optionChoosen = Options.INCREASE_STATS;
        }
        else if (Input.GetKeyDown(KeyCode.L) && enemy.levelOfChangeStats > -gameManager.maxOfTimesChangingLevelOfStats && mana >= gameManager.manaSpentWithDecreasingStats)
        {
            optionChoosen = Options.DECREASE_STATS;
        }
        else if (Input.GetKeyDown(KeyCode.H) && mana >= gameManager.manaSpentWithHealing)
        {
            optionChoosen = Options.HEAL;
        }
        else if (Input.GetKeyDown(KeyCode.D) && mana >= gameManager.manaSpentWithDefense)
        {
            optionChoosen = Options.DEFENSE;
        }
        else
        {
            optionDecided = false;
        }
        return optionDecided;
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
        lifeSlider.value = life;

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
        manaSlider.value = mana;
        Debug.Log(playerName + " has " + mana + " of mana");
    }
    void Attack(int attackDamage)
    {
        int damage = (attack - (int)(enemy.defense * enemy.defendingBonus)) * attackDamage;
        enemy.TakeDamage(damage < 0 ? 0 : damage);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        life = life < 0 ? 0 : life;
        Debug.Log(playerName + "has a life of " + life);
        if (life <= 0)
        {
            dead = true;
        }
        lifeSlider.value = life;
    }

    public int getSpeed() { return speed; }

    public void Reset()
    {
        life = initialLife;
        lifeSlider.value = life;
        attack = initialAttack;
        defense = initialDefense;
        speed = initialSpeed;
        mana = initialMana;
        manaSlider.value = mana;
        endTurn = true;
        playerTurn = false;
        dead = false;
        optionChoosen = Options.NONE;
        defendingBonus = 1;
    }
}
