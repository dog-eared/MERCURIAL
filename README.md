# MERCURIAL

####v.0.001

Mercurial is a topdown space adventure with roleplaying elements. The goal is to
recreate the classic Mac game _[Escape Velocity]_ (http://www.ambrosiasw.com) with more modern, action based
gameplay and stronger characterization.

The player character of Mercurial has just been granted their basic Starpilot's
License and suddenly the galaxy has opened to them. They can serve as agents of 
the Terran Dominion, investigate the mysteries of humanity's predecessors, trade
goods from planet to planet or explore the cosmos. 

##COMPLETED TASKS
1. Implemented basic flight: both inertialess and physics based
2. Implemented structure for systems/planets reading from json files
3. Added scrolling space backdrop and placeholder assets
4. Placeholder playerData, pilotData, and other structures that need to be built 
5. barebones asteroid behaviour added
6. Placeholder planet menu and behaviour

##GOALS FOR NEXT BUILD
1. Basic NPC ships with simple movement.
2. Simple laser weapon for ships
3. Health and shields systems for ships

##GOALS FOR LATER
1. Jumping btwn star systems
2. Flesh out landing on planets
3. Planet Menu should display data from planet
4. Star map menu
5. Player stats screen 

##Current Issues:
1. Hitting Escape/Cancel opens the settings menu if you have no menus open...this is fine,
except there's no good way out yet.
2. Need to make sure InputManager for GUI instantiates at runtime so you don't need the 
Landing menu to be able to interact w. buttons
3. And buttons should probably do something...at least the cancel button!



...note to self, make a credits section to link to the placeholder art 

Rawdanitsu -- Planets -- https://opengameart.org/content/2d-planets-0 
Mysitemyway -- Space backdrops -- http://webtreats.mysitemyway.com/tileable-classic-nebula-space-patterns/
Skorpio -- Spaceships https://opengameart.org/content/spaceship-8 and other links I need to collect

