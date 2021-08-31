# BetterUI
Add some more options to the screen!

Thanks to [Dimenzio](https://github.com/GrafDimenzio) for explaining me how to patch!

The Respawn Timer is heavily inspired by the one being used on LunarGaming, big shoutout for their great Developers there!

[![forthebadge](https://forthebadge.com/images/badges/built-with-swag.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/it-works-why.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/powered-by-black-magic.svg)](https://forthebadge.com)

## What is this plugin?
_**BetterUI**_ patched the normal Hint system in order to not just use hints to show different information to the player but also to show normal hints sent by other plugins like it should be. _**BetterUI**_ has three big parts into it. **I.** The Respawn Timer. **II.** The UI Elements while being Alive. **III.** The RoundEnd Leaderboard. You can fully decide what UI Elements you want enabled and what not.

_**Patching the Hints System was chancer but it was worth it. I will never ever work with Hints again**_

## Features
* Respawn Timer/Ticket Counter/Round Time
* Ammo and Grenade Counter
* Team Counter
* Kill/Death/KD/r Stats
* SCP-035/SCP-056 Target Indicator
* Damage Indicator
* SCP-096 Indicator
* Kill Information
* Round Leaderboard
* LobbyText

## Supported Languages 
* English
* German
* Polish (Created by `Doktor#7356`)

## How to add new Languages
You have to either dm me on Discord, then I can explain it to you or you just create a pull request. (My Discord Account: `TheVoidNebula#5090`)

## Installation
1. [Install Synapse](https://github.com/SynapseSL/Synapse/wiki#hosting-guides)
2. Place the SupplyDrops.dll file that you can download [here](https://github.com/TheVoidNebula/SupplyDrops/releases) in your plugin directory
3. Restart/Start your server.

## Showcase
![UI](/assets/ui.png)
![Killed Window](/assets/2.png)
![Respawn Timer](/assets/3.png)
![Round Leaderboard](/assets/4.png)
![SCP-035/SCP-056 Target Indicator](/assets/5.png)
![SCP-096 Indicator](/assets/6.png)

https://user-images.githubusercontent.com/75329526/131568473-fa4abd36-ffe4-458d-9997-93e60cd15861.mp4



## Config
Name  | Type | Default | Description
------------ | ------------ | ------------- | ------------ 
`MinPlayersForSupply` | Int | 4 | Minimum players for supply drops to happen
`EnableTeamCountElement` | Boolean | true | Should the amount of alive players in your team be displayed?
`EnableTotalAmmoElement` | Boolean | true | Should the amount of all of your ammo should be displayed?
`EnableGrenadesElement` | Boolean | true | Should the amount of your grenades be displayed?
`EnableTargetCounter` | Boolean | true | Should a Targetcounter be displayed for SCP-035/056 if you have them installed?
`EnableRespawnTimerElement` | Boolean | true | Should the time in which spectators respawn be displayed to dead players?
`Enable096IndicatorElement` | Boolean | true | Should a player be notificated if he sees SCP-096?
`EnableDamageElement` | Boolean | true | Should a player be shown how much damage he dealt?
`EnableKillElement` | Boolean | true | Should a player be shown the role name and hp of the player he killed/he got killed from?
`EnableStatsElements` | Boolean | true | Should a player be shown the current kills, deaths and a KD/r on the screen?
`EnableLeaderboard` | Boolean | true | Should at the end of the round the best players be shown?
`EnableWaitingForUsersText` | Boolean | false | Should a Text appear when the server is starting and waiting for users? Only use this if you do NOT have the WaitAndChill Plugin installed!
`WaitingForUsersText` | String | '\n\n\n\nWelcome to MyServer!\n<b><color=%rainbow%>Join our Discord Server!\ndiscord.gg/yourdiscord</color></b>' | What Text should show up on a roundstart?

## Config.syml
```yml
[BetterUI]
{
# Should this Plugin be enabled?
isEnabled: true
# Should the amount of alive players in your team be displayed?
enableTeamCountElement: true
# Should the amount of all of your ammo should be displayed?
enableTotalAmmoElement: true
# Should the amount of your grenades be displayed?
enableGrenadesElement: true
# Should a Targetcounter be displayed for SCP-035/056 if you have them installed?
enableTargetCounter: false
# Should the time in which spectators respawn be displayed to dead players?
enableRespawnTimerElement: true
# Should a player be notificated if he sees SCP-096?
enable096IndicatorElement: true
# Should a player be shown how much damage he dealt?
enableDamageElement: true
# Should a player be shown the role name and hp of the player he killed/he got killed from?
enableKillElement: true
# Should a player be shown the current kills, deaths and a KD/r on the screen?
enableStatsElements: false
# Should at the end of the round the best players be shown?
enableLeaderboard: false
# Should a Text appear when the server is starting and waiting for users? Only use this if you do NOT have the WaitAndChill Plugin installed!
enableWaitingForUsersText: false
# What Text should show up on a roundstart?
waitingForUsersText: >2-




  Welcome to MyServer!

  <b><color=%rainbow%>Join our Discord Server!

  discord.gg/yourdiscord</color></b>
}
```
