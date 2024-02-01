using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State {

    protected Movement Movement { get => movement ??= core.GetComponent<Movement>(); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetComponent<CollisionSenses>(); }
    private CollisionSenses collisionSenses;

    protected D_MoveState stateData;

    protected bool isDectectingWall;
    protected bool isDectectingLedge;
    protected bool isPlayerInMinAgroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void DoChecks() {
        base.DoChecks();

        if (CollisionSenses) {
            isDectectingLedge = CollisionSenses.LedgeVertical;
            isDectectingWall = CollisionSenses.WallFront;
            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        }
    }

    public override void Enter() {
        base.Enter();

        Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
