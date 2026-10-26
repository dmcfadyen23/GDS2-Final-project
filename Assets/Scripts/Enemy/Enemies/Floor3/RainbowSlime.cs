using System.Collections.Generic;
using Attacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RainbowSlime : Enemy
{
    // three attacks, one regular damage, one that heals self, one that reduces player attack
    [SerializeField] public List<AttackSO> attacks;
    private enum SlimeColour
    {
        Red, Green, Blue
    }

    private SlimeColour currentColour = SlimeColour.Red;
    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI customDialogue;
    private SpriteRenderer enemyBg;

    public override void UseAttack(Player targetPlayer)
    {
        attackContext.target = targetPlayer;
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        Debug.Log(currentColour);
        switch (currentColour)
        {
            case SlimeColour.Red:
                attackStat = 150;
                attacks[0].Attack(attackContext);
                currentColour = SlimeColour.Green;
                spriteRenderer.color = new Color32(199, 255, 0, 255);
                break;
            case SlimeColour.Green:
                attackStat = 100;
                attacks[1].Attack(attackContext);
                
                currentColour = SlimeColour.Blue;
                spriteRenderer.color = new Color32(255, 255, 255, 255);
                break;
            case SlimeColour.Blue:
                attacks[2].Attack(attackContext);
                currentColour = SlimeColour.Red;
                
                spriteRenderer.color = new Color32(255, 0, 0,255);
                break;
        }
    }

    public override float TakeDamage(AttackSO attack, AttackContext context)
    {
        float damageTaken = attack.GetDamageDealt();
        // special interaction with colour attack (hit by same colour multiple times = win), use wrong coloured attack, lose health
        switch (currentColour)
        {
            case SlimeColour.Red:
                if (attack.colour == Color.red)
                {
                    damageTaken *= 2;
                }
                else
                {
                    context.sourceUnit.health -= 20;
                    damageTaken *= 0.5f;
                }

                break;
            case SlimeColour.Green:
                if (attack.colour == Color.green)
                {
                    damageTaken = 20;
                    context.sourceUnit.health += 20;
                }
                else
                {
                    context.sourceUnit.health -= 20;
                }

                break;
            case SlimeColour.Blue:
                if (attack.colour == Color.blue)
                {
                     health -= 20;
                    debuffs.Add(("Paralyze", 3));
                }
                else
                {
                    context.sourceUnit.health -= 20;
                    damageTaken *= 0.5f;
                }

                break;
        }

        health -= damageTaken;
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.UpdateEnemyHealthBar(health, enemyNum);
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(255, 0, 0, 255);
        // GameObject.FindWithTag("CustomDialogue").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
