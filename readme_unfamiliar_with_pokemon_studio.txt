((you should be comming from "readme.txt", continue at 22) or start over from 01) ))
Here are the steps I am using currently
01) create a new project in PokemonStudio ( https://www.youtube.com/watch?v=hkxUMJ5okrQ&list=PLBCQaB3tnyNxqvm48pSC1jEESpgqw0RSe )
02) edit the reginal dex in PokemonStuido ( https://www.youtube.com/watch?v=je9W-5tWh-Q&list=PLBCQaB3tnyNxqvm48pSC1jEESpgqw0RSe&index=6 )
03) close PokemonStudio
04) put my image (see above) next to my PokemonStudio project and name it like the foloder of the PokemonStudio project 
05) start PokemonSDK.JuyJuka.QuickMapStart
06) click "import" & select the image from 4) , PokemonSDK.JuyJuka.QuickMapStart tries to find the PokemonStudio project & the dex in it
06) you can select the other input(s) manually using the "..." buttons, if you wish to or the automatic try to find them failed
07) click "import" (yes a second time)
08) click "preview" , you can have a look at the different maps (as made with Tiled https://www.mapeditor.org/ ) by clicking on the buttons
08) you can assigne a different "color" to any map by clicking on "next color", you can cange the pokemon that will be assigned to each map
08) But remember that PokemonSDK.JuyJuka.QuickMapStart is only doing a head-start, the real work and beauty of your project should come from working with PokemonStudio, Tiled and so on
08) Better to adjust and toy around with the inputs (image, dex and the other parameters) on the tab "main" again and do 7) "import" again (it is quite fast) and look "preview" again
09) (!) be aware the next steps can only be done once per PokemonStudio project (!) I do not know hot to do it again
10) click "export" & wait , PokemonSDK.JuyJuka.QuickMapStart will work for a while and make a pop-up when it needs you again.
11) (!) keep the pop-up open (!)
12) open PokemonStudio & open your PokemonStudio project
13) click "new map" (under the map symbol) and the button below the label "Import Tiled maps" (at the very end/buttom on the right side)
14) click "browser your files" (sorry if the translation is worong)
15) PokemonSDK.JuyJuka.QuickMapStart has created a folder next to your PokemonStudio project named like your project + "_tiled", navigate there
16) click "open folder"
17) PokemonStuidio will recognice all the maps (as in 08) click on the top most check box wich will check all maps for import
18) click "import" & wait , PokemonStudio will work for a while
19) close PokemonStudio (do not forgett to save)
20) close the pop-up from PokemonSDK.JuyJuka.QuickMapStart & wait , this should go quickly
21) close PokemonSDK.JuyJuka.QuickMapStart 
22) open PokemonStudio & open your PokemonStudio project
22) if you are familia with PokemonStudio you can now use the maps as you see fit
22) if you are new to PokemonStudio here are my first steps I took here
23) open Game_RMXP_1.05.rxproj in the folder of your PokemonStudio project ( RPG Maker XP should open )
24) dubble click on map "Start" 
25) double click on the top left square (white border, black inside) a new pop-up opens
26) double click the first line,  a new pop-up opens
27) click "control switches", a new pop-up opens
28) click the liltle (sometimes invisible) arrow next to the first input, a new pop-up opens
29) click "[0051 - 0075]" in the left list, click double "0053: Player can run" in the right list , the pop-up closes
30) click "ok", the pop-up closes
31) double click the first line,  a new pop-up opens
32) go down the "List of event commands:". I delete the following by clicking on them once (they turn blue) and the key-board-key DEL but you can keep them
32) - every line that starts with ">Wait"
32) - Line starting with ">Text: 2,1"
32) - Line starting with ">Text: 2,2"
32) - Line starting with ">Text: 2,3"
32) - Line starting with ">Text: 2,4"
32) - Line starting with ">Text: 2,5"
32) - Line starting with ">Text: 2,6"
32) - Line starting with ">Text: 2,7"
32) - Line starting with ">Text: 2,8" (not ">Text: 2,0" not "Text: 2,9"
32) - the 4 lines after the big comment from the PokemonStudio Team that tells you to delete them (there is on ">Wait" line within, so only 3 if you already deleted all the ">Wait" lines)
33) Some where at the end comes a line starting with ">Transfere Player:[0,19: Exterior", right click it and click "edit", a new pop-up opens
34) click the liltle (sometimes invisible) arrow next to the first input, a new pop-up opens
35) click the first map created by PokemonSDK.JuyJuka.QuickMapStart in the list on the left, it should start with "E_X0", a gray rectangle appears on the right side
36) click the top-right corner of the gray rectangle, a white border should appear (like in 25 but grey not black)
37) click "okay", the pop-up closes
38) click "okay", the pop-up closes
39) double click the line you just edited, a new pop-up opens
40) click "3"
50) click "Script..." (the last button on left before "cancel"), a new pop-up opens
51) insert "add_rename_pokemon(:pikachu,5)" (you can copy from here)
52) click "ok", the pop-up closes
53) click "Apply"
54) repeate from 39) to 53) with the folloing text "give_item(:poke_ball,99)" instead of "add_rename_pokemon(:pikachu,5)"
55) click "OK", the last pop-up closes
56) close RPG Maker XP, do not forgett to save
57) return to PokemonStudio (you did not need to close it, but you can open it again) and click the play button (the triangel/arrorw pointing leftwards)
58) follow the screen/game instructions, you should end up in game, on the map PokemonSDK.JuyJuka.QuickMapStart created for you with a Pikachu and 99 Pokeballs
59) try around, and return to 01) or 22) when you are more familiar with PokemonStudio

Greetings
Juy Juka
