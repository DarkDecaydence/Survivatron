using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameActions;
using Survivatron.GameEvents;
using Survivatron.GameObjects.Dynamics;
using Survivatron.MapSpecs;
using Survivatron.Mastermind.Tactics;


namespace Survivatron.Mastermind
{
    public class MastermindAI
    {
        private static MastermindAI _instance;
        public static MastermindAI Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MastermindAI();
                    return _instance;
                }
                else
                { return _instance; }
            }
        }


        private List<DynamicAI> minions;
        private bool done = false;
        private int IDCounter = 0;

        private MastermindAI()
        { minions = new List<DynamicAI>(); }

        public Dynamic AddSheep()
        {
            Sheep newSheep = new Sheep(++IDCounter);
            minions.Add(newSheep);
            return newSheep;
        }

        public void Update()
        {
            if (!done)
            {
                foreach (DynamicAI d in minions)
                { d.NextAction = d.AITactic.CalculateNextAction(); }
                done = true;
            }
        }
    }
}
