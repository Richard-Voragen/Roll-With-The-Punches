# Roll With The Punches #

## Summary ##

**A paragraph-length pitch for your game.**

**Roll With The Punches** is a platforming run and gun starring our main character, a boxer named **Arthur**. After a less-than-successful start to his career, he was humiliated in a match against the **Champ**. After suffering a huge blow to his pride, he came upon golden gloves that promised him the power to defeat anyone... at a cost that he could never lose again. With desperation, he agrees and thus inherits the elemental powers of electricity, fire, and ice, which is a fundamental aspect of the gameplay, throughout the venture **Arthur** will be up against a multitude of **The Champ's** lackeys, which is where type matchups will come into play. He aims to traverse through the underground fight club owned by **The Champ** and take him down to reclaim his pride and fulfill his contract with the golden gloves. The stages will include a multitude of projectiles, obstacles, and platforming, but with the help of the golden gloves, **Arthur** just might have a shot. 

## Project Resources

[Web-playable version of your game](https://jmg21ucd.itch.io/rolling-with-the-punches)

[Trailor](https://youtu.be/6IHbzBngjl0?si=dyH1bY0KTx7Go3g1)  

[Press Kit](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/Presskit.md)  

[Proposal: make your own copy of the linked doc.](https://docs.google.com/document/d/1c0Az5TF7P1AOS-_UADZltUwi-6YoYZbPrAL-sjL724I/edit#heading=h.i3tv2mxf7h7z)

## Gameplay Explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. Explaining the button mappings and the most optimal gameplay strategy is encouraged.**

D- Right, A- Left, S- Duck, Space- Jump, S+Space- Crouch Dodge, Left Click(Hold)- Attack (Projectile NOT Melee), Right Click- Change Types. 

  After the prologue, you are dropped into a tutorial that should teach you fundamental aspects of the game such as button mapping. However, to most optimally play the game is to understand the strengths of lightning and crouch jumping. When you are dropped into the first stage, you are greeted with a drone that drops projectiles and an enemy that will chase you and launch fireballs at you. Though to a beginner it seems daunting, lightning inflicts stun on the enemy, and is the most effective way to hit pesky drones and eliminate them. Remove drones ASAP, as they are especially difficult to deal with when also facing enemies. As you progress through the stage you will find that having electricity set before you jump to new platforms is the most effective approach, as you will frequently find yourself jumping to a spot on the platform where you cannot see the enemies. Speaking of, each enemy has a distinct weakness and type of matchup, which is water beats fire, electricity stuns water for longer leaving it more vulnerable, and electricity is weak to water. However, electric enemy projectiles do not inflict damage, they inflict stun and will then rush you so you take damage as you collide with them. This collision damage may seem threatening, especially when you are trapped against the end of a platform or wall, however, we have the solution in the form of a crouch jump, which provides invincibility frames and lets you jump past pursuing enemies. Lastly, to handle the final boss of the stage, alternate between lightning and water, the boss cannot be stunned but the homing missiles can be destroyed by lightning, and water will do some serious damage to the statue. And with that, you have beaten an infuriating stage. 
  
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

## User Interface and Level Design (Jason Kohl)

The user interface for our game consists of a health point counter and the current weapon that the player has selected. The health point counter is an important part of our game's gameplay as it helps the user keep track of how much more damage they are able to take before they die. The current weapon UI is also a very important part of the game's gameplay as it helps the user keep track of their current weapon and gives them the ability to know which attack they will perform when facing an enemy. To create the health point counter, I edited the player damage engine to make use of the already calculated player's health. By creating a link to the canvas text object in the player damage engine, I was able to display the player's current health in the bottom left corner of the screen (https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/1a87ba0605a13fd704c302e4e0f4c7b4c026671f/RollingWithThePunches/Assets/Scripts/Player/PlayerDamageEngine.cs#L11).

Since we allow the player to have different weapons, we have to be able to detect when the player wants to switch weapons. We accomplished this task by tracking the player's button presses and linked the fire2 (right click) command to swap the player's weapons. This means we also have to swap the current weapon UI that is above the player's health points counter. To do this we added code into the fire2 detection logic to track when fire2 is pressed and to then switch to the next weapon icon to match the current weapon that the player is holding. This was accomplished by editing the ShootFireball.cs file to set the correct UI weapon image to match the correct current weapon(https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/e32ea1f8b5804aaa2b87743b1ba98609c54a062d/RollingWithThePunches/Assets/Scripts/Player/ShootFireball.cs#L106).

![Fire](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/Presskit%20Images/fire.png)

![Water](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/Presskit%20Images/water.png)

![Electricity](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/Presskit%20Images/electric.png)

Level design had a big role in our game as it was the environment that the user was put into and interacted with throughout their time playing the game. The level design had to be both exciting enough for the user to find interesting, but also challenging as our game was also a platformer. To achieve this I researched other platformer games to see what kinds of environments and challenges they put into their games to make their games both fun and exciting to experience. One big part of many platformers is moving platforms. I was able to implement these items into the game via the upAndDown.cs and sideToSide.cs scripts. These scripts were attached to platforms that we wanted to move around to add challenge, and would detect when they collided with a bumper to know when to start moving the opposite way.The sideToSide.cs script (https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enivorment/sideToSide.cs) is used for all horizontal moving platforms. upAndDown.cs script is used for moving the platforms vertically (https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enivorment/upAndDown.cs), mainly in the section of the game where there are three vertical moving platforms that are all moving at once. 


## Movement/Physics (Jared Martinez)

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

Getting the movement to feel similar or the same as Cuphead was one of the most vital steps for this game. At first, I had to study and really refresh my memory on how the Cuphead characters moved in their world and replicate it from a pure game feel which is also ironic since that is my other role. 

Controls:
| Action | Button |
| ----------- | ----------- |
| Left | A |
| Right | D |
| Jump | Space |
| Duck | S |
| Roll/Dodge | S + Space |
| Shoot | Left Click (Hold) |
| Change Projectile | Right Click |
| Pause | Escape |

In terms of physics, we relied on Unity's built-in physics engine and attached rigid bodies to objects that needed some type of physics/force applied to them. Since this would be essentially a 2D platformer, the physics would not need to be very robust at all and thankfully because of that, it saved us quite a bit of headaches.

## Animation and Visuals (Pablo Rivera)

**List your assets, including their sources and licenses.**
Stage Asset Tileset: https://guardian5.itch.io/warehouse-tileset (Used for all elements minus the moving platforms which were made by me, also the concrete tile flooring was made by me as well)
Projectile Assets:
  Lightning: https://sanctumpixel.itch.io/lightning-lines-pixel-art-effect?download
  Fire and Water: https://www.gamedeveloperstudio.com/graphics/viewgraphic.php?item=1k4p441y0p3p8z9a5n
  
*Initial Concepts*: 
  We went into the project with a strong concept for a run-and-gun style game revolving around boxers to fit the theme of "rolling with the punches". After we established the core concept of the game, I went into research looking for brawler-type sprites to reference and use as a base for my animations. This led me to reference Scott Pilgrim Vs the World: The Game, where their expressive motions I sought to emulate in my work. ![image](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/8150ed7d-cb13-4aa7-99d1-cadd2de0b3e7)
  
However, I did not want to take from the style, just the motions, and I wanted my team to have workable animations ASAP, so I drew skeletons manually in Aseprite that mapped the head, joints, hands, and feet for all key motions like idle, jump, punch, and run. Also, each player was kept to a standard 64X64 bit canvas and was adjusted as needed. 
Example:

![player_idle_animation](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/692f11a3-0908-4ff3-a35b-b15491939c55) ![player_running_sprite](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/6a126495-23cc-4215-aa11-9a16c0027e71) ![player_punching_sprite](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/82fa295e-1ce1-420d-af46-223a30c50fad) ![player_jump_sprite](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/81be5184-ca6e-4ea6-8614-06cbfbc59561)

[Templating and Character Design]
After the skeletons were made, a generic body outline was constructed for each animation for all enemies and the player to be easily constructed, given all are boxers, the idea of templating works as they all will have similar lean physiques. Now as for the intent behind enemy design, given the idea for elemental powers in both combat and utility, I wanted to design enemies that reflected one of each element. However, given they follow a template, the majority of their uniqueness would have to be in very distinguishable face, hair, and accessory design, as well as a color palette. The color palettes were kept to a minimum, each character featuring a key color in red, yellow, or blue to represent their element, and then a complementary skin tone. Regarding each design, hair was one of the biggest factors, as when playing the game it's a larger detail the player will notice far more than some of the lesser details like even the face. For electricity I gave a static spiky hair with black lightning bolt streaks, the water I kept flowier with polar bear hair clips to sell the colder pallet, lastly for the fire was a more obvious one as I made the hair a literal flowing flame for the player to notice. Lastly for Arthur, I wanted to design a character that is simple and unassuming, and ultimately one the player can maybe insert themselves into, which is why his features are shrouded by his hair. and he isn't defined by a signature color like the enemies. 
![Untitled_Artwork](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/ca856179-3072-4a1f-b160-c4ddc11e2958)
![player_idle_animation](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/e563f4cc-7ff4-4a15-a36f-3908bc272340)![electric_idle_animation](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/2743bebf-0694-4e9d-8090-10cb2012e9e3)![ice_idle_animation](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/328d73de-dde6-4f14-a7f2-39ce81c026d2) ![fire_idle_animation](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/a99741be-277b-4e01-8d27-be09027c8b02)

[Stage Design] Given we already had a pre-established narrative and setting, the Stage design was a fairly simple process as it was just a matter of developing assets for a warehouse setting. So, I converted the found warehouse tileset to a unity tileset and just played around with settings until I got my desired look that I felt fit the narrative and properly overlayed the initial stage developed by another team member. This warehouse setting inspired some other elements to be made, such as the surveillance drones that drop hazards, the moving platforms that look like wooden pallets, and even the electric-generated platforms, as well as the concrete blocks. Essentially, every stage element was designed with the express purpose of fitting into the narrative and making the player feel as though they are part of the adventure being told to them. 
![Capture_1](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/db3baff3-1943-4593-8449-e77493956042)
![Capture_3](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/73a0c647-f2c9-41a9-a2be-89182e815d50)
![Capture_2](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/74acc97e-72d0-4842-93e7-b9b1aeb3c9f0)
[Last Minute Additions]
As the game progressed we wanted to add more options to player movement and even a stage boss for the player to overcome. The three major additions were a crouch jump, an air attack, and a death animation. For the animations, I took a more freeform approach and wanted to add some game references while making them believable to the narrative. For the crouch jump, I took inspiration from Sonic's spin dashing, this kept animation simple but gave a visually appealing short hop as the player has spiky hair so it fit with the character as well. For air attacks, it was just a fusion of the jump and punch animations so nothing special. Lastly, for the death animation, I wanted to incorporate an earthbound reference, which is when Ness explodes from failed teleportation. Essentially, this fits narratively as the contract with the golden gloves implies that Arthur can never lose again, as such if he does, the gloves combust, and that is KO for Arthur.
![crouch_jump](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/a5f71ea5-f1c1-4772-a5cc-cb1dd508b8b0)
![air_punch](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/381d330e-5387-4432-b0b3-af9d9dffe448)
![player_death](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/4337fdb0-23e1-45d5-8a04-fe9f5aca6555)

The Stage Boss was kept simple yet challenging for time constraints. It is a statue modeled after the electric enemy, however, instead of stun bolts, the enemy was designed as a homing missile launcher, which makes sense as drones can already drop missiles but instead, these missiles just have a special property. It also makes sense as a first boss narratively as the champ would set up mechanical obstacles to test any challenger's abilities before a genuine face-off. Lastly, some drawings were completed for an introduction cutscene, which was directly created with the express purpose of illustrating the written narrative of Arthur's lore.\  
![boss_statue](https://github.com/Richard-Voragen/Roll-With-The-Punches/assets/57019320/5f43e858-08c2-4820-b1af-2ff746891f9b)

[Implementation of Animations and Added Responsibilities]

  *Implementing Animations into Unity*: All animations were made by hand in Aseprite, made into sprite sheets, sliced and then added to animation controllers for the programmers to use and program. 

  *Setting Up a Tile Map*: For us to have a workable tilemap, I had to figure out how the tilemap system works, as well as how to configure the collider and composite collider as well as the rigid body to make a dynamic collider that will adjust as we draw our tiles. The program was differentiated between a floor and platforms, which platforms used a script developed by another team member. From there I overlayed the entire stage and playtested it to ensure functionality. 
  
  *Setting Up Moving Platforms* To get the desired effect from the moving platforms, I completely refactored the code for all moving platforms, as well as wrote the script for the generator platform. 
https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/b7bb922f64d7cfc8117e6f541facf9960b31e332/RollingWithThePunches/Assets/Scripts/Enivorment/MovingPlatforms.cs#L1-L77
https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/b7bb922f64d7cfc8117e6f541facf9960b31e332/RollingWithThePunches/Assets/Scripts/Enivorment/ToggleScript.cs#L1-L39

*Coding The Final Boss* 
In addition to designing the final boss, I was in charge of writing the homing missile scripts as well as the boss script. 
https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/b7bb922f64d7cfc8117e6f541facf9960b31e332/RollingWithThePunches/Assets/Scripts/Enemys/BossScript.cs#L1-L54
https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/b7bb922f64d7cfc8117e6f541facf9960b31e332/RollingWithThePunches/Assets/Scripts/Enemys/Drone/HomingMissile.cs#L1-L44

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Game Logic (Richard Voragen, Jared Martinez, Pablo Rivera, Jason Kohl)

For our game logic, we implemented multiple scripts to handle everything from camera controls to enemy spawning.  The [PlayerDamageEngine.cs]( https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Player/PlayerDamageEngine.cs) script was integral to managing our game’s state as it tracked when the game should reset depending on the health of the player. We also had scripts such as [Teleport.cs]( https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Tutorial/Teleport.cs)  that moved the player from the tutorial scene over to the main level scene once the player’s location reached a certain point on the tutorial map. The [EnemyFactory.cs]( https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Enemys/EnemyFactory.cs)  script was also important to the game’s state as it tracked the location of the player and started spawning enemies once the player reached a certain distance. The [PositionLockCameraController.cs]( https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/RollingWithThePunches/Assets/Scripts/Camera/PositionLockCameraController.cs)  script also played a large role in our game’s state as it tracked the player’s location and moved the camera to stay with the player. 

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

## Gameplay Testing (Richard Voragen, Jared Martinez, Pablo Rivera, Jason Kohl)

The key findings of our public playtesting is that our game had great visuals as well as great audio. Our game is also very challenging, as many players were not able to make it even halfway through our game's level. Though our game was challenging, participants reported that they liked the art as well as the audio enough to want to play more of our game, even though they were not actually doing very good. Some participants even reported liking the challenge of our game and wanted to play it more because it was hard.

## Narrative Design (Pablo Rivera)

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 
Admittedly, I came up with the concept, the premise, and the storyline with the support of my team. As such, throughout the entirety of the project, we went in with a clear concept, and most if not all decisions made pertained to the storyline.

[Assets]
As both the narrative designer and the art/animation designer, I had executive control over ensuring all of my assets fit the narrative. From the warehouse landscape, the character and enemy design, down to the death animation of the player. I made sure visually we are telling the story of Arthur and his reckless journey through the champion's warehouse where he hosts his underground fight full of lackeys. 

[Gameplay systems]
The two major systems are the combat and platforming systems, both of which follow the narrative as they rely on Arthur's newfound elemental abilities from the golden gloves and physical prowess as a boxer to traverse and progress through the level.

[The Introduction Cutscene] 
The introduction cutscene was designed with the intent of giving the player all of the exposition they will need for gameplay, both the art and the writing were done by me to communicate Arthur's story in a fun and visually appealing manner. This then gives the player an idea of who the character is, their goals and motivation, and as you see throughout the gameplay the struggle is to achieve what he is after even with his newfound powers. Also, as a final gag, you see in any death the gloves explode on Arthur, as a final hint at the contract that he can no longer afford to lose or he will lose any hope of beating the champion as the gloves will reject him and combust. 

Ultimately, everything was crafted with the design and intent of following the narrative that started and inspired out entire game project. 

## Press Kit and Trailer (Jason Kohl)

<a href="https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/Presskit.md">Presskit</a>

Trailer
<a href="https://youtu.be/6IHbzBngjl0?si=dyH1bY0KTx7Go3g1" target="_blank"><img src="https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/Presskit%20Images/TrailerTitle.PNG" alt="Trailer" /></a>

To best showcase our work, I played through our game multiple times to capture exciting or cool moments that I would find interesting and enticing if I was watching a gameplay trailer. I then loaded all of the clips that I had recorded into shortcut and worked on cutting down the clips into only the best and most exciting parts. I then wanted to follow the theme of our game and included the music that is used in-game in the trailer. The music track was way too long, so I managed to splice it into a 1-minute version to fit the gameplay trailer timeline. I then added in some free video of silent movie cards to really go with the theme of the game and add variety to the trailer so that it was not only gameplay. I made sure to keep the title cards short to maximize audience attention.

![Editing](https://github.com/Richard-Voragen/Roll-With-The-Punches/blob/main/Presskit%20Images/trailerEditing.png)

I chose the screenshots included in the press kit as they show a lot of the main parts of our game in order that the player would experience them. The first screenshot shows off our intro scene that gives the player the backstory of the game. I then included a screenshot of the tutorial to show how our game features a tutorial to help any players learn how to play our game. The next few screenshots show off different parts of our game such as combat scenarios that the player might find themselves in or the platforming obstacles that they will have to conquer on their way to the final boss. Lastly I included a screenshot of the player fighting the final boss as that is the ending of our game, but I did not show the player actually beating the boss as that would take away all of the suspense and drive of the player to actually play the game. 

## Game Feel and Polish

**Document what you added to and how you tweaked your game to improve its game feel.**
