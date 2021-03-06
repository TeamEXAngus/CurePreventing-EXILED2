using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System;

namespace CurePreventing.Handlers
{
    internal class MedicalItemUsed
    {
        public void OnMedicalItemUsed(UsedMedicalItemEventArgs ev)
        {
            Log.Debug($"{ev.Player.Nickname} used {ev.Item}",
                CurePreventing.Instance.Config.ShowDebugMessages);

            if (ev.Item != ItemType.SCP500) { return; }

            if (ev.Player.SessionVariables.ContainsKey("Used 500"))
            {
                ev.Player.SessionVariables["Used 500"] = GetActivePills(ev.Player) + 1;
            }
            else
            {
                ev.Player.SessionVariables.Add("Used 500", 1);
            }

            Log.Debug($"Updated {ev.Player.Nickname}'s protection; It now is {ev.Player.SessionVariables["Used 500"]}",
                CurePreventing.Instance.Config.ShowDebugMessages);

            CurePreventing.Instance.Coroutines.Add(
                Timing.CallDelayed(CurePreventing.config.PreventionTime, () =>
                {
                    var ActivePills = GetActivePills(ev.Player);

                    if (ActivePills <= 1)
                    {
                        ev.Player.SessionVariables["Used 500"] = 0;
                    }
                    else
                    {
                        ev.Player.SessionVariables["Used 500"] = ActivePills - 1;
                    }

                    Log.Debug($"{ev.Player.Nickname}'s protection wore off; It now is {ev.Player.SessionVariables["Used 500"]}",
                        CurePreventing.Instance.Config.ShowDebugMessages);
                })
            );
        }

        // Player.SessionVariables values are type object, so compiler error if they're accessed directly
        private int GetActivePills(Player player) => Convert.ToInt32(player.SessionVariables["Used 500"]);
    }
}