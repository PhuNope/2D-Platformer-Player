using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState {
    protected bool isAbilityDone;

    protected Movement Movement { get => movement ??= core.GetComponent<Movement>(); }
    protected CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetComponent<CollisionSenses>(); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    private bool isGrouned;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void DoChecks() {
        base.DoChecks();

        if (CollisionSenses) {
            isGrouned = CollisionSenses.Ground;
        }
    }

    public override void Enter() {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (isAbilityDone) {
            if (isGrouned && Movement?.CurrentVelocity.y < 0.01f) {
                stateMachine.ChangeState(player.IdleState);
            }
            else {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
