namespace demining.bl
{
    using System.Collections.Generic;

    public class Field
    {
        public bool Detected { get; set; }
        public Type Type { get; set; }
        public Mark Flag { get; set; }
        public string Value { get; set; }
        public Position Position { get; set; }
        public List<Field> Neighbours { get; set; }

        public Field()
        {
            Detected = false;
            Type = Type.Count;
            Value = "";
            Flag = Mark.no;
            Position = new Position(-1, -1);
            Neighbours = new List<Field>();
        }

        public Field(bool mine) : this()
        {
            if (mine)
            {
                Type = Type.Mine;
                Value = "";
            }
        }
    }
}