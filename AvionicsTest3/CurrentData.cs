using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvionicsTest3
{
    public class CurrentData
    {
        public float[] altPID { get; set; } = new float[3];
        public float[] pitchPID { get; set; } = new float[3];
        public float[] bankPID { get; set; } = new float[3];
        public float[] speedPID { get; set; } = new float[3];
        public float[] headingPID { get; set; } = new float[3];
        public float[] runwayLineupPID { get; set; } = new float[3];

    }
}
