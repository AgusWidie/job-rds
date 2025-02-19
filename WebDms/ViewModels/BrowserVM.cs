using WebDms.Models;
using Directory = WebDms.Models.Directory;

namespace WebDms.ViewModels
{
    public class BrowserVM
    {
        public Directory directory { get; set; }
        public Document document { get; set; }
        public List<TreeDirectory> treeDirectories { get; set; }
        public List<Collection> collections { get; set; }
        public List<DocumentVM> documents { get; set; }
        public List<DocumentType> document_types { get; set; }
        //public List<User> users { get; set; }
    }
}
