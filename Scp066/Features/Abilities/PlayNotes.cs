using System.IO;
using LabApi.Loader.Features.Paths;
using RoleAPI.API.Abilities;
using UnityEngine;

namespace Scp066.Features.Abilities;

public class PlayNotes : AbilityBase
{
    public override string Name => "\ud83c\udfb6 Note";
    public override string Description => "Play back random creepy notes";
    public override KeyCode DefaultKey => KeyCode.R;
    public override float Cooldown => 10f;
    
    protected override void OnExecute(AbilityExecutionContext context)
    {
        context.LocksDuringExecution  = true;
        var value = Random.Range(0, 6) + 1;
        context.SoundFile = Path.Combine(PathManager.Configs.FullName, "Scp066", $"Notes{value}.ogg");
    }
}