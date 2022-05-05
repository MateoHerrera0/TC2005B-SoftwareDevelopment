# **Soul Link**

## _Game Design Document_

---

##### **Copyright notice / author information / boring legal stuff nobody likes**

##
## _Index_

---

1. [Index](#index)
2. [Game Design](#game-design)
    1. [Summary](#summary)
    2. [Gameplay](#gameplay)
    3. [Mindset](#mindset)
3. [Technical](#technical)
    1. [Screens](#screens)
    2. [Controls](#controls)
    3. [Mechanics](#mechanics)
4. [Level Design](#level-design)
    1. [Themes](#themes)
        1. Ambience
        2. Objects
            1. Ambient
            2. Interactive
        3. Challenges
    2. [Game Flow](#game-flow)
5. [Development](#development)
    1. [Abstract Classes](#abstract-classes--components)
    2. [Derived Classes](#derived-classes--component-compositions)
6. [Graphics](#graphics)
    1. [Style Attributes](#style-attributes)
    2. [Graphics Needed](#graphics-needed)
7. [Sounds/Music](#soundsmusic)
    1. [Style Attributes](#style-attributes-1)
    2. [Sounds Needed](#sounds-needed)
    3. [Music Needed](#music-needed)
8. [Schedule](#schedule)

## _Game Design_

---

### **Summary**

Given the difficulty in teaching video game design, Soul Link is an intuitive and simple dungeon crawler type game in which the user has the ability to create a dungeon and play in it. Said dungeon can have many “rooms”, enemies and traps chosen by the game master (user). The player will need to use only one arrow to traverse the deadly maze, shooting it and recalling it as they go. The target audience for this game is anyone who is interested in game design or simply likes dungeon crawler type games.

### **Gameplay**

Being a video game focused on design, the gameplay consists of the use of tools (obstacles and enemies) to create a level. The user is free to add the elements as they wish (considering limits such as not adding two elements in the same place) and, at the end, then test their level. Specifically, the user will be able to add rooms to the general map, and subsequently choose a room to edit and insert elements in it. 

Regarding the built level itself, the gameplay is based around classic dungeon crawler mechanics. The character has the ability to shoot at enemies with an arrow that the player will need to recall before being able to shoot again. This shoot/recall action is the way in which the player will deal damage to enemies, and it is the main mechanic the game designers will have to keep in mind in order to create interesting levels. Furthermore, the player will also be able to explore the dungeon using the WASD keys, as well as dodge incoming damage with the spacebar key.

The main goal of the game is to reach the final room inside the dungeon and beat the boss inside the room, which the game designer will have the opportunity to customize. To do this, the player will have to explore the rooms inside the dungeon and complete the objectives set in the individual rooms. Upon completion, the game will provide relevant statistics to the player, such as time taken for completion and points acquired during gameplay, as well as presenting them with the options to exit to the main menu or retry the level.

### **Mindset**

Regarding the game design, we want the user to feel creative, comfortable and adventurous. This will be achieved by having enough customizable game options to explore their creativity and develop a sense of game design, as well as providing them with an intuitive platform that will motivate them to keep creating even when frustrating design choices may come up.

As for the player experience, we intend to provoke excitement and, to a certain extent, uncertainty. This will happen organically, as the levels will vary in difficulty and complexity depending on the creations different game designers come up with. Furthermore, we plan to keep the players entertained by giving them responsive game mechanics and an ever decreasing health bar that will need to be replenished by killing enemies.

### **Lore & Background**

Bingu: they follow their instinct constantly, often distinguishing right from wrong. Bingu strives to be like their late deceased mother while behaving a bit recklessly like his father. Bingu has always been curious and adventurous, he does not scare easily.

The night: The day that Bingu fell into the cave, they were returning home when suddenly, a light appeared in front of them. Bingu feels drawn to follow the light, and as the luminous body flys through the darkness of the night Bingu chases without a second thought. Bingu, distracted by the chase and their feelings, falls through a hole in the ground, finding themselves in a cave that has no apparent exit.

The Arrow: after falling into the cave, an arrow of mysterious origin appears for Bingu to use fending off from the enemy’s that lurk in the dark. It’s powers and limits are unknown.

The light: finding the light is Bingu’s purpose, they fell into the cave following it. The question of who the light is is persistent, as Bingu has the feeling that the light's presence is familiar, maybe a parental figure long lost to them.

## _Technical_

---

### **Screens**

1. Title Screen (Home)
2. Play Game
    1. Choosing “Jugar”
    2. HUD
    3. Pause
3. Level Creation 
    1. Map builder (Choosing “Crear Nivel”)
    2. Room builder (Entering room with double click)
5. End Credits

_(example)_

### **Controls**

How will the player interact with the game? Will they be able to choose the controls? What kind of in-game events are they going to be able to trigger, and how? (e.g. pressing buttons, opening doors, etc.)

### **Mechanics**

Are there any interesting mechanics? If so, how are you going to accomplish them? Physics, algorithms, etc.

## _Level Design_

---

_(Note : These sections can safely be skipped if they&#39;re not relevant, or you&#39;d rather go about it another way. For most games, at least one of them should be useful. But I&#39;ll understand if you don&#39;t want to use them. It&#39;ll only hurt my feelings a little bit.)_

### **Themes**

1. Forest
    1. Mood
        1. Dark, calm, foreboding
    2. Objects
        1. _Ambient_
            1. Fireflies
            2. Beams of moonlight
            3. Tall grass
        2. _Interactive_
            1. Wolves
            2. Goblins
            3. Rocks
2. Castle
    1. Mood
        1. Dangerous, tense, active
    2. Objects
        1. _Ambient_
            1. Rodents
            2. Torches
            3. Suits of armor
        2. _Interactive_
            1. Guards
            2. Giant rats
            3. Chests

_(example)_

### **Game Flow**

1. Player starts in forest
2. Pond to the left, must move right
3. To the right is a hill, player jumps to traverse it (&quot;jump&quot; taught)
4. Player encounters castle - door&#39;s shut and locked
5. There&#39;s a window within jump height, and a rock on the ground
6. Player picks up rock and throws at glass (&quot;throw&quot; taught)
7. … etc.

_(example)_

## _Development_

---

### **Abstract Classes / Components**

1. BasePhysics
    1. BasePlayer
    2. BaseEnemy
    3. BaseObject
2. BaseObstacle
3. BaseInteractable

_(example)_

### **Derived Classes / Component Compositions**

1. BasePlayer
    1. PlayerMain
    2. PlayerUnlockable
2. BaseEnemy
    1. EnemyWolf
    2. EnemyGoblin
    3. EnemyGuard (may drop key)
    4. EnemyGiantRat
    5. EnemyPrisoner
3. BaseObject
    1. ObjectRock (pick-up-able, throwable)
    2. ObjectChest (pick-up-able, throwable, spits gold coins with key)
    3. ObjectGoldCoin (cha-ching!)
    4. ObjectKey (pick-up-able, throwable)
4. BaseObstacle
    1. ObstacleWindow (destroyed with rock)
    2. ObstacleWall
    3. ObstacleGate (watches to see if certain buttons are pressed)
5. BaseInteractable
    1. InteractableButton

_(example)_

















































































## _Graphics_

---

### **Style Attributes**

The colors to be used in the scenes and the general aesthetic of the game are a palette of light to dark blues with gray and yellow (see the game logo as reference). Objects, the main character, and enemies might vary in colors, aiming to create contrast with the main scene and general style. Additionally, the graphic style to be used is cartoony, smooth curvatures, shaded/illuminated objects and black outlines only for elements (main character, enemies, objects). Elements that are repeated but have different characteristics, such as arrows, will change colors but will maintain their base style. 
The general color palette looks as follows: 


Interactions with different elements will look as follows: 
* Damage: when the player or an enemy gets damaged, they will flicker. 
* Door open: when a door gets opened, the screen will slightly shake. 
* New arrow: when the player holds a new arrow, the arrow section on the screen will change to the respective arrow.
* Change rooms: The player will be able to look at a map to see the current room and their position within it. This will appear on the top menu section. 
* Heal: when the player gets the ability to heal (takes a heart), the main character will quickly shine.


### **Graphics Needed**

1. Characters
    1. Player
        1. idle 
        2. walking
        3. shooting
        4. recalling
        5. rolling
        6. damage taken
        7. death
        8. 

2. Enemies
    1. Bat
        1. idle
        2. flying
        3. damage
        4. death
        5. 

    2. Ant
        1. idle
        2. crawling
        3. damage
        4. death
        5. 

    3. Slime
        1. idle
        2. sliding
        3. damage
        4. death
        5. 

    4. Ghost
        1. idle
        2. float
        3. damage
        4. death
        5. 

    5. Spider
        1. idle
        2. crawling
        3. damage
        4. death
        5. 

3. Room
    1. Walls
    2. Door (matching Stone Bricks)
4. Ambient
    1. Dirt surface
        1. 

    2. Pointy rocks / spike
        1. 

    3. Rocks
        1. 

    4. Bright rocks
        1. 

5. Other
    1. Arrow
        1. different colors
        2. recall
    2. Health
    3. PowerUps 
    4. 



## _Sounds/Music_

---

### **Style Attributes**

Instrumental vibes, nothing too exciting. Slow to medium tempo on normal gameplay, up the beat when fighting a boss. Actions have sounds, however nothing stands out more than the other, except for the chimes when the arrow is thrown.

### **Sounds Needed**

1. Effects
    1. Soft Footsteps (Payer movement on dirt floor)
    2. Whistling arrow through the air
    3. Flapping bat wings
    4. Gooey sound (slime enemy movement)
    5. Soft tickling (ant movement)
    6. Pitched tickling (spider movement)
    7. Thumping footsteps (boss movement)
2. Feedback
    1. Heal up sound (health power up)
    2. Heart pumping sound (heart health object)
    3. Enemy shriek (when attacking)
    4. Shocked "Huuuuhhh!" (attacked by enemy/boss)
    5. Normal Chime (when normal arrow is thrown)
    6. Hard-ice chime (when ice arrow is thrown)
    7. Pitched chime (when speed arrow is thrown)
    8. Grand chime (when ultimate arrow is thrown)
    9. Sad chime (Player died)
    10. Puff sound (Enemy died)
    11. Victory sound (Boss died)


### **Music Needed**

1. Slow-paced, nerve-racking "cave" track
2. Exciting somewhat upbeat “boss” track
3. Happy ending credits track


## _Schedule_

---

1. Game Beta -May 13th
    1. Draft of functional game (not builder)
2. Start software engineering process - May 13th 
    1. Use cases and requirements
3. User Backlog - May 20th 
    1. Complete the definition of requirements
4. Start Game Builder - May 20th 
    1. Define base classes
5. Define database structure - May 20th 
    1. Normalization 
6. UML documentation - May 27th 
7. Frontend - May 27th 
8. Backend - June 3rd
    1. Create database script 
    2. Connect APIs
9. Finish Game Builder - June 10th 
    1. Finish implementation
    2. Revise with AMEXVID
10. Finish Whole Game (Builder + Game) - June 10th 
    1. Design sounds
    2. Design music
    3. Implement Animations
    4. Fix Bugs
11. Final Video  - June 17th 
    1. Final presentation to AMEXVID

