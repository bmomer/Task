# Task
Fortune Wheel Demo for Vertigo Games
-

Structure / Design:
--------------------------
I decided to use Component Based State machine for this project in order the have full control
of the game flow. I've created manager scripts for complex parts of the game

-Game Manager (pretty much access to everything)
-UI Manager (handles all ui based behaviours)
-Wheel Manager (handles wheel behaviours)
-Animation Manager (handles all animations in the game)
-Game State Machine (handles state changes and initial state setups)

And i have 6 states that divides the game into parts.

After everything initialized from Game Manager, states goes like this

Zone Select -> Wheel Select -> Ready To Spin -> Spinning -> CardReveal -> End Game (game flow starts over) -> Zone Select

*all states have access to all managers, but managers do not have access to each other (except Game Manager)
we control the game flow with the states, calling with events and methods from managers basically.

*i only used Dotween for animations. the parameters for animations is
on the animation manager, easily editable from editor (for speeding up animations or card reveal rotations etc).

*i used scriptable objects for rewards and assigned all items an unique ID. ID makes possible to
create Item Card's and show acquired items on somewhere else in the game (like end screen or reward panel)

ID also makes it possible to store player items (simulating the inventory with GameData scriptable object), i used an int array to store item amounts
and used item ID's as an index for this array. (for example gold item id = 3, so array[3] is basically player's gold amount.)

*rewards can be added to the wheels on editor, i created a seperate folder for scriptable object rewards.

*all rewards on wheels have Chance value, makes it possible to make some items even harder to acquire (basically breaks the 1/8 chance logic)

*editor code for button references

*Wheels can be configured via editor (on WheelManager gameobject on the scene)




