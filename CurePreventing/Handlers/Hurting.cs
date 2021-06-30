using System;
using System.Collections.Generic;
using Exiled.Events.EventArgs;
using Exiled.API.Features;

namespace CurePreventing.Handlers
{
    internal class Hurting
    {
        public void OnHurting(HurtingEventArgs ev)
        {
            Log.Debug($"{ev.Target.Nickname}({ev.Target.Role}) was hurt by {ev.Attacker.Nickname}({ev.Attacker.Role})",
                CurePreventing.Instance.Config.ShowDebugMessages);

            if (CurePreventing.Instance.Config.Hurting049ResetsProtection &&
                ev.Target.Role == RoleType.Scp049 &&
                ev.Attacker.SessionVariables.ContainsKey("Used 500"))
            {
                ev.Attacker.SessionVariables["Used 500"] = 0;
                Log.Debug($"{ev.Attacker.Nickname}'s protection was reset",
                    CurePreventing.Instance.Config.ShowDebugMessages);
            }
        }
    }
}