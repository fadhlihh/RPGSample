using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AIHeavyAttack", story: "[AI] Heavy Attack", category: "Action", id: "4039875881b8b5c85a0119da651926e5")]
public partial class AiHeavyAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<AIController> AI;

    protected override Status OnUpdate()
    {
        AI.Value.OnHeavyAttack?.Invoke();
        return Status.Success;
    }
}

