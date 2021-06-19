using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurePreventing.Handlers
{
    internal class Spawning
    {
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("Used 500"))
            {
                ev.Player.SessionVariables["Used 500"] = 0;
            }
        }
    }
}