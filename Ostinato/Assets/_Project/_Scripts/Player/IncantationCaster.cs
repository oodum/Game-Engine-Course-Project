using Utility;
using Ostinato.Core.Incantations;
namespace Combat {
    public interface IIncantationCaster {
        public ObservableList<IIncantation> Incantations { get; set; }
        public void Cast(float timing);
    }
}
