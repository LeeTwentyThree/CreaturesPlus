using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreCreatures
{
    public class DiveAction : CreatureAction
    {
        public override float Evaluate(Creature creature)
        {
            if((creature.transform.position.y > Ocean.main.GetOceanLevel() - 90f || creature.transform.position.y < Ocean.main.GetOceanLevel() - 275f) && creature.transform.position.y > Ocean.main.GetOceanLevel() - 400f)
            {
                return 0.75f;
            }
            return 0f;
        }

        public override void StartPerform(Creature creature)
        {
            swimBehaviour.SwimTo(new Vector3(transform.position.x, -175f, transform.position.z), 25f);
        }
    }
}
