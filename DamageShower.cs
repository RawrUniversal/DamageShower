using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using Logger = Rocket.Core.Logging.Logger;

namespace DamageShower
{
    public class DamageShower : RocketPlugin
    {
        public static string version = "1.0.0";
        public static DamageShower Instance;
        public List<string> damage = new List<string>();

        protected override void Load()
        {
            Instance = this;
            Logger.Log("This Plugin was made by: [RAR] Metshival", ConsoleColor.Green);
            Logger.Log("Version: " + version, ConsoleColor.Green);
            DamageTool.damagePlayerRequested += UnturnedEvents_OnPlayerDamaged;
        }

        private void UnturnedEvents_OnPlayerDamaged(ref DamagePlayerParameters pars, ref bool canDamage)
        {
            try
            {
                UnturnedPlayer player = UnturnedPlayer.FromPlayer(pars.player);
                UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(pars.killer);
                if (damage.Contains(killer.Id))
                {

                    PlayerEquipment pe = killer.Player.equipment;
                    ItemJar item = killer.Inventory.getItem(pe.equippedPage, player.Inventory.getIndex(pe.equippedPage, pe.equipped_x, pe.equipped_y));
                    UnturnedChat.Say(killer, string.Format("Damage: {0}, Limb: {1}, and Weapon: {2} ({3})", (pars.damage * pars.times), LimbToName(pars.limb), item.interactableItem.asset.itemName, item.item.id));
                    canDamage = false;
                    return;
                }
            }
            catch (Exception) { }
        }

        public string LimbToName(ELimb limb)
        {
            string Part = "???";
            if (limb == ELimb.SKULL)
            {
                Part = ("Head");
            }
            else if (limb == ELimb.SPINE)
            {
                Part = ("Body");
            }
            else if (limb == ELimb.LEFT_ARM || limb == ELimb.RIGHT_ARM || limb == ELimb.LEFT_HAND || limb == ELimb.RIGHT_HAND)
            {
                Part = ("Arm");
            }
            else if (limb == ELimb.LEFT_LEG || limb == ELimb.RIGHT_LEG || limb == ELimb.LEFT_FOOT || limb == ELimb.RIGHT_FOOT)
            {
                Part = ("Leg");
            }
            return Part;
        }

        protected override void Unload()
        {
            Logger.Log("This Plugin was made by: [RAR] Metshival", ConsoleColor.Green);
            Logger.Log("Version: " + version, ConsoleColor.Green);
            DamageTool.damagePlayerRequested -= UnturnedEvents_OnPlayerDamaged;
        }
    }
}