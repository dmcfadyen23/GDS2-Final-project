using System.Collections.Generic;
using Attacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chef : Enemy
{
    // two attacks, one regular damage (30 damage) and one that heals player
    [SerializeField] private List<AttackSO> attacks;
    private bool healTurn;
    private enum OvenStatus
    {
        Dull, Fire, LargeFire, Explosion
    }

    private OvenStatus ovenStatus = OvenStatus.Fire;
    [SerializeField] public TextMeshProUGUI customDialogue;
    private SpriteRenderer enemyBg;
    
    public override void UseAttack(Player targetPlayer)
    {
        attackContext.target = targetPlayer;
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        if (healTurn)
        {
            attacks[0].Attack(attackContext);
        }
        else
        {
            attacks[1].Attack(attackContext);
        }

        healTurn = !healTurn;
    }

    public override float TakeDamage(AttackSO attack, AttackContext context)
    {
        // takes half damage from everything
        float damageTaken = attack.GetDamageDealt()*0.5f;
        
        // special interaction with fire attack (hit by fire enough times = instant win, but increased attack)
        if (attack.attackName == "Fireball")
        {
            damageTaken = 0;
            customDialogue.text = "Haha! Yes! That's it! ";
            if (ovenStatus != OvenStatus.Explosion)
            {
                ovenStatus++;
            }
            switch (ovenStatus)
            {
                case OvenStatus.Fire:
                    enemyBg.color = new Color(255, 140,48, 204);
                    customDialogue.text += "Let's get this fire STARTED";
                    attackStat = 100;
                    break;
                case OvenStatus.LargeFire:
                    enemyBg.color = new Color(225, 107, 48, 180);
                    customDialogue.text += "Wow, this fire is ROARING!";
                    attackStat = 125;
                    break;
                case OvenStatus.Explosion:
                    enemyBg.color = new Color(243,54,37,216);
                    customDialogue.text = "Uh oh! Looks like things got too hot to handle!";
                    break;
            }
        }
        // special interaction with water, lowers attack stat
        else
        {
            if (attack.attackName == "Waterfall")
            {
                customDialogue.text = "Oh no! My oven! ";
                if (ovenStatus != OvenStatus.Dull)
                {
                    ovenStatus--;
                }

                switch (ovenStatus)
                {
                    case OvenStatus.Dull:
                        enemyBg.color = new Color(38,133,213,140);
                        customDialogue.text += "It's all gone!";
                        attackStat = 75;
                        break;
                    case OvenStatus.Fire:
                        enemyBg.color = new Color(255, 140,48, 204);
                        customDialogue.text += "The temperature isn't hot enough!";
                        attackStat = 100;
                        break;
                    case OvenStatus.LargeFire:
                        enemyBg.color = new Color(225, 107, 48, 180);
                        customDialogue.text += "I was at the perfect temperature!";
                        attackStat = 125;
                        break;
                    
                }
            }
        }
        health -= damageTaken;
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.UpdateEnemyHealthBar(health, enemyNum);
        
        // if enough fire attacks, instantly win
        if (ovenStatus == OvenStatus.Explosion)
        { 
            health = 0;
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            // check if all enemies dead
            bool allDead = true;
            CombatManager combatManager = FindAnyObjectByType<CombatManager>();
            GameObject[] enemies = combatManager.GetAllEnemies();
            foreach (var VARIABLE in enemies)
            {
                if (VARIABLE.activeSelf)
                {
                    allDead = false;
                }
            }

            if (allDead)
            {
                CombatManager.WinCombat();
            }
        }
        return damageTaken;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        GameObject bgObject = GameObject.FindWithTag("EnemyBg");
        customDialogue = GameObject.FindWithTag("CustomDialogue").GetComponentInChildren<TextMeshProUGUI>();
        customDialogue.text = "Oh? Have you brought new ingredients for me?";
        enemyBg = bgObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
