using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLA.utils
{
     class WindowSizeHelper
    {
        public int width {  get; set; }
        public int heigth {  get; set; }
        
        public string name { get; set; }    

        public WindowSizeHelper ()
        {
        }
        public string setName(string name)
        {
            this.name = name;
            return this.name;
        }
    }
}
