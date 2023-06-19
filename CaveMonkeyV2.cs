using MelonLoader;
using BTD_Mod_Helper;
using CaveMonkeyV2;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using System.Media;
using System.IO;
using System.Security.Policy;
using BTD_Mod_Helper.Api.Display;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using System;
using System.Text;
using System.Threading.Tasks;
using TimeTraveler.Displays.Projectiles;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

[assembly: MelonInfo(typeof(CaveMonkeyV2.CaveMonkeyV2), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace CaveMonkeyV2;

public class SplinterDisplay : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "SplinterDisplay");
    }
}
public class TrapDisplay : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "TrapDisplay");
    }
}
public class TrapFullDisplay : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "TrapFullDisplay");
    }
}
public class RockDisplay : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "RockDisplay");
    }
}
public class CaveMonkeyV2 : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<CaveMonkeyV2>("CaveMonkeyV2 loaded!");
    }
}
public class CaveMonkey : ModTower
{

    public override TowerSet TowerSet => TowerSet.Magic;
    public override string BaseTower => TowerType.CaveMonkey;
    public override int Cost => 450;
    public override int TopPathUpgrades => 5;
    public override int MiddlePathUpgrades => 5;
    public override int BottomPathUpgrades => 5;
    public override bool DontAddToShop => false;
    public override string Description => "The Cave Monkey joined the game";
    public override ParagonMode ParagonMode => ParagonMode.Base555;
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.GetAttackModel().weapons[0].projectile = Game.instance.model.GetTowerFromId("Sauda 3").GetAttackModel().weapons[0].projectile.Duplicate();
    }
}
public class PokeyClub : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => TOP;
    public override int Tier => 1;
    public override int Cost => 350;
    public override string Description => "A pokey club does more damage to bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        projectile.GetDamageModel().damage += 1;
    }
}
public class BigClub : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => TOP;
    public override int Tier => 2;
    public override int Cost => 500;
    public override string Description => "Club becomes bigger and does more damage but loses speed";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        attackModel.weapons[0].Rate *= 1.2f;
        projectile.GetDamageModel().damage += 1;
    }
}
public class HeavyThump : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => TOP;
    public override int Tier => 3;
    public override int Cost => 1600;
    public override string Description => "Club creates a small bang upon hitting the ground";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-200").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
        projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-200").GetAttackModel().weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
    }
}
public class StunningThud : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => TOP;
    public override int Tier => 4;
    public override int Cost => 5700;
    public override string Description => "Thudding now stuns bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        projectile.RemoveBehavior<CreateProjectileOnContactModel>();
        projectile.RemoveBehavior<CreateEffectOnContactModel>();
        var proj = Game.instance.model.GetTowerFromId("BombShooter-400").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
        proj.projectile.GetDamageModel().damage *= 2;
        projectile.AddBehavior(proj);
        projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-400").GetAttackModel().weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
    }
}
public class TheBigBang : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => TOP;
    public override int Tier => 5;
    public override int Cost => 57100;
    public override string Description => "Explosions on a gigantic scale";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        projectile.RemoveBehavior<CreateProjectileOnContactModel>();
        projectile.RemoveBehavior<CreateEffectOnContactModel>();
        var proj = Game.instance.model.GetTowerFromId("BombShooter-500").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
        proj.projectile.GetDamageModel().damage *= 2;
        projectile.AddBehavior(proj);
        projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-500").GetAttackModel().weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
    }
}
public class FasterClub : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => MIDDLE;
    public override int Tier => 1;
    public override int Cost => 350;
    public override string Description => "Attacks much faster";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        attackModel.weapons[0].Rate *= 0.9f;
    }
}
public class Splinters : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => MIDDLE;
    public override int Tier => 2;
    public override int Cost => 650;
    public override string Description => "Fires splinters at the bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;

        var splitmodel = Game.instance.model.GetTowerFromId("MonkeySub-002").GetWeapon().projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().Duplicate();
        splitmodel.projectile.ApplyDisplay<SplinterDisplay>();
        splitmodel.projectile.GetDamageModel().damage = 1;
        splitmodel.projectile.pierce = 1;
        towerModel.GetAttackModel().weapons[0].projectile.AddBehavior(splitmodel);
    }
}
public class Trap : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => MIDDLE;
    public override int Tier => 3;
    public override int Cost => 3650;
    public override string Description => "Places money making traps on the path";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.towerSelectionMenuThemeId = "SelectPointInput";
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        var trap = Game.instance.model.GetTowerFromId("EngineerMonkey-024").behaviors.First(a => a.name.Contains("BloonTrap")).Cast<AttackModel>().Duplicate();
        trap.range = 40;
        towerModel.range = 40;
        trap.weapons[0].projectile.pierce = 200;
        trap.weapons[0].projectile.GetBehavior<EatBloonModel>().rbeCapacity = 200;
        trap.weapons[0].projectile.GetBehavior<EatBloonModel>().rbeCashMultiplier = 2f;
        trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.pierce = 200;
        trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.GetBehavior<CashModel>().minimum = 400;
        trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.GetBehavior<CashModel>().maximum = 400;
        trap.weapons[0].projectile.ApplyDisplay<TrapDisplay>();
        trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.ApplyDisplay<TrapFullDisplay>();
        trap.weapons[0].Rate = 6;
        towerModel.AddBehavior(trap);
    }
}
public class DeeperTrap : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => MIDDLE;
    public override int Tier => 4;
    public override int Cost => 4750;
    public override string Description => "A deeper trap catches more bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        foreach (var attacks in towerModel.GetAttackModels())
        {
            if (attacks.name.Contains("Trap"))
            {
                var trap = attacks;
                trap.weapons[0].projectile.pierce = 600;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().rbeCapacity = 600;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().rbeCashMultiplier = 2.5f;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.pierce = 600;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.GetBehavior<CashModel>().minimum = 1500;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.GetBehavior<CashModel>().maximum = 1500;

            }

        }
    }
}
public class MoabTrap : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => MIDDLE;
    public override int Tier => 5;
    public override int Cost => 257000;
    public override string Description => "This BIG Trap can now catch moab class bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        foreach (var attacks in towerModel.GetAttackModels())
        {
            if (attacks.name.Contains("Trap"))
            {
                var trap = attacks;
                trap.weapons[0].projectile.RemoveBehavior<ProjectileFilterModel>();
                trap.weapons[0].projectile.pierce = 40000;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().rbeCapacity = 40000;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().rbeCashMultiplier = 2.5f;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.pierce = 40000;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.GetBehavior<CashModel>().minimum = 100000;
                trap.weapons[0].projectile.GetBehavior<EatBloonModel>().projectile.GetBehavior<CashModel>().maximum = 100000;

            }

        }
    }
}
public class RockThrow : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => BOTTOM;
    public override int Tier => 1;
    public override int Cost => 550;
    public override string Description => "Throws rocks at nearby bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        var rock = Game.instance.model.GetTowerFromId("DartMonkey-220").GetAttackModel().Duplicate();
        rock.name = "Rock_Weapon";
        rock.range = 50;
        rock.weapons[0].projectile.ApplyDisplay<RockDisplay>();
        towerModel.AddBehavior(rock);
        towerModel.range = 50;
    }
}
public class BetterAccuracy : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => BOTTOM;
    public override int Tier => 2;
    public override int Cost => 650;
    public override string Description => "Can now see camo bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        towerModel.AddBehavior(new OverrideCamoDetectionModel("Camo", true));
    }
}
public class SharperRock : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => BOTTOM;
    public override int Tier => 3;
    public override int Cost => 1650;
    public override string Description => "Rocks gain much more pierce and damage";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        foreach (var attacks in towerModel.GetAttackModels())
        {
            if (attacks.name.Contains("Rock"))
            {
                var proj = attacks.weapons[0].projectile;
                proj.pierce += 3;
                proj.GetDamageModel().damage += 1;
            }

        }
    }
}
public class BagOfRocks : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => BOTTOM;
    public override int Tier => 4;
    public override int Cost => 3650;
    public override string Description => "Gains a bag of rocks, and now throws 4 rocks at a time";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        foreach (var attacks in towerModel.GetAttackModels())
        {
            if (attacks.name.Contains("Rock"))
            {
                var proj = attacks.weapons[0].projectile;
                attacks.weapons[0].Rate *= .5f;
                attacks.weapons[0].emission = new ArcEmissionModel("ArcEmissionModel_", 4, 0, 20, null, false, false);
            }

        }
    }
}
public class RockRifle : ModUpgrade<CaveMonkey>
{
    public override string Portrait => "CaveMonkey-Portrait.png";
    public override int Path => BOTTOM;
    public override int Tier => 5;
    public override int Cost => 35650;
    public override string Description => "Shoots at a amazingly high speed";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        foreach (var attacks in towerModel.GetAttackModels())
        {
            if (attacks.name.Contains("Rock"))
            {
                var proj = attacks.weapons[0].projectile;
                attacks.weapons[0].Rate *= .10f;

            }

        }
    }
}
