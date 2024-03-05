using RepositoryLayer.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Repository.Entity;

public class LabelEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LabelId { get; set; }
    public string LabelName { get; set; }
    [ForeignKey("Note")]
    public int Id { get; set; }
    [JsonIgnore]
    public virtual UserEntity Note { get; set; }
    [ForeignKey("NoteId")]
    public int NotesId { get; set; }

    [JsonIgnore]
    public virtual NotesEntity NoteId { get; set; }
}