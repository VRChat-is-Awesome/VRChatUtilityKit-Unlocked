# Sleepers VRC Mods<!-- omit in toc -->

These are mods that weren't being maintained anymore, gathered together.
The hope with this repository is that as a community we could still maintain them.
Scroll down to [the table of contents](#table-of-contents) for the list of mods.

This repository was forked from Loukylor's mods after he stopped maintaining them.
The intention with the name change is to make it clear that this project hopes to have multiple people maintaining it instead of a single individual.
It's also a tribute to all the modders who've helped make and maintain these mods but aren't maintaining them anymore - even modders need to sleep eventually.

Please go see [the credits](#credits) for the full list of people who've helped make this all possible!
Or maybe even [get your name added](./CONTRIBUTING.md) to that list?

## Disclaimer<!-- omit in toc -->

VRChat does **not** condone the use of mods.
You **will** be punished if found to be modifying the client.

That being said, there is no anticheat of any sort in the client as of writing this.
There is an API limiter & Photon checks.
The mods listed in this repository avoid triggering those.
The only real way to get punished is to piss of the aforementioned checks, or be reported by a user with evidence of you using a mod.

**These mods are provided as-is without any warranty and we will not be held responsible for anything that using mods may cause**.

## Installation<!-- omit in toc -->

1. Follow the instructions on the [MelonLoader wiki](https://melonwiki.xyz/#/) on installing MelonLoader (MelonLoader is the mod loader which will allow the mods to run).
2. Download the mod(s) you would like to install.
3. Drag'n'drop the downloaded mod(s) into the `Mods` folder in the `VRChat` folder (if the folder isn't there, run the game once).

More detailed instructions and more mods can be found in the [VRChat Modding Group Discord](https://discord.gg/rCqKSvR).

## Table of contents<!-- omit in toc -->

- [AskToPortal](#asktoportal)
- [AvatarHider](#avatarhider)
- [ChairExitController](#chairexitcontroller)
- [CloningBeGone](#cloningbegone)
- [PreviewScroller](#previewscroller)
- [PrivateInstanceIcon](#privateinstanceicon)
- [ReloadAvatars](#reloadavatars)
- [RememberMe](#rememberme)
- [TriggerESP](#triggeresp)
- [UserHistory](#userhistory)
- [UserInfoExtensions](#userinfoextensions)
- [VRChatUtilityKit](#vrchatutilitykit)
  - [For Developers](#for-developers)
  - [Licensing](#licensing)
- [Credits](#credits)

## AskToPortal

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]

Makes sure you want to enter a portal, every time you enter a portal.

This mod also contains many checks for portal droppers, or people who use a mod that drops portals maliciously.
If the mod detects a portal dropper, it will give you the option of blacklisting the user until you restart your game.

You can also toggle the mod on and off and auto accept portals from friends, yourself, and one's placed in the world itself (by the creator).

![Basic User Prompt](https://i.imgur.com/IiOnkCM.png)
![Detailed User Prompt](https://i.imgur.com/N4QHlbb.png)
![Basic User Prompt with Errors](https://i.imgur.com/fja7qNY.png)
![Detailed User Prompt with Errors](https://i.imgur.com/SJPALdl.png)

## AvatarHider

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]

Automatically hides avatars based on the distance away from you.
There's no real reason to render avatars that you don't even pay attention to, right?

For the best experience, it is recommended to run this mod with UIExpansionKit.
"Hide Distance" is customizable and can be changed in meters (default is 7 meters).
Friend's avatars are ignored by default, but can be hidden by distance too if needed.
"Exclude Shown Avatars" will ignore to hide a persons avatar if you are showing the avatar.
"Disable Spawn Sounds" will only prevent a spawn sound from replaying when the avatar becomes visable again.

Tip:
If a friend is using an unoptimized avatar and you would like AvatarHider to hide it, disable "Ignore Friends" and enable "Exclude Shown Avatars".
Then show your friends avatars that you would like to be ignored by AvatarHider.
And set the friend with the unoptimized avatar to the "Use Safety Settings" in the QuickMenu.

## ChairExitController

Prevents you from falling out of chairs accidentally.
Press both triggers in VR, or q and e in desktop to leave chairs.

## CloningBeGone

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]

Turns off cloning when you join an instance.

You can configure whether you want cloning to be on or off based off instance type.
So for example, you can have cloning on in Invite+ worlds and off in all the other instance types.

You can also disable/enable cloning for a specific avatar. The buttons to control these can be toggled on and off.
Keep in mind however, that this requires the use of UIX and will overwrite the instance type cloning.

## PreviewScroller

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]

Let's you sort of scroll the avatar preview so you can control where it's facing.

![GIF of the Scrolling in Action](https://i.imgur.com/D2JVwnD.mp4)

## PrivateInstanceIcon

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]

Adds an icon to the social menu that shows if you can join a person or not.

Let's you configure what the mod does when a user is in a certain world type.
There are 3 things the mods does:

- Hides the user from the list
- Shows an icon indicating what world type they're in
- Acts like default VRChat

Each of these behaviors can be applied to users in:

- Private worlds (or non-joinable mode)
- Private worlds that you can join (Join me but in private)
- Friends only worlds
- Friends plus worlds
- Public worlds

You can also configure the mod to not affect the favorite users lists.

![Picture of me with the icon on it](https://i.imgur.com/T0Z0uba.png)

## ReloadAvatars

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]
[![Requires UIExpansionKit][UIXRequiredBadge]][UIXLink]

Adds buttons to reload a single user's avatar or all users' avatar.

The buttons can each be toggled on and off using UIX

## RememberMe

Adds a "Remember Me" check-box to the Login screen.
When "Remember Me" is checked off it will auto-fill the last saved VRChat Credentials.

## TriggerESP

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]
[![Requires UIExpansionKit][UIXRequiredBadge]][UIXLink]

Will highlight all VRChat interactables as well as any Unity UI buttons.

The color the ESP is customizable, and you can also set the color as random.
The strength of the ESP is customizable as well.

Note that it disables itself in worlds that don't allow risky functions.

![Picture of Outline](https://i.imgur.com/QnawlKb.jpg)
![Picture of Wireframe](https://i.imgur.com/nnTN4na.jpg)

## UserHistory

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]
[![Optionally uses UIExpansionKit][UIXOptionalBadge]][UIXLink]

Shows you when an user joined, and when clicking on them, opens when in the user page.
It's basically a copy + paste of [InstanceHistory](https://github.com/Nirv-git/VRC-Mods).

It is highly recommended to use UIX because it's just easier, although there are preferences to change the position of the regular button if you don't like UIX.

## UserInfoExtensions

[![Requires VRChatUtilityKit][VRCUKRequiredBadge]][VRCUKLink]
[![Requires UIExpansionKit][UIXRequiredBadge]][UIXLink]

Adds buttons to the to make VRChat more convenient.

Adding individually toggleable buttons that allow you to:

- Select a user in the Quick Menu from the Social Menu page.
- Find the avatar's author from the Social Menu and Avatar Menu pages.
- Open the links the selected user has in their bio.
- Display the languages the selected user has in their bio.

The buttons can always be accessed in a popup attached to the User Details Page.

Additionally, in the popup you can see the user's:

- username (what the person logs in with)
- platform (Quest or PC)
- last login (literal login, not starting the game)
- date joined (date original unmerged account created)
- friend number (the number friend they are. like 1st friend, 2nd friend, etc.)

For avatars, you can see their:

- author's name
- name
- supported platforms
- release type
- time which they were last updated
- version

## VRChatUtilityKit

Various sets of utilities for developers to use in their mods.

### For Developers

If you wish to use this mod, please respect the license, LGPL v3. You can read more below.
The source is documented, and the XML file is included in the release.
To utilise the XML file, just put it in the same directory as the copy of the utility kit you are referencing.

### Licensing

This library is licensed under LGPL v3.
This means that you are allowed to reference the library in your code as long as you disclose source and have a license and copyright notice you will be fine.
In the case that you would like to modify or include the library in your mod, you must use the same license as well as state any changes.

We will seek to punish license violations.

There is some code that was originally licensed under GPL v3, however express permission has been granted to license said code under LGPL v3.

## Credits

Here is a long list of the awesome people who have helped make these mods a reality:

- [Loukylor](https://github.com/loukylor) for being the original author of a tons of things here, as this repo was forked from his.
- [knah](https://github.com/knah) for [Join Notifier's](https://github.com/knah/VRCMods) join/leave and asynchronous utilities.
- [DubyaDude](https://github.com/DubyaDude) for [RubyButtonAPI](https://github.com/DubyaDude/RubyButtonAPI) as reference for the button API.
- [Psychloor](https://github.com/Psychloor) for the risky functions check.
- [Brycey92](https://github.com/Brycey92) for AvatarHider contributions.
- [dave-kun](https://github.com/dave-kun) for RememberMe & AvatarHider contributions.
- [ImTiara](https://github.com/ImTiara) for the original version of AvatarHider
- [KortyBoi](https://github.com/KortyBoi) for PlayerList's layout & help with getting some of the information.
- Frostbyte for contributing to PlayerList's optimizations
- [HerpDerpinstine](https://github.com/HerpDerpinstine) for being the original author of RememberMe
- [neitri](https://github.com/netri) for the ["Distance Face Outline" shader](https://github.com/netri/Neitri-Unity-Shaders) that was modified to create the TriggerESP shader.
- Potato for PreviewScroller contributions.
- [Sarayalth](https://github.com/Sarayalth) for a lot of contributions to many of the mods here.
- [ljoonal](https://github.com/ljoonal) for various modifications, cleanup, and management.
- [Nirv](https://github.com/Nirv-git) for taking up maintaining InstanceHistory in [their repository](https://github.com/Nirv-git/VRC-Mods) and more.
- [Adnezz](https://github.com/Adnezz) for taking up maintaining PlayerList in [their repository](https://github.com/Adnezz/PlayerList)
- [SDraw](https://github.com/SDraw) for VRCUK fix help

In a way, it's what the "Sleepers" means as the author name.
A reference to everyone who has contributed to these mods in one way or another.

The list would become too long to list everything, but it should hopefully cover at least the biggest contributions.
If you notice that the list is missing someone or something, do create a PR!

[VRCUKRequiredBadge]: https://img.shields.io/badge/VRCUK-Required-important?style=flat
[VRCUKLink]: https://github.com/SleepyVRC/Mods/releases
[UIXRequiredBadge]: https://img.shields.io/badge/UIX-Required-important?style=flat
[UIXOptionalBadge]: https://img.shields.io/badge/UIX-Optional-informational?style=flat
[UIXLink]: https://github.com/knah/VRCMods/
