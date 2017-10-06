using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateHandling;

namespace States
{
    public class State
    {
        public String name;
        public Boolean isVisible;
        public Boolean isActive;

        public State(String name)
        {
            this.isVisible = false;
            this.isActive = false;
            this.name = name;
        }

        public Boolean getVisible()
        {
            return isVisible;
        }

        public void setVisible(Boolean value)
        {
            this.isVisible = value;
        }

        public Boolean getActive()
        {
            return isActive;
        }
        public void setActive(Boolean value)
        {
            this.isActive = value;
        }

    }
}
