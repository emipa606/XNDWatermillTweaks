# [[XND] Watermill Tweaks (Continued)](https://steamcommunity.com/sharedfiles/filedetails/?id=2055956298)

![Image](https://i.imgur.com/buuPQel.png)

Update of XeoNovaDans mod 
https://steamcommunity.com/sharedfiles/filedetails/?id=1496115783

Fixed the letter-text for turbulent river
Added info in Watermill description when turbulent river is active

![Image](https://i.imgur.com/pufA0kM.png)
	
![Image](https://i.imgur.com/Z4GOv8H.png)

# **Compatibility**

Should be safely addable to existing savegames. There may be issues removing this from ongoing saves if there's ongoing turbulent waters though.

Should work fine with all mods out there.

If you find any bugs, please link me to a full output log and give some basic information on how you triggered the bug; this is vital information for me to be able to fix said bugs.

# **Overview**

Watermill Tweaks is a mod that makes several changes that are specific to the watermill generator to make it somewhat of a more nuanced power source compared to other power sources. It does so by doing the following:


- **Changing the Power Output - **Watermill generators now have a base generation of 1400W rather than 1100W in vanilla.
- **Temperature Variation -** If outdoor temperatures are below 0°C (32°F) or above 50°C (122°F), watermill generators will produce less power. This is to simulate a gradual increase of viscosity from freezing and lower levels from increased evaporation respectively. On the hot end, power production halves at 60°C (140°F), and stops entirely at and beyond 90°C (194°F). However, it varies by river type for the cool end: -5°C and -8°C for creeks; -10°C and -15°C for rivers; -15°C and -23°C for large rivers; -20°C and -30°C for huge rivers.
- **Seasonal Variation -** Power output also changes depending on the season. 20% more power is produced during the spring, and 10% more power is produced during the fall, but 30% less power is produced during the winter


**Adding a new Incident - **Watermill Tweaks also adds a new but somewhat uncommon incident called 'Turbulent Waters'. When this incident happens, all watermill generators on the affected map produce 50% more electricity, but they also get damaged over time because of debris and stress. Turbulent waters will only ever affect maps with standard rivers, large rivers and huge rivers though, with the incident being 2x more common with large rivers and 4x more common with huge rivers. Maps with greater than 2000mm of rainfall are also more likely to affected, and maps with less than 1000mm are less likely to be affected.

Powered by the Harmony Patch Library.

# **Credits**

**Brrainz -** for their excellent Harmony Patch Library!
**Mehni -** The Seasonal MVP
**Marnador -** for the RimWorld-style font
**AileTheAlien -** for the idea

# **License**

As is standard with my mods: you may include this mod in a mod pack, and you may derive from this, but please inform me if you're doing so through Ludeon Forums (preferably) or the Steam Comments Section, and give credit where credit's due.


![Image](https://i.imgur.com/PwoNOj4.png)



-  See if the the error persists if you just have this mod and its requirements active.
-  If not, try adding your other mods until it happens again.
-  Post your error-log using [HugsLib](https://steamcommunity.com/workshop/filedetails/?id=818773962) or the standalone [Uploader](https://steamcommunity.com/sharedfiles/filedetails/?id=2873415404) and command Ctrl+F12
-  For best support, please use the Discord-channel for error-reporting.
-  Do not report errors by making a discussion-thread, I get no notification of that.
-  If you have the solution for a problem, please post it to the GitHub repository.
-  Use [RimSort](https://github.com/RimSort/RimSort/releases/latest) to sort your mods

 

[![Image](https://img.shields.io/github/v/release/emipa606/XNDWatermillTweaks?label=latest%20version&style=plastic&color=9f1111&labelColor=black)](https://steamcommunity.com/sharedfiles/filedetails/changelog/2055956298) | tags:  seasons
