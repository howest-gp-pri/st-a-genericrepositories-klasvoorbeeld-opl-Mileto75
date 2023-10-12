namespace Pri.Ca.Web.ViewModels
{
    public class GamesInfoViewModel : BaseViewModel
    {
        public string Description { get; set; }
        public BaseViewModel Publisher { get; set; }
        public IEnumerable<BaseViewModel> Genres { get; set; }
    }
}
