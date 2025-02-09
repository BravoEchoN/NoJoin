The NoJoin plugin allows server administrators to temporarily block new player connections during maintenance or other downtime. When enabled, players attempting to join the server will receive a customizable maintenance message, while players with the appropriate bypass permission can still join. The plugin also includes commands to toggle the maintenance mode and manage bypass permissions, making it a versatile tool for server management.

Key Features:

Maintenance Mode: Block new player connections with a customizable message.

Bypass Permissions: Grant specific players or groups the ability to join even during maintenance.

Chat Commands: Easily enable or disable maintenance mode and manage bypass permissions via chat commands.

Automatic Status Updates: Regularly log the current status of maintenance mode in the console.

Customizable Messages: All messages are configurable, allowing you to tailor the plugin to your server's needs.

Commands:

/nojoin - Toggle maintenance mode on or off.

/nojoin_add <SteamID> - Add a player to the bypass list.

Permissions:

nojoin.bypass - Allows a player to join during maintenance.

nojoin.toggle - Allows a player to toggle maintenance mode.

nojoin.managebypass - Allows a player to manage the bypass list.

Installation:

Download the NoJoin.cs file.

Place it in your server's oxide/plugins directory.

Restart or reload the server.

Configuration:
The plugin comes with default messages, but you can customize them by editing the NoJoin.json file in the oxide/config directory.
