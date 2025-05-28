using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AILightAttack", story: "[AI] Light Attack", category: "Action", id: "a7cb9254a1d5de3bdf8ddeab10296d14")]
public partial class AiLightAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<AIController> AI;

    protected override Status OnUpdate()
    {
        AI.Value.OnAttack?.Invoke();
        return Status.Success;
    }
}

