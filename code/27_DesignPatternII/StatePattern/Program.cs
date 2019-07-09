using System;

namespace Rextester
{
    public abstract class AbstractState{
        public Character figure;
        public AbstractState(Character figure){
            this.figure = figure;
        }
        public virtual void Provoked(){
            Console.WriteLine("'Provoked' transition is not supported in current state!");
        }
        public virtual void Addressed(){
            Console.WriteLine("'Addressed' transition is not supported in current state!");
        } 
        public virtual void DealingWith(){
            Console.WriteLine("'DealingWith' transition is not supported in current state!");
        }
    }

    public class HappyState : AbstractState{
        public HappyState(Character figure): base(figure) {}
        
        public override void Provoked(){
            Console.WriteLine("{0} is Happy but switches to Aggressive", figure.name);
            figure.setState(new AggressiveState(figure));
        }

        public override void Addressed(){
            Console.WriteLine("{0} is Happy", figure.name);
        }
        
        public override void DealingWith(){
            Console.WriteLine("{0} is DealingWith", figure.name);
        }
    }

    public class AggressiveState : AbstractState{
        public AggressiveState(Character figure): base(figure) {}
        
        public override void Provoked(){
            Console.WriteLine("{0} is Agrgessive", figure.name);
        }

        public override void Addressed(){
            Console.WriteLine("{0} is Aggressive but switches to Neutral", figure.name);
            figure.setState(new NeutralState(figure));
        }
        
        public override void DealingWith(){
            Console.WriteLine("{0} is Aggressive but switches to Neutral", figure.name);
            figure.setState(new NeutralState(figure));
        }
    }

    public class NeutralState : AbstractState{
        public NeutralState(Character figure): base(figure) {}
        
        public override void Provoked(){
            Console.WriteLine("{0} is Neutral but swiches to Agrgessive", figure.name);
            figure.setState(new AggressiveState(figure));
        }

        public override void Addressed(){
            Console.WriteLine("{0} is Neutral", figure.name);
        }
        
        public override void DealingWith(){
            Console.WriteLine("{0} is Aggressive but switches to Neutral", figure.name);
            figure.setState(new HappyState(figure));
        }
    }

    public class Character{

        private AbstractState currentState;
        public string name;

        public Character(string name){
            this.name = name;
            currentState = new HappyState(this);
        }

        public void setState(AbstractState newState){
            currentState = newState;
        }

        public void Addressed(){
            currentState.Addressed();
        }

        public void Provoked(){
            currentState.Provoked();
        }

        public void DealingWith(){
            currentState.DealingWith();
        }


    }
    public class Program {
        public static void Main(string[] args){
            Character Golum = new Character("Golum");
            Golum.Provoked();
            Golum.DealingWith();

        }
    }
}