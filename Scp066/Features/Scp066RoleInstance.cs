using CustomAbilityLib.API;
using CustomPlayerEffects;
using CustomRoleLib.API;
using CustomRoleLib.API.DefaultComponents.Generic;
using Scp066.Features.Abilities;

namespace Scp066.Features;

public class Scp066RoleInstance : RoleInstanceBase, IInitializable
{
    public void OnInitialized()
    {
        Owner.MaxHealth = 2500f;
        Owner.Health = 2500f;

        Owner.EnableEffect<Fade>(255, 0f);
        Owner.EnableEffect<Slowness>(25, 0f);
        Owner.EnableEffect<SilentWalk>(255, 0f);

        Owner.CustomInfo = "";

        Owner.SendHint(Scp066Plugin.Singleton.Config.SpawnBroadcast, Scp066Plugin.Singleton.Config.SpawnBroadcastDuration);

        CustomAbilityManager.TryGiveAbility<PlayEric>(Owner);
        CustomAbilityManager.TryGiveAbility<PlayNotes>(Owner);
        CustomAbilityManager.TryGiveAbility<PlayNoise>(Owner);
    }
}
