using CustomRoleLib;
using CustomRoleLib.API;
using CustomRoleLib.API.Attributes;
using CustomRoleLib.API.DefaultComponents;
using CustomRoleLib.API.DefaultComponents.Generic;
using PlayerRoles;

namespace Scp066.Features;

[CustomRole(RoleTypeId.Scp0492)]
[CustomRoleAttributeBase(typeof(Scp066AudioComponent))]
[CustomRoleAttributeBase(typeof(InitializerComponent<Scp066RoleInstance>))]
[CustomRoleAttributeBase(typeof(Scp066BehaviorComponent))]
[CustomRoleAttributeBase(typeof(Scp066SchematicComponent))]
[CustomRoleAttributeBase(typeof(RoleNameDisplayComponent))]
[CustomRoleAttributeBase(typeof(RoleReceivedHintComponent))]
[CustomSpawnpointRole<Scp066RoleInstance>(RoleTypeId.Scp096)]
public class Scp066Role : CustomRoleBase<Scp066RoleInstance>
{
    public override string Name => "SCP-066";
    public override string Description => "SCP-066, Eric's Toy. Plays sounds to disorient and kill humans.";
    public override string Id => "scp066";

    public override bool NaturallySpawnable => true;
    public override float RoleSpawnWeight => 10f;
    public override float RoleNotSpawnWeight => 90f;
    public override RoleTypeId[] RoleSpawnOriginalRoleIds => [RoleTypeId.Scp173];
}
