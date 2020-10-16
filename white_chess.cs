using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gobang
{
    class white_chess : chess
    {
        public white_chess(int x, int y) : base(x,y)
        {
            this.Image = Properties.Resources.white;
            
        }
    }
}
