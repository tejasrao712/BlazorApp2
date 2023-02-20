namespace BlazorApp2.Client.Models
{
    public class CalculationModel
    {
        public int? FirstNumber { get; set; }
        public int? SecondNumber { get; set; }
        public string SelectedOperation { get; set; }
        public double? Result { get; set; }
    }
    public class Calculationtransaction
    {
        public int Id { get; set; } 
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public string Operation { get; set; }
        public int Result { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}