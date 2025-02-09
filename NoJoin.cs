using Oxide.Core.Plugins;
using System.Collections.Generic;
using System.Timers;

namespace Oxide.Plugins
{
    [Info("NoJoin", "BravoEchoN", "1.1.0")]
    [Description("Blocks player connections with a maintenance message, toggleable via chat.")]
    public class NoJoin : CovalencePlugin
    {
        private bool isEnabled = false;
        private const string PermissionBypass = "nojoin.bypass";
        private const string PermissionToggle = "nojoin.toggle";
        private const string PermissionManageBypass = "nojoin.managebypass";
        private Timer statusUpdateTimer;
        private const double DefaultUpdateInterval = 60000; // Default interval: 60 seconds

        private void Init()
        {
            permission.RegisterPermission(PermissionBypass, this);
            permission.RegisterPermission(PermissionToggle, this);
            permission.RegisterPermission(PermissionManageBypass, this);
            LoadDefaultMessages();
            SetupStatusUpdateTimer(DefaultUpdateInterval);
        }

        private void SetupStatusUpdateTimer(double interval)
        {
            if (statusUpdateTimer != null)
            {
                statusUpdateTimer.Stop();
                statusUpdateTimer.Dispose();
            }

            statusUpdateTimer = new Timer(interval);
            statusUpdateTimer.Elapsed += (sender, e) => CheckAndUpdateStatus();
            statusUpdateTimer.AutoReset = true;
            statusUpdateTimer.Start();
        }

        private void CheckAndUpdateStatus()
        {
            if (isEnabled)
            {
                Puts("[NoJoin] Maintenance mode is currently enabled.");
            }
        }

        private object CanUserLogin(string name, string id, string ip)
        {
            if (!isEnabled) return null;
            if (permission.UserHasPermission(id, PermissionBypass)) return null;
            return lang.GetMessage("MaintenanceMessage", this, id);
        }

        [ChatCommand("nojoin")]
        private void ToggleNoJoinCommand(BasePlayer player, string command, string[] args)
        {
            if (!permission.UserHasPermission(player.UserIDString, PermissionToggle))
            {
                player.ChatMessage(lang.GetMessage("NoPermission", this, player.UserIDString));
                return;
            }
            isEnabled = !isEnabled;
            string status = isEnabled ? "enabled" : "disabled";
            Puts($"[NoJoin] Maintenance mode has been {status}.");
            player.ChatMessage(string.Format(lang.GetMessage("ToggleMessage", this, player.UserIDString), status));
        }

        [ChatCommand("nojoin_add")]
        private void AddBypassCommand(BasePlayer player, string command, string[] args)
        {
            if (!permission.UserHasPermission(player.UserIDString, PermissionManageBypass))
            {
                player.ChatMessage(lang.GetMessage("NoPermission", this, player.UserIDString));
                return;
            }
            if (args.Length == 0)
            {
                player.ChatMessage(lang.GetMessage("UsageAddBypass", this, player.UserIDString));
                return;
            }
            string targetId = args[0];
            permission.GrantUserPermission(targetId, PermissionBypass, this);
            player.ChatMessage(string.Format(lang.GetMessage("BypassAdded", this, player.UserIDString), targetId));
        }

        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                { "MaintenanceMessage", "Server is currently down for maintenance. Please try again later." },
                { "NoPermission", "You do not have permission to use this command." },
                { "ToggleMessage", "NoJoin: {0}" },
                { "UsageAddBypass", "Usage: /nojoin_add <SteamID>" },
                { "BypassAdded", "User {0} has been added to the bypass list." }
            }, this);
        }
    }
}
