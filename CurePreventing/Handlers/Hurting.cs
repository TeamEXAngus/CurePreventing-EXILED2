using System;
using System.Collections.Generic;
using Exiled.Events.EventArgs;

namespace CurePreventing.Handlers
{
    internal class Hurting
    {
        public void OnHurting(HurtingEventArgs ev)
        {
            if (CurePreventing.Instance.Config.Hurting049ResetsProtection &&
                ev.Target.Role == RoleType.Scp049 &&
                ev.Attacker.SessionVariables.ContainsKey("Used 500"))
            {
                ev.Attacker.SessionVariables["Used 500"] = 0;
            }
        }
    }
}