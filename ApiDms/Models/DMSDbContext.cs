using Microsoft.EntityFrameworkCore;

namespace ApiDms.Models
{
    public class DMSDbContext : DbContext
    {
        static DMSDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DMSDbContext(DbContextOptions<DMSDbContext> options) : base(options) { }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<CollectionDocumentType> CollectionDocumentTypes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<VDocument> VDocuments { get; set; }
        public DbSet<VDocumentVersions> VDocumentVersions { get; set; }
        public DbSet<Directory> Directories { get; set; }
        public DbSet<DocumentFavorite> DocumentFavorites { get; set; }
        public DbSet<DocumentVersions> DocumentVersions { get; set; }
        public DbSet<DocumentTag> DocumentTags { get; set; }
        public DbSet<DocumentTagValue> DocumentTagsValue { get; set; }
        public DbSet<DocumentIndex> DocumentIndices { get; set; }
        public DbSet<DocumentIndexValue> DocumentIndicesValue { get; set; }
        public DbSet<JsTreeModel> FCJsTreeModels { get; set; }
        public DbSet<DocumentDelete> DocumentDelete { get; set; }
    }
}
