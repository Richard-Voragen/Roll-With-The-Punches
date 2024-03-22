# The title of your game #

## Summary ##

**A paragraph-length pitch for your game.**

**Roll With The Punches** is a platforming run and gun starring our main character, a boxer named **Arthur**. After a less-than-successful start to his career, he was humiliated in a match against the **Champ**. After suffering a huge blow to his pride, he came upon golden gloves that promised him the power to defeat anyone... at a cost that he could never lose again. With desperation, he agrees and thus inherits the elemental powers of electricity, fire, and ice, which is a fundamental aspect of the gameplay, throughout the venture **Arthur** will be up against a multitude of **The Champ's** lackeys, which is where type matchups will come into play. He aims to traverse through the underground fight club owned by **The Champ** and take him down to reclaim his pride and fulfill his contract with the golden gloves. The stages will include a multitude of projectiles, obstacles, and platforming, but with the help of the golden gloves, **Arthur** just might have a shot. 

## Project Resources

[Web-playable version of your game.](https://itch.io/)  
[Trailor](https://youtube.com)  
[Press Kit](https://dopresskit.com/)  
[Proposal: make your own copy of the linked doc.](https://docs.google.com/document/d/1qwWCpMwKJGOLQ-rRJt8G8zisCa2XHFhv6zSWars0eWM/edit?usp=sharing)  

## Gameplay Explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. Explaining the button mappings and the most optimal gameplay strategy is encouraged.**
D- Right, A- Left, S- Duck, Space- Jump, S+Space- Crouch Dodge, Left Click(Hold)- Attack (Projectile NOT Melee), Right Click- Change Types. 

After the prologue, you are dropped into a tutorial that should teach you fundamental aspects of the game such as button mapping. However, to most optimally play the game is to understand the strengths of lightning and crouch jumping. When you are dropped into the first stage, you are greeted with a drone that drops projectiles and an enemy that will chase you and launch fireballs at you. Though to a beginner it seems daunting, lightning inflicts stun on the enemy, and is the most effective way to hit pesky drones and eliminate them. Remove drones ASAP, as they are especially difficult to deal with when also facing enemies. As you progress through the stage you will find that having electricity set before you jump to new platforms is the most effective approach, as you will frequently find yourself jumping to a spot on the platform where you cannot see the enemies. Speaking of, each enemy has a distinct weakness and type of matchup, which is water beats fire, electricity stuns water for longer leaving it more vulnerable, and electricity is weak to water. However, electric enemy projectiles do not inflict damage, they inflict stun and will then rush you so you take damage as you collide with them.  
**Add it here if you did work that should be factored into your grade but does not fit easily into the proscribed roles! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

# Main Roles #

Producer, Enemy Movement(Assisted), Game Logic(Assisted) : Richard Voragen

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least four such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The game's background consists of procedurally generated terrain produced with Perlin noise. The game can modify this terrain at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## Producer (Richard Voragen)

As producer I was tasked with ensuring that my group was up to date on the plan with the game and caught up with deadlines. In order to do this I created a detailed [Gantt spreadsheet](https://onedrive.live.com/edit?id=61845D5D8EC1E8FA!7433&resid=61845D5D8EC1E8FA!7433&ithint=file%2Cxlsx&nav=MTVfezFBOUJFQUQwLTc4NkEtNEYzQS1CQ0QzLURBNzQ0NENCOTk0MX0&authkey=!AEAJLLwJvmdem0s&wdo=2&cid=61845d5d8ec1e8fa) detailing all of tasks each group member was tasked to do and when those tasks had to be submitted as well as who completed each task, updates on each task, and topics discussed during team meetings. <br> 
Beyond that I organized weekly meetings on those deadline days where each group member would show off what they have completed. Every Tuesday I would check in with each group member every Tuesday to remind them what is due that Thurday and I would message each group member on Thursdays to assign them their work for the week.<br>
Along with organizing those meetings and updates I also helped a lot with the coding when group members fell behind schedule due to school or other personal reasons since it was my job to keep the group on schedule.

For player movement I implemented the following scripts:
1. [ADSRManager.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Player/ADSRManager.cs) is similar (in fact a modified version) of the script we covered in the class about ADSR. It handles movement for left to right for the player and handles jumping and taking knockback. When the player leaves the ground the box collider is set to be a trigger, this allows for the ability to crouch jump through enemies and jump through platforms. Jumping just applies a force in the y-direction to the player's rigidbody. Finally crouching just shrinks the player's box collider.

These following scripts are for enemy movement. Some of these were modified versions of Jared's scripts but these ended up being the final versions in the game:

1. [DroneController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/Drone/DroneController.cs) This script is a modified version of the ADSRManager.cs. It makes the drone hover over the player by accelerating towards the player and then decelerating once the drone has passed the player. The height is calculated by using a raycast that searches for the ground and moves towards or away from it.
2. [BombController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/Drone/BombController.cs) This script simply translates the angle of the bomb tangent to it's velocity. When it's trigger enter's the player's hitbox it calls the player's TakeDamage() function. The bomb is a fire type attack.
3. [ElectricEnemyController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/Electric/ElectricEnemyController.cs) This script is for the Electric type enemy. The enemy will constantly run left to right on a platform. It sends a raycast downwards from the center of it's sprite and if the raycast cannot collide with ground then the enemy changes direction. The enemy will also fire a projectile every fireballCooldown amount of time.
4. [EnemyLightning.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/Electric/EnemyLightning.cs) This is the controller for the lightning attack. It sends the lightning in the direction given to it in the direction variable. When the lightning collides with the player it inflicts an electric type attack on the player. If the player collides with the enemy, it inflicts damage to the player.
5. [FireEnemyController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/Fire/FireEnemyController.cs) This script makes the fire enemy run towards the player if the player is more than 1 unit away from it. It will also fire a projectile every fireballCooldown seconds. If the y-value of the player is greater than the y-value of the enemy, the enemy will attempt to jump. If the player collides with the enemy, it inflicts a fire type damage to the player.
6. [EnemyFireball.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/Fire/EnemyFireball.cs) This is the controller for the fireball attack. It sends the fireball in the direction given to it in the direction variable. When the fireball collides with the player it inflicts an fire type attack on the player.
7. [WaterEnemyController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/Ice/WaterEnemyController.cs) The water enemy does not move left and right, it simply just attacks every waterballCooldown seconds. If the y-value of the player is greater than the y-value of the enemy, the enemy will attempt to jump. If the player collides with the enemy, it inflicts damage to the player.
8. [EnemyWaterball.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/Ice/EnemyWaterball.cs) This controller finds the angle between the player and the fireball and then lerps to rotate the fireball towards the play while moving forward. This allows the waterball to track towards the player while not turning too quickly. When the waterball collides with the player it inflicts an water type attack on the player.
9. [IEnemyController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/IEnemyController.cs) This is an interface similar to the one from the pirate homework assignment. It ensures that each enemy controller has a stun, death, and SetUpProcess class. The SetUpProcess class is meant to randomize each enemy's stats, the death class handles how an enemy should die, and the stun class handles what happens when an enemy is hit by lightning.


These following scripts are for the game logic:

1. [EnemySpawn.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/EnemySpawn.cs) This script is similar to the data class we made for the 4th homework asignment. It stores a prefab of the enemy, it's spawn location, and a few other variables to store a timer for the spawn object. When the timer finishes a new enemy of that type can be spawned.
1. [EnemyFactory.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/EnemyFactory.cs) This script is similar to the factories we made for the 4th homework asignment. It stores a list of EnemySpawn objects and when the player enters the factory's trigger, all of the EnemySpawn objects are spawned in the world at their relative positions. The EnemyFactory is also in charge of incrementing the timers on each EnemySpawn and spawning a new enemy if the timers are completed.
3. [PlayerDamageEngine.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Player/PlayerDamageEngine.cs) This script is attached to the player, when the TakeDamage function is called the player will lose one health point. When the player has no health the script will call the player's death function (which is in ADSRManager.cs).
4. [EnemyDamageEngine.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/EnemyDamageEngine.cs) This script is attached to each enemy, when the TakeDamage function is called the enemy will look up a damage table (similar to the 4th homework assignment) to see how much damage should be subtracted from the enemy's health.
5. [PositionLockCamera.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Camera/PositionLockCameraController.cs) & [AbstractCameraController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Camera/AbstractCameraController.cs) These scripts were simply copied from the second homework assignment. It sets the location of the camera 2 units above the location of the player.
6. [DialogueController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Camera/DialogueController.cs) This script changes the text on screen during the tutorial section of the game. When the Fire1 button is pressed it will envoke an enumerator that prints the text one letter at a time until the sentence is complete.
7. [TutorialCameraController.cs](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Camera/TutorialCameraController.cs) This is similar to the camera controllers we made during the second homework assignment. During the tutorial section of the game, if the NextImage() function is called then this script will scroll the camera over to a pre-set position by updating it's position in small increments in the update function.



## User Interface and Input

**Describe your user interface and how it relates to gameplay. This can be done via the template.**
**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals

**List your assets, including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Game Logic

**Document the game states and game data you managed and the design patterns you used to complete your task.**

# Sub-Roles
Audio: Richard Voragen

## Audio

The songs that were used are listed below. Some touch-ups were done in Audacity to the music to remove static from the recordings.<br>
1. [Buggle Call Rag](https://www.loc.gov/item/jukebox-16321/) by Victor Military Band. This song was recorded in 1916 so under the Music Modernization Act it is free to use in the public domain.
2. [My Own Iona](https://www.loc.gov/item/jukebox-21051/) by Victor Military Band. This song was recorded in 1916 so under the Music Modernization Act it is free to use in the public domain.

The sound effects are listed below. Pitch and volume have been modified in Unity.<br>
1. Fireball attack sound: [fireball.wav](https://freesound.org/people/perduuus/sounds/701804/) by [perduuus](https://freesound.org/people/perduuus/). The licence is Attribution Noncommercial 4.0, it is free to use for noncommercial purposes with credit given to the author.
2. Fireball hit sound: [Fireball Explosion.wav](https://freesound.org/people/HighPixel/sounds/431174/) by [HighPixel](https://freesound.org/people/HighPixel/). The licence is Creative Commons 0, it is free to use for anyone.
3. Waterball attack sound: [Freeze](https://freesound.org/people/JustInvoke/sounds/446112/) by [JustInvoke](https://freesound.org/people/JustInvoke/). The licence is Attribution 4.0, it is free to use with credit given to the author.
4. Waterball hit sound: [ICEImpt_ImpactBreak01_InMotion](https://freesound.org/people/InMotionAudio/sounds/719169/) by [InMotionAudio](https://freesound.org/people/InMotionAudio/). The licence is Creative Commons 0, it is free to use for anyone.
5. Lightning zap sound: [FX_Red_Laser_Shot_5.wav](https://freesound.org/people/JakeAMT/sounds/681974/) by [JakeAMT](https://freesound.org/people/JakeAMT/). The licence is Creative Commons 0, it is free to use for anyone.
6. Bomb/drone exploding sound: [Explosion 4.wav](https://freesound.org/people/theplax/sounds/560575/) by [theplax](https://freesound.org/people/theplax/). The licence is Attribution 4.0, it is free to use with credit given to the author.
7. Player taking damage sound: [Casual Death Loose](https://freesound.org/people/GameAudio/sounds/220203/) by [GameAudio](https://freesound.org/people/GameAudio/). The licence is Creative Commons 0, it is free to use for anyone.

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

## Game Feel and Polish

**Document what you added to and how you tweaked your game to improve its game feel.**
