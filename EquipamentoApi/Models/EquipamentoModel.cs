namespace EquipamentoApi.Models
{
    public class EquipamentoModel
    {
        protected EquipamentoModel() { }

        public string Tag { get; private set; }
        public string Name { get; private set; }
        public string File { get; private set; }
        public string State { get; private set; }
        public DateTime Data { get; private set; }

    }
}
