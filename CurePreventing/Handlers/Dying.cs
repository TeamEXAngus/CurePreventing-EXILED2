using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;

namespace CurePreventing.Handlers
{
    internal class Dying
    {
        public void OnDying(DyingEventArgs ev)
        {
            Log.Debug($"{ev.Target.Nickname}({ev.Target.Role}) was killed by {ev.Killer.Nickname}({ev.Killer.Role})",
                CurePreventing.Instance.Config.ShowDebugMessages);

            if (ev.Killer.Role != RoleType.Scp049 ||
                !ev.Target.SessionVariables.TryGetValue("Used 500", out object ActivePills))
            {
                return;
            }

            if (Convert.ToInt32(ActivePills) > 0)
            {
                Log.Debug($"{ev.Target.Nickname} was protected by SCP-500; They have {ActivePills} levels of protection",
                    CurePreventing.Instance.Config.ShowDebugMessages);

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