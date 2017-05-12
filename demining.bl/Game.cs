namespace demining.bl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        public Field[,] Fields { get; set; }
        public int NumberOfMines { get; set; }
        public int NumberOfFlags { get; set; }
        public Status Status { get; set; }

        public Game(int x, int y, int mines)
        {
            Fields = new Field[y, x];

            NumberOfMines = mines;

            NumberOfFlags = 0;

            Status = Status.Running;
        }

        public Status CreateGameField()
        {
            var numbersOfFields = InitializeFields();

            if (NumberOfMines > 0)
            {
                DistributeMinesByRandom(numbersOfFields);

                if (NumberOfMines < numbersOfFields)
                {
                    CalculateNeighboursAndFieldValues();
                    return Status = Status.Running;
                }
            }

            return Status = Status.Won;
        }
        
        public string SwitchFieldmarkerForMine(Field field)
        {
            if (field.Detected)
            {
                return field.Value;
            }

            switch (field.Flag)
            {
                case Mark.yes:
                    NumberOfFlags -= 1;
                    field.Flag = Mark.maybe;
                    return "?";

                case Mark.maybe:
                    field.Flag = Mark.no;
                    return string.Empty;

                case Mark.no:

                    if (NumberOfFlags == NumberOfMines)
                    {
                        field.Flag = Mark.maybe;
                        return "?";
                    }

                    field.Flag = Mark.yes;
                    NumberOfFlags += 1;
                    return "!";

                default:
                    return string.Empty;
            }
        }

        public bool IsThereAMine()
        {
            var actualFieldList = Fields.Cast<Field>().ToList();

            int numberOfSuspectedMines = actualFieldList.Count(w => w.Flag == Mark.yes && w.Type == Type.Mine);
            
            if (numberOfSuspectedMines == NumberOfMines)
            {
                return true;
            }

            int numberOfDetectedFields = actualFieldList.Count(w => w.Detected);

            if (numberOfDetectedFields + NumberOfMines == Fields.Length)
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Erstellt undefinierte Räumen auf dem Spielfeld
        /// </summary>
        /// <returns>Gibt die aktuelle Anzahl aller Räume zurück.</returns>
        private int InitializeFields()
        {
            if (Fields.GetLength(0) == 0 || Fields.GetLength(1) == 0)
            {
                throw new Exception("Es wurde kein Spielfeld definiert.");
            }

            for (int y = 0; y < Fields.GetLength(0); y++)
            {
                for (int x = 0; x < Fields.GetLength(1); x++)
                {
                    Fields[y, x] = new Field();
                }
            }

            return Fields.GetLength(0) * Fields.GetLength(1);
        }

        /// <summary>
        /// Verteilt zufällig die angegebene Anzahl an Minen auf dem Spielfeld
        /// </summary>
        /// <param name="numberOfFields">Anzahl aller Felder auf dem Spielfeld.</param>
        private void DistributeMinesByRandom(int numberOfFields)
        {
            var numberOfFieldsWithMines = NumberOfMines > numberOfFields ? numberOfFields : NumberOfMines;

            var fieldArray = new string[numberOfFieldsWithMines];

            var fieldValue = string.Empty;

            for (var i = 0; i < numberOfFieldsWithMines; i++)
            {
                do
                {
                    var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    fieldValue = rnd.Next(0, numberOfFields).ToString();
                }
                while (fieldArray.Contains(fieldValue));

                fieldArray[i] = fieldValue;
            }

            foreach (string value in fieldArray)
            {
                int x = int.Parse(value) / Fields.GetLength(0);
                int y = int.Parse(value) % Fields.GetLength(0);

                Fields[y, x] = new Field(true);
            }
        }

        /// <summary>
        /// Berechnet die Feldwerte in Bezug auf die gelegten Minen im Spiel.
        /// Setzt die Position in allen Feldern
        /// Erstellt Nachbarschaftsliste für alle Felder
        /// 1 2 3
        /// 4 X 5
        /// 6 7 8
        /// </summary>
        private void CalculateNeighboursAndFieldValues()
        {
            for (int y = 0; y < Fields.GetLength(0); y++)
            {
                for (int x = 0; x < Fields.GetLength(1); x++)
                {
                    Fields[y, x].Position = new Position(x, y);
                    
                    List<Field> neighbours = new List<Field>();

                    if ((y - 1 >= 0) && (x - 1 >= 0))
                        neighbours.Add(Fields[y - 1, x - 1]);
                    if ((y - 1 >= 0))
                        neighbours.Add(Fields[y - 1, x]);
                    if ((y - 1 >= 0) && (x + 1 < Fields.GetLength(1)))
                        neighbours.Add(Fields[y - 1, x + 1]);
                    if ((x - 1 >= 0))
                        neighbours.Add(Fields[y, x - 1]);
                    if ((x + 1 < Fields.GetLength(1)))
                        neighbours.Add(Fields[y, x + 1]);
                    if ((y + 1 < Fields.GetLength(0)) && (x - 1 >= 0))
                        neighbours.Add(Fields[y + 1, x - 1]);
                    if ((y + 1 < Fields.GetLength(0)))
                        neighbours.Add(Fields[y + 1, x]);
                    if ((y + 1 < Fields.GetLength(0)) && (x + 1 < Fields.GetLength(1)))
                        neighbours.Add(Fields[y + 1, x + 1]);

                    Fields[y, x].Neighbours.AddRange(neighbours);

                    if (Fields[y, x].Type != Type.Mine)
                    {
                        Fields[y, x].Value = neighbours.Where(w => w.Type == Type.Mine).ToList().Count.ToString();
                        Fields[y, x].Value = (Fields[y, x].Value == "0") ? string.Empty : Fields[y, x].Value;
                    }
                }
            }
        }
    }
}
