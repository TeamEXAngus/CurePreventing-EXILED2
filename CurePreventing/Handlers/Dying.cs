using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;

namespace CurePreventing.Handlers
{
    internal class Dying
    {
        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Killer.Role != RoleType.Scp049) { return; }
            if (Convert.ToInt32(ev.Target.SessionVariables["Used 500"]) != 0)
            {
                Action<Player, ushort, string> DisplayMethod;
                if (CurePreventing.config.UseHints) { DisplayMethod = Hint; }
                else { DisplayMethod = Broadcast; }

                ev.IsAllowed = false;

                DisplayMethod(ev.Killer, CurePreventing.config.PreventionTime,
                             CurePreventing.config.Message.Replace("{player}", $"{ev.Target.Nickname}"));
            }
        }

        private void Broadcast(Player player, ushort time, string message)
        {
            player.ClearBroadcasts();
            player.Broadcast(time, message);
        }

        private void Hint(Player player, ushort time, string message)
        {
            player.ShowHint(message, time);
        }
    }
}