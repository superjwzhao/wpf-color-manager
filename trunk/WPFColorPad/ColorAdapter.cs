using System.Windows.Media;

namespace BCharppe.WPFColorManager
{
    public class ColorAdapter
    {




        public string Name { get; set; }
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public string Hexa { get; set; }

        public ColorAdapter(Color color, string name)
        {
            Name = name;
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
            Hexa = color.ToString();
        }

        public string Details
        {
            get { return string.Format("A:{0}, R:{1}, G:{2}, B:{3}", A, R, G, B); }
        }
    }
}