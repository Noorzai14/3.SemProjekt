namespace BarberAkji.Web.Models
{
    public class ErrorViewModel // Model til visning af fejl – bruges hvis noget går galt i webdelen
    {
        public string? RequestId { get; set; } // Unik ID for anmodningen, bruges til fejlfinding

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // Viser kun RequestId hvis den ikke er tom – bruges i fejlsider
    }
}
