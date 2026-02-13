# Legally Distinct Flappy Bird

# Projects Specifications and Progress Tracking:

[MADE WITH UNIVERSAL 3D TEMPLATE]

### Requirements:

- Artwork must be custom made

- Clicking must be accompanied by a sound effect

- Game Over needs a sound effect

- Game Over when colliding with object

- Score Tracking

- Infinite scrolling

- Gradual difficulty increase

Self-Imposed

- Pause Menu

- Close Game button

- Restart Button

- Music w/ adjustment slider

- Half decent project documentation

- Start Menu (likely just a start button)

### Progress:

- [x] Flappy Bird model finished

- [x] Obstacle model finished

- [x] Backdrop finished

- [x] Input Handling

- [x] Score Tracker

- [x] Obstacle Spawner

- [x] Persistent UI

- [x] GameState handling

- [x] Game Over Screen

- [x] Pause Menu

- [x] Close Game Button

- [x] Difficulty Increase (increase obstacle frequency)

- [x] Clicking sound effect

- [x] Game Over + Sound Effect

- [x] Music ([https://www.youtube.com/watch?v=pbuJUaO-wpY](<https://www.youtube.com/watch?v=pbuJUaO-wpY>) at around 6:00 for exposing mixer param)

- [x] Program Compiled

- [x] Documentation

- [x] Documentation export as markdown (make sure all sections are unfolded)

### Reusable Code:

#### From: Clickism

- Audio Manager Script

- Volume Controller Script

- GameManager Script (partial)

#### From: Challenge3

- MoveLeftX Script

- PlayerControllerX Script

- RepeatBackGroundX Script

- SpawnManagerX Script

# Documentation:

## Concept:

  The project is fairly simple: create a FlappyBird-like Unity game that meets a specific list of criteria; including original artwork, gradual difficulty scaling, and sound effects. Self-imposed requirements also include a proper pause menu, a button to close the application, and adjustable volume. There is plenty of code from previous projects that can be repurposed for this project, which have been added to a dedicated "TemplateScripts" folder inside the project itself.

  As another self-imposed requirement, I intend to document most of the project specifications, development aspects, and summaries of each script in the project. The final documentation file will be exported as HTML from Notesnook, and both the Progress Tracker and Documentation itself will be merged into a single file.

## Planning:

### Scripting:

  First things first, I need to identify what can be reused (within specification) from previous projects, and two projects seem to have what I need. "*Clickism", *a Fruit-Ninja-style game, has a largely plug-and-play audio script, as well as some applicable components from the main GameManager script. Methods like the `CloseGame()`, `RestartGame()`, and `PauseHandler()`should be usable as is (*though the latter should likely be renamed for clarity*), while `LivesManager()` can be removed entirely.

  Specifics were not provided in the spec for *how* scoring should be counted, so I think it best to count it based on seconds. I simple Coroutine that checks for a game over status and increments the score every second should be sufficient for this project. The `StartGame()` method from GameManager.cs should be largely usable as-is, with only minor tweaks needed.

  Mentions of `Rigidbody` and `Collider` will need to be changed to `Rigidbody2D` and `Collider2D` respectively.

### Assets:

  Assets are to be made in MS Paint, mainly for the cheap knockoff aesthetic. The player model will likely be done in Piskel for easier pixel art. All will remain simple as to allow time to be focused on scripting and editor work. Sound may have to be sourced from older projects, and music will likely not be included until the very end of development, if at all.

### Difficulty Progression:

  Since the player themselves only move vertically, all difficulty changes will need to be made to the obstacle spawner and actual background. The main way I intend to achieve this is to increase the speed of the obstacles over time, incrementally gaining a minor percentage increase to the base speed (possibly something like `speed *= 1.1f;` inside a coroutine).

### Documentation:

  Documentation will be done in Notesnook and exported as a PDF when completed. The link to the documentation file is in the project checklist, which might be imported to the final document after the project is completed. As far as formatting is concerned, everything will remain fairly simple with HTML headers.

## Project Documentation:

### PlayerController.cs:

  PlayerContoller.cs handles player input, obstacle detection, and player physics. It consists of 5 total methods and one co-routine, that latter of which is used for autonomous input during the title screen. The script resets gravity when the script wakes up by first setting the gravity to it's default force before reapplying the multiplier. This was done because restarting the game would apply the multiplier to an already multiplied gravity, and because I had failed to consider that maybe I could just hardcode the gravity force in the first place. I opted to leave it alone though, since it works well enough and because this is only for an assignment that checks functionality above all else. `Awake()` also starts the autonomous input Coroutine and fetches the Rigidbody component from the player object.

  The `Update()` method does two things every frame: handling the actual player input - applying force and playing the flap sound when the spacebar is pressed, and fetching the players current Y coordinate for the autonomous input function. An if-statement is run at every frame to check that both the spacebar is pressed and that the game is actually in an active state (via the boolean isGameActive).

  The PlayerController.cs script contains both an `OnCollisionEnter()` and `OnTriggerEnter()` method. Both methods are extremely simple, consisting of a couple lines of code each. `OnCollisionEnter()` detects whether an obstacle has made contact with the player, and triggers the `GameOver()` method in GameManager.cs if so. `OnTriggerEnter()` detects when the player passes between the pipes via entering a dedicated trigger game object. When the trigger area is entered, the method calls the `UpdateScore()` method in GameManager.cs, which increments the score by 1.

  The final method is more of an easter egg than anything. `OnMouseDown()` detects whether the player object has been clicked, and changes the game music if so.

  Lastly, the `IdleController()` coroutine handles autonomous input while on the title screen. When the current Y coordinate of the player falls below a certain coordinate, the method applies an upwards impulse to the player in the same way that the player input works. If input is applied, the coroutine returns a longer wait time than it otherwise would, preventing excessive input that could launch the player object faster and higher than desirable.
  

### MoveLeft.cs & RepeatBackground.cs:

  MoveLeft.cs and RepeatBackground.cs are both scripts that handle the "scrolling" of the background. RepeatBackground.cs has only 2 methods, `Awake()` and `Update()`. `Awake()` sets the starting position of the background and fetches the Box Collider size along the X axis, which is then divided by 2. This creates a fairly seamless "scrolling" effect. `Update()` simply checked the position of the background every frame. If the background moves past the set X position, it returns the background to the start position.

  Additionally, MoveLeft.cs also applies that same right-to-left movement to the game obstacles. The only method in this scripts is the `FixedUpdate()` method, which checks for game status (via the isGameActive boolean from GameManager.cs), object tag, and destroys game objects that pass a set X coordinate unless tagged with the "Background" tag. The speed can be set in the Unity Editor, and thus allows the obstacles to move at a different speed than the background.

  Initially, the `FixedUpdate()` method was the `Update()` method. This worked fine in the editor itself, but did not work correctly once compiled. This change fixed that issue.


### GameManager.cs:

  GameManager.cs is the most complex script in this project, handling score counting, game states, UI management, and game closing. The script has 8 total methods, the first being `Awake()`. This methods runs at launch, and returns all game states to their default values. The isGameActive boolean is set to false, time scale is reset to 1f, and a new GameManager instance is created. All UI elements are set to Inactive, barring the title screen, which is set to Active. With this script, even if one were to accidentally leave a UI element incorrectly active, the game will be in it's correct state when loaded.

  The `StartGame()` method is very similar in structure to the `Awake()` method, though does a few additional things. When the start button is pressed, the method sets the isGamePaused boolean to false, and the isGameActive to true. It also resets the score to 0. Like the `Awake()` method, this sets every UI element to it's correct state, disabling the title screen and exit button while enabling the persistent UI. It lastly calls the `StartSpawner()` method in SpawnManager.cs.

  `Update()` is a far simpler method than the previous two, only checking for when the escape key is pressed and whether the game is in it's active state. When both criteria are met, it called the `PauseHandler()` method. This method is a bit more complex. The isGamePaused boolean is set to the inverse of what it previously was, and any affected UI elements are enabled or disabled with that same boolean. The time scale is also toggled between 0f and 1f, effectively setting the passage of time to either completely stopped or at it's default speed.

  `RestartGame()` is also a rather simple method. Being toggled by a UI button usable only on the Game Over or Game Paused screens. When activated, the method simply reloads the scene via Unity's scene manager. `CloseGame()` is a similarly simple method, only containing a single line of code. This method is also run via a UI button, which is accessible the Pause Menu, Game Over screen, and Title Screen. On activation, the method runs Unity's built-in `Application.Quit()` method, closing the game.

  The `GameOver()` method runs on player collision with an obstacle. When called by PlayerController.cs, the method checks the players score. If that value exceeds a certain threshold, a title screen congratulating the player is displayed. If the score is below this threshold, a separate screen is displayed which subtly mocks the player. This method also enables both the Restart and Exit UI buttons, sets both isGamePaused and isGameActive to false, resets the timescale to 1f, and plays the Game Over sound effect.

  The final method is `UpdateScore()`, another simple one. Called by PlayerController.cs whenever the player passed between the pipes, the method checks whether the isGameActive boolean is set to true. If true, the score integer is incremented by 1, and Score Text is updated to reflect the new value.


### SpawnManager.cs:

  SpawnManager.cs handles the creation of obstacles and increase in difficulty, and contains three methods and one coroutine. The `Start()` method is a single line method that resets the difficulty to its default value. The `Update()` method is also simple, gradually updating the difficulty a small amount every frame until it reaches it's maximum allowed value. The `StartSpawner()` method is an equally simple method. If the `SpawnTarget()` coroutine is Null, it initializes the coroutine.

  The `SpawnTarget()` coroutine is the most complex part of this script. When initialized, it will run until the isGameActive boolean is set to false by the `GameOver()` method in GameManager.cs. While running, the script generates 3 spawn positions, two for each obstacle and one for the score trigger. The first spawn coordinate has the lowest Y position of the three, and the other two simply reuse the same coordinate, just adding a certain amount to the Y value. The base Y coordinate is set by a random range between -8 and -1. The time between spawns is gradually decreased by the difficulty value, slowly lowering from its maximum of 4f to its minimum of 1f over time.


### VolumeController.cs & AudioManager.cs:

  VolumeController.cs and AudioManager.cs are scripts that both handle how the music volume is loaded, changes, and stored. AudioManager.cs has 3 methods, `Awake()`, `LoadVolume()`, and `MemeMusic()`. Awake() creates an instance of the Audio Manager, calls the `LoadVolume()` method, and starts the background music. The `LoadVolume()` method loads the volume value stored by VolumeContoller.cs and updates the volume relative to the position of the UI slider. Lastly, the `MemeMusic()` method is called by the `OnMouseDown()` method in PlayerController.cs, and replaces the default music with the easter egg music.

  VolumeController.cs has 4 single-line methods. `Start()` sets the slider value to the position corresponding to the value saved in PlayerPerfs. `Awake()` adds a listener to the UI Slider on initialization, which allows the slider to set the volume. The `OnDisable()` method saves the current slider position to PlayerPerfs when the game is closed, and `SetMusicVolum()` saves the current volume to PlayerPerfs.


