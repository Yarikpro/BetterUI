# BetterUI
Add some more options to the screen!

[![forthebadge](https://forthebadge.com/images/badges/built-with-swag.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/it-works-why.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/powered-by-black-magic.svg)](https://forthebadge.com)

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

## Config
Name  | Type | Default | Description
------------ | ------------ | ------------- | ------------ 
`IsEnabled` | Boolean | true | Is this plugin enabled?
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

## Config.syml
```yml
[BetterUI]
{
# Should this plugin be enabled?
isEnabled: true
# Should the amount of alive players in your team be displayed?
enableTeamCountElement: true
# Should the amount of all of your ammo should be displayed?
enableTotalAmmoElement: true
# Should the amount of your grenades be displayed?
enableGrenadesElement: true
# Should a Targetcounter be displayed for SCP-035/056 if you have them installed?
enableTargetCounter: true
# Should the time in which spectators respawn be displayed to dead players?
enableRespawnTimerElement: true
# Should a player be notificated if he sees SCP-096?
enable096IndicatorElement: true
# Should a player be shown how much damage he dealt?
enableDamageElement: true
# Should a player be shown the role name and hp of the player he killed/he got killed from?
enableKillElement: true
# Should a player be shown the current kills, deaths and a KD/r on the screen?
enableStatsElements: true
# Should at the end of the round the best players be shown?
enableLeaderboard: true
}
```
