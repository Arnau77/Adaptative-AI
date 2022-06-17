# Adaptative AI (Final Degree Project)
 
**Author:** Arnau Falgueras Garcia de Atocha

This project consists of a turn-based RPG combat against an AI that has diferents levels that change depending of how well the player is playing.

## Character Stats
Each character has the following stats:
 - HP: the health points, when it reaches 0 the character loses.
 - Mana: these points are used to do special actions that can help the character win the combat.
 - Attack: this stat is used to determine how much damage an attack can do to the enemy, the higher the stat, the higher the damage.
 - Defense: this stat is used to determine how much damage is received when the enemy attacks, the higher the stat, the lower the damage.
 - Speed: this stat is used to determine which character will do its action first, if both characters have the same speed, the first one will be decided randomly.

## Character actions
In the combat you can do these following actions:
 - Attack to damage the enemy.
 - Recover mana (a maximum of 10 times).
 - Different spells that consume mana, the cost of the spell is displayed in the lower right part of the spell's button:
    - Special attack that does more damage than a normal attack.
    - Heal to recover HP.
    - Defend to receive half of the damage for a turn (until you do another action).
    - Increase stats to increase your attack, defense and speed.
    - Decrease stats to decrease your enemy's attack, defense and speed.

## Menus

If you push "Esc" button you will access a menu where you will be able to restart a combat, quit the game or access the options menu.
The option menu will have these two options:
 - Change AI level, where you will be able to change it between the level 0 and the level 10.
 - Change the number of consecutive victories or loses needed to change automatically the AI level, being an integer number greater or equal to 0.
