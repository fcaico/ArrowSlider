using System;
using System.Collections.Generic;

namespace Fcaico.Controls.ArrowSlider
{
    public abstract class ArrowSliderDelegate<T> 
    {
        public List<T> Values
        {
            get;
            set;
        }

        public T CurrentValue
        {
            get;
            set;
        }

        public ArrowSliderDelegate ()
        {
        }


    }
}
             
