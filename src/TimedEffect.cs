using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By : Jesse Mitchell
     * 
     * This is a timed effect, it will timeout and deactivate itself.
     * 
     */
    [Serializable()]
    class TimedEffect : GameEffect
    {
        [NonSerialized()]
        private System.Timers.Timer timer;
        private int time;
        private int rechargeTime;
        private bool recharged;

        public TimedEffect(int time, int rechargeTime) : base()
        {
            this.time = time;
            this.rechargeTime = rechargeTime;
            this.timer = new System.Timers.Timer();
            recharged = true;
        }

        public override void activate()
        {
           if(timer == null)
           {
               this.timer = new System.Timers.Timer();
           }
           if(isRecharged() && !isActivated())
            {
                activated = true;
                recharged = false;
                this.timer.Interval = time;
                this.timer.Elapsed += deactivateEffect;
                this.timer.Enabled = true;
            }
        }

        public bool isRecharged()
        {
            return recharged;
        }

        private void deactivateEffect(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.timer.Elapsed -= deactivateEffect;
            deactivate();
            if(rechargeTime == 0)
            {
                recharged = true;
            } else
            {
                this.timer.Elapsed += recharge;
                this.timer.Interval = rechargeTime;
            }
        }

        private void recharge(object sender, System.Timers.ElapsedEventArgs e)
        {
            recharged = true;
            this.timer.Enabled = false;
            this.timer.Elapsed -= recharge;
        }

        public override void deactivate()
        {
            activated = false;
        }
    }
}
