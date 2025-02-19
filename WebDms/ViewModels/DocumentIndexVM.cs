using WebDms.Models;

namespace WebDms.ViewModels
{
    public class DocumentIndexVM
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public List<DocumentIndex> document_index { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public DocumentType document_type { get; set; }
    }
}
