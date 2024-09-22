namespace berkepite
{
    public class SlingshotHolding : SlingshotState
    {
        public override void EnterState(Slingshot context)
        {
            context.TrajectoryRenderer.Enable();
        }
        public override void UpdateState(Slingshot context)
        {
            if (context.TouchAction.IsPressed())
                context.HandleHolding();
            else
                context.HandleReleased();
        }
        public override void ExitState(Slingshot context)
        {
            context.TrajectoryRenderer.Disable();
        }
    }
}
