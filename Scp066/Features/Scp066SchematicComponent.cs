using System.Collections.Generic;
using CustomRoleLib.API.DefaultComponents;
using Mirror;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp066.Features;

public class Scp066SchematicComponent : ComponentBase<Scp066RoleInstance>
{
    private const string SchematicName = "Scp066";
    private static readonly Vector3 PositionOffset = new(0f, -1f, 0f);
    private static readonly Quaternion RotationOffset = Quaternion.Euler(0f, 90f, 0f);

    private readonly Dictionary<Scp066RoleInstance, SchematicObject> _schematics = new();

    public override void OnCreatedInstance(Scp066RoleInstance instance)
    {
        var schematic = ObjectSpawner.SpawnSchematic(SchematicName, PositionOffset, RotationOffset);
        if (schematic == null) return;

        schematic.transform.SetParent(instance.Owner.GameObject!.transform, false);
        _schematics[instance] = schematic;
    }

    public override void OnDestroyedInstance(Scp066RoleInstance instance)
    {
        if (!_schematics.TryGetValue(instance, out var schematic)) return;
        _schematics.Remove(instance);
        if (schematic != null)
            NetworkServer.Destroy(schematic.gameObject);
    }
}
