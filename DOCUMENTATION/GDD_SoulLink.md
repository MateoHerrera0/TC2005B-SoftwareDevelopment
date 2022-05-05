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

What kinds of colors will you be using? Do you have a limited palette to work with? A post-processed HSV map/image? Consistency is key for immersion.

What kind of graphic style are you going for? Cartoony? Pixel-y? Cute? How, specifically? Solid, thick outlines with flat hues? Non-black outlines with limited tints/shades? Emphasize smooth curvatures over sharp angles? Describe a set of general rules depicting your style here.

Well-designed feedback, both good (e.g. leveling up) and bad (e.g. being hit), are great for teaching the player how to play through trial and error, instead of scripting a lengthy tutorial. What kind of visual feedback are you going to use to let the player know they&#39;re interacting with something? That they \*can\* interact with something?

### **Graphics Needed**

1. Characters
    1. Human-like
        1. Goblin (idle, walking, throwing)
        2. Guard (idle, walking, stabbing)
        3. Prisoner (walking, running)
    2. Other
        1. Wolf (idle, walking, running)
        2. Giant Rat (idle, scurrying)
2. Blocks
    1. Dirt
    2. Dirt/Grass
    3. Stone Block
    4. Stone Bricks
    5. Tiled Floor
    6. Weathered Stone Block
    7. Weathered Stone Bricks
3. Ambient
    1. Tall Grass
    2. Rodent (idle, scurrying)
    3. Torch
    4. Armored Suit
    5. Chains (matching Weathered Stone Bricks)
    6. Blood stains (matching Weathered Stone Bricks)
4. Other
    1. Chest
    2. Door (matching Stone Bricks)
    3. Gate
    4. Button (matching Weathered Stone Bricks)

_(example)_


## _Sounds/Music_

---

### **Style Attributes**

Again, consistency is key. Define that consistency here. What kind of instruments do you want to use in your music? Any particular tempo, key? Influences, genre? Mood?

Stylistically, what kind of sound effects are you looking for? Do you want to exaggerate actions with lengthy, cartoony sounds (e.g. mario&#39;s jump), or use just enough to let the player know something happened (e.g. mega man&#39;s landing)? Going for realism? You can use the music style as a bit of a reference too.

 Remember, auditory feedback should stand out from the music and other sound effects so the player hears it well. Volume, panning, and frequency/pitch are all important aspects to consider in both music _and_ sounds - so plan accordingly!

### **Sounds Needed**

1. Effects
    1. Soft Footsteps (dirt floor)
    2. Sharper Footsteps (stone floor)
    3. Soft Landing (low vertical velocity)
    4. Hard Landing (high vertical velocity)
    5. Glass Breaking
    6. Chest Opening
    7. Door Opening
2. Feedback
    1. Relieved &quot;Ahhhh!&quot; (health)
    2. Shocked &quot;Ooomph!&quot; (attacked)
    3. Happy chime (extra life)
    4. Sad chime (died)

_(example)_

### **Music Needed**

1. Slow-paced, nerve-racking &quot;forest&quot; track
2. Exciting &quot;castle&quot; track
3. Creepy, slow &quot;dungeon&quot; track
4. Happy ending credits track
5. Rick Astley&#39;s hit #1 single &quot;Never Gonna Give You Up&quot;

_(example)_


## _Schedule_

---

_(define the main activities and the expected dates when they should be finished. This is only a reference, and can change as the project is developed)_

1. develop base classes
    1. base entity
        1. base player
        2. base enemy
        3. base block
  2. base app state
        1. game world
        2. menu world
2. develop player and basic block classes
    1. physics / collisions
3. find some smooth controls/physics
4. develop other derived classes
    1. blocks
        1. moving
        2. falling
        3. breaking
        4. cloud
    2. enemies
        1. soldier
        2. rat
        3. etc.
5. design levels
    1. introduce motion/jumping
    2. introduce throwing
    3. mind the pacing, let the player play between lessons
6. design sounds
7. design music

_(example)_
