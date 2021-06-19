using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurePreventing.Handlers
{
    internal class MedicalItemUsed
    {
        public void OnMedicalItemUsed(UsedMedicalItemEventArgs ev)
        {
            if (ev.Item != ItemType.SCP500) { return; }

            if (ev.Player.SessionVariables.ContainsKey("Used 500"))
            {
                ev.Player.SessionVariables["Used 500"] = GetActivePills(ev.Player) + 1;
            }
            else
            {
                ev.Player.SessionVariables.Add("Used 500", 1);
            }

            CurePreventing.Instance.Coroutines.Add(
                Timing.CallDelayed(CurePreventing.config.PreventionTime, () =>
                {
                    var ActivePills = GetActivePills(ev.Player);

                    if (ActivePills <= 1)
                    {
                        ev.Player.SessionVariables.Remove("Used 500");
                    }
                    else
                    {
                        ev.Player.SessionVariables["Used 500"] = ActivePills - 1;
                    }
                })
            );
        }

        // Player.SessionVariables values are type object, so compiler error if they're accessed directly
        private int GetActivePills(Player player) => Convert.ToInt32(player.SessionVariables["Used 500"]);
    }
}