using UnityEngine;
using UnityEngine.UIElements;

namespace Cassette.State
{
    public class DropState : IState
    {
        Vector2 velocity;
        Vector2 acceleration;

        public override void Enter()
        {
            velocity.y = 10;
            acceleration.y = -10;
        }
        public override void Update()
        {
            Vector2 position = ChooseState.viewCassettes[ChooseState.viewMaxCount / 2].transform.position;
            if (position.y > -5)
            {
                velocity += acceleration * Time.deltaTime;
                position += velocity * Time.deltaTime;
            }
            else
            {
                position.y = -5;
            }
            ChooseState.viewCassettes[ChooseState.viewMaxCount / 2].transform.position = position;
        }
        public override void Exit()
        {

        }
    }
}
