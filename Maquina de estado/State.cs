
public abstract class State
{
    public abstract void Enter();
    //Es el equivalente al Update
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
}
