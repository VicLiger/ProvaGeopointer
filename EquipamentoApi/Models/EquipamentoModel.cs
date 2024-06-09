namespace EquipamentoApi.Models
{
    public class EquipamentoModel
    {
        public EquipamentoModel() { }

        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

    }
}
