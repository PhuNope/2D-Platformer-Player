using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State {

    protected Movement Movement { get => movement ??= core.GetComponent<Movement>(); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetComponent<CollisionSenses>(); }
    private CollisionSenses collisionSenses;

    protected D_LookForPlayerState stateData;

    protected bool turnImmediately;
    protected bool isPLayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;

    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void DoChecks() {
        base.DoChecks();

        isPLayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter() {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        Movement?.SetVelocityX(0f);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);

        if (turnImmediately) {
            Movement?.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone) {
            Movement?.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns) {
            isAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone) {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool flip) {
        turnImmediately = flip;
    }
}
