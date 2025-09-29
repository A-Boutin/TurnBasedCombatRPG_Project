# Turn-Based Combat RPG Project
Taking heavy inspiration from the mechanics of Dark Souls: The Board Game, this is project attempts to create a modular system for enemy and player design alike that is also easily portable to a mobile game. 

Enemies and players are both given "Actions" which tells them the way they are allowed to move and attack on the board.  
An enemy will always have the same Action and therefore always perform their turn in a predictable way. This allows for players to strategize how they would like to spend their own turn.  
Players can get Weapons which will have their own Actions attached to them. Most weapons will have multiple Actions that can be taken for different costs to the player. Players that are out of combat can change Weapons, effectively changing their Actions, as much as they'd like before entering combat again.  
To balance this, enemies will perform their Actions much more often than the player.

---
## Actions
[showcase and explain action system in depth]
There are two main Actions that are performed in this project and in the original game this is based on. These Actions are the Move Action and the Attack Action.
Actions can be placed in any order and can be repeated as many times as desired.

### Move Action
[showcase the move any and explain what's happening]
[showcase the move left and explain what's happening]

### Attack Action
There are a few parts to the Attack Action:
- Target: This holds the same definition as that of the Move Action.
- Dmg Set (Damage Set): This is a list of Dice (for randomness) & a modifier (for concrete) to determine to amount of damage to be dealt.
- Range: This determines how close, in terms of nodes, the target has to be in order to able to be hit
- AOE (Area Of Effect): If this box is checked, the amount of damage dealt is dealt to everything in the node instead of just the a single target.
- Shaft: If this box is checked, this attack cannot be performed on nodes with a range of 0 (usually the same node the attacker is on).
- Magic: If this box is checked, this attack will do magic damage instead of physical damage.
- Push: If this box is checked, this attack will also move the target 1 node away from the attacker.
When combined, these parts create a flexible attack system where one attack can differ greatly from another.

[showcase the action in use and explain what's happening]
[picture of the move action described]
[picture of the attack action described]
