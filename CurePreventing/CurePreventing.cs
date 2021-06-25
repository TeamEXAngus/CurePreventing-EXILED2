using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerHandler = Exiled.Events.Handlers.Player;

namespace CurePreventing
{
    public class CurePreventing : Plugin<Config>
    {
        private static CurePreventing singleton = new CurePreventing();
        public static CurePreventing Instance => singleton;
        public static Config config => Instance.Config;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        public override Version RequiredExiledVersion { get; } = new Version(2, 10, 0);
        public override Version Version { get; } = new Version(1, 0, 0);

        public List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();

        private Handlers.Dying dying;
        private Handlers.MedicalItemUsed medicalItemUsed;
        private Handlers.Spawning spawning;
        private Handlers.Hurting hurting;

        private CurePreventing()
        {
        }

        //Run startup code when plugin is enabled
        public override void OnEnabled()
        {
            RegisterEvents();
        }

        //Run shutdown code when plugin is disabled
        public override void OnDisabled()
        {
            foreach (var coro in Coroutines)
            {
                Timing.KillCoroutines(coro);
            }

            UnregisterEvents();
        }

        //Plugin startup code
        public void RegisterEvents()
        {
            dying = new Handlers.Dying();
            medicalItemUsed = new Handlers.MedicalItemUsed();
            spawning = new Handlers.Spawning();
            hurting = new Handlers.Hurting();

            PlayerHandler.Dying += dying.OnDying;
            PlayerHandler.MedicalItemUsed += medicalItemUsed.OnMedicalItemUsed;
            PlayerHandler.Spawning += spawning.OnSpawning;
            PlayerHandler.Hurting += hurting.OnHurting;
        }

        //Plugin shutdown code
        public void UnregisterEvents()
        {
            PlayerHandler.Dying -= dying.OnDying;
            PlayerHandler.MedicalItemUsed -= medicalItemUsed.OnMedicalItemUsed;
            PlayerHandler.Spawning -= spawning.OnSpawning;
            PlayerHandler.Hurting -= hurting.OnHurting;

            dying = null;
            medicalItemUsed = null;
            spawning = null;
            hurting = null;
        }
    }
}