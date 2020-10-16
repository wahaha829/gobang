using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gobang
{
    class black_chess : chess
    {
        public black_chess(int x, int y) : base(x,y)
        {
            this.Image = Properties.Resources.black;


        }
    }
}
